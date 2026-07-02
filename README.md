# ListasCompras — Guia de Alterações no Banco de Dados

Este projeto usa um banco **SQLite** (`loja.db`), gerado automaticamente na primeira
execução com dados iniciais (`ListasCompras/Data/SeedData.cs`). Como ainda não existe
uma tela de administração no site para editar categorias, produtos ou modelos de
celular, essas alterações precisam ser feitas direto no banco.

> ⚠️ O seed (`SeedData.cs`) só roda **uma vez**, quando o banco está vazio. Depois que
> o banco já tem dados, editar `SeedData.cs` não muda nada — é preciso alterar o
> arquivo `loja.db` diretamente, como mostrado abaixo.

## Onde fica o banco

```
ListasCompras/bin/Debug/net10.0/loja.db
```

## Duas formas de editar o banco

Você pode usar o terminal (`sqlite3`) ou uma interface gráfica (`DB Browser for
SQLite` / `sqlitebrowser`). O resultado final é o mesmo — escolha a que preferir.

### Opção A — Terminal (`sqlite3`)

Pré-requisito: ter o `sqlite3` instalado. Para checar:

```bash
sqlite3 --version
```

Passo a passo:

1. Abra o banco pelo terminal:
   ```bash
   sqlite3 ListasCompras/bin/Debug/net10.0/loja.db
   ```
2. Rode o comando SQL desejado (exemplos abaixo).
3. Confira o resultado com um `SELECT`.
4. Saia com `.quit`.
5. Recarregue a página no navegador (F5) — não precisa reiniciar o site, o SQLite lê o
   arquivo direto.

Dica: você também pode rodar um comando único sem entrar no modo interativo:

```bash
sqlite3 ListasCompras/bin/Debug/net10.0/loja.db "SELECT * FROM Categorias;"
```

### Opção B — DB Browser for SQLite (interface gráfica)

Pré-requisito: ter o `sqlitebrowser` instalado (pacote `sqlitebrowser` na maioria das
distros Linux, ou baixar em https://sqlitebrowser.org).

Para abrir já direto no banco do projeto, pelo terminal:

```bash
sqlitebrowser ListasCompras/bin/Debug/net10.0/loja.db
```

Passo a passo:

1. Vá na aba **Execute SQL**.
2. Cole o comando SQL desejado (exemplos abaixo).
3. Execute com o botão **▶ (play)** ou `Ctrl+Enter`.
4. Clique em **"Escrever modificações"** (Write Changes) — sem isso a alteração não é
   salva de fato no arquivo.
5. Se pedir para salvar o arquivo ao fechar, salve **por cima do mesmo arquivo
   original** (`ListasCompras/bin/Debug/net10.0/loja.db`), não em outro lugar —
   senão o site continua lendo o banco antigo sem a alteração.
6. Recarregue a página no navegador (F5).

> ⚠️ Enquanto o DB Browser está com o banco aberto, o arquivo fica "travado" para
> escrita. Se o site (`dotnet run`) tentar gravar algo ao mesmo tempo (por exemplo,
> alguém adicionando um item na lista), pode dar erro de `database is locked`. Feche o
> DB Browser (ou pelo menos escreva as modificações) antes de usar o site normalmente.

---

## Tabelas principais

| Tabela | Colunas | Descrição |
|---|---|---|
| `Categorias` | Id, Nome, RequerModelo | Categorias do dropdown (Capinha, Película, Cabo...). `RequerModelo` = 1 se a categoria exige selecionar marca/modelo do celular. |
| `Produtos` | Id, Nome, Descricao, CategoriaId | Itens vendidos, vinculados a uma categoria. |
| `MarcasCelular` | Id, Nome | Marcas (Samsung, Apple, Motorola, Xiaomi...). |
| `ModelosCelular` | Id, Nome, MarcaCelularId | Modelos de celular (Galaxy A22, iPhone 13...), vinculados a uma marca. Um modelo cadastrado fica disponível para **qualquer** categoria com `RequerModelo = 1` (não precisa cadastrar separado por categoria). |

---

## Renomear uma categoria (ex: "Cabo USB" → outro nome)

```sql
UPDATE Categorias SET Nome = 'Novo Nome' WHERE Nome = 'Cabo USB';
```

## Adicionar uma nova categoria

```sql
INSERT INTO Categorias (Nome, RequerModelo) VALUES ('Suporte Veicular', 0);
```

Use `RequerModelo = 1` se a categoria precisar de marca/modelo de celular (como Capinha
e Película).

## Adicionar um novo produto dentro de uma categoria existente

```sql
INSERT INTO Produtos (Nome, CategoriaId)
VALUES ('Suporte Magnético', (SELECT Id FROM Categorias WHERE Nome = 'Suporte Veicular'));
```

## Adicionar um novo modelo de celular (ex: Galaxy A22 na Samsung)

```sql
INSERT INTO ModelosCelular (Nome, MarcaCelularId)
VALUES ('Galaxy A22', (SELECT Id FROM MarcasCelular WHERE Nome = 'Samsung'));
```

Esse modelo passa a aparecer automaticamente no dropdown "Modelo" para qualquer
categoria que exija modelo (Capinha, Película etc.), sem precisar repetir o cadastro.

## Adicionar uma nova marca de celular

```sql
INSERT INTO MarcasCelular (Nome) VALUES ('Realme');
```

## Editar (renomear/corrigir) outros registros

```sql
-- Renomear um produto
UPDATE Produtos SET Nome = 'Capinha Transparente' WHERE Nome = 'Capinha Silicone';

-- Renomear um modelo de celular
UPDATE ModelosCelular SET Nome = 'Galaxy A23s' WHERE Nome = 'Galaxy A23';

-- Trocar uma categoria para exigir modelo (ou deixar de exigir)
UPDATE Categorias SET RequerModelo = 1 WHERE Nome = 'Suporte Veicular';
```

## Remover um registro

```sql
DELETE FROM Produtos WHERE Nome = 'Nome do Produto';
DELETE FROM ModelosCelular WHERE Nome = 'Galaxy A05';
DELETE FROM Categorias WHERE Nome = 'Suporte Veicular';
```

> ⚠️ Cuidado: apagar uma `Categoria` ou `Produto` que já tem itens em listas de compras
> vinculados apaga **em cascata** esses itens (`ON DELETE CASCADE`). O mesmo vale para
> `MarcasCelular` → `ModelosCelular`. Se não tiver certeza, faça um backup antes (veja
> abaixo) ou rode um `SELECT` primeiro para ver o que seria afetado.

## Conferir se um registro foi inserido/alterado corretamente

```sql
SELECT * FROM Categorias WHERE Nome LIKE '%Suporte%';
SELECT * FROM ModelosCelular WHERE Nome = 'Galaxy A22';
SELECT * FROM Produtos ORDER BY Id DESC LIMIT 5; -- últimos produtos inseridos
```

## Ver o que seria apagado antes de um DELETE

```sql
-- Quantos itens de lista usam esse produto?
SELECT COUNT(*) FROM ItensListaCompra WHERE ProdutoId =
  (SELECT Id FROM Produtos WHERE Nome = 'Nome do Produto');
```

---

## Fazer backup antes de mexer no banco

Como o banco é só um arquivo, basta copiá-lo antes de qualquer alteração:

```bash
cp ListasCompras/bin/Debug/net10.0/loja.db ListasCompras/bin/Debug/net10.0/loja.db.bak
```

Se algo der errado, é só restaurar a cópia:

```bash
cp ListasCompras/bin/Debug/net10.0/loja.db.bak ListasCompras/bin/Debug/net10.0/loja.db
```

## Erros comuns

| Erro | Causa provável |
|---|---|
| `database is locked` | O DB Browser (ou outro programa) está com o banco aberto ao mesmo tempo que o site. Feche um dos dois. |
| Alteração não aparece no site | Esqueceu de clicar em "Escrever modificações" no DB Browser, ou salvou o arquivo em outro lugar. Confirme o caminho `ListasCompras/bin/Debug/net10.0/loja.db`. |
| `FOREIGN KEY constraint failed` | Está tentando inserir um `CategoriaId`/`MarcaCelularId` que não existe, ou apagar um registro "pai" (Categoria/Marca) que ainda tem "filhos" (Produtos/Modelos) vinculados. |
| Editou `SeedData.cs` mas nada mudou | O seed só roda quando o banco está vazio. Depois que já tem dados, altere direto no `loja.db` (não no código). |

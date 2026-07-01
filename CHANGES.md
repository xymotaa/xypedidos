# Registro de Alterações

## [2026-07-01] Namespace incorreto em SeedData.cs (recorrência)

### Problema
Ao adicionar `Data/SeedData.cs` (populador inicial de categorias/marcas/modelos) e integrá-lo ao `Program.cs`, o novo arquivo repetiu o mesmo erro já corrigido anteriormente: foi declarado com o namespace singular `ListaCompras` (sem 's') em vez de `ListasCompras`, e `Program.cs` importava `using ListaCompras.Data;` além do `using ListasCompras.Data;` correto — o projeto não compilava (`CS0246`).

### Arquivos Alterados

| Arquivo | Antes | Depois |
|---|---|---|
| `Data/SeedData.cs` | `namespace ListaCompras.Data` + `using ListaCompras.Models` | `namespace ListasCompras.Data` + `using ListasCompras.Models` |
| `Program.cs` | `using ListasCompras.Data;` + `using ListaCompras.Data;` (duplicado/errado) | apenas `using ListasCompras.Data;` |

### Resultado
- Build volta a compilar sem erros (`dotnet build` — 0 erros, 2 avisos, mesmo aviso pendente de `SQLitePCLRaw.lib.e_sqlite3` já registrado abaixo).
- `Program.cs` agora chama `SeedData.Initialize(db)` após `db.Database.Migrate()`, populando o banco com categorias, marcas e modelos de celular na primeira execução.

---

## [2026-07-01] Correção de Namespaces Inconsistentes

### Problema
O projeto usa o nome `ListasCompras` (com 's'), mas 8 arquivos declaravam namespace como `ListaCompras` (sem 's'), causando 3 erros de compilação (`CS0246 - tipo ou namespace não encontrado`):

```
Models/Categoria.cs(7): error CS0246 – 'Produto' não encontrado
Models/Produto.cs(10): error CS0246 – 'Categoria' não encontrado
Data/AppDbContext.cs(10): error CS0246 – 'Categoria' não encontrado
```

### Causa Raiz
Mistura entre dois namespaces distintos:
- **Correto** (`ListasCompras.*`): `Categoria.cs`, `ErrorViewModel.cs`, `HomeController.cs`
- **Incorreto** (`ListaCompra.*`): todos os demais arquivos

### Arquivos Alterados

| Arquivo | Antes | Depois |
|---|---|---|
| `Models/Produto.cs` | `namespace ListaCompras.Models` | `namespace ListasCompras.Models` |
| `Models/ListaCompra.cs` | `namespace ListaCompras.Models` | `namespace ListasCompras.Models` |
| `Models/ItemListaCompra.cs` | `namespace ListaCompras.Models` | `namespace ListasCompras.Models` |
| `Models/MarcaCelular.cs` | `namespace ListaCompras.Models` | `namespace ListasCompras.Models` |
| `Models/ModeloCelular.cs` | `namespace ListaCompras.Models` | `namespace ListasCompras.Models` |
| `Models/ProdutoModeloCompatibilidade.cs` | `namespace ListaCompras.Models` | `namespace ListasCompras.Models` |
| `Data/AppDbContext.cs` | `namespace ListaCompras.Data` + `using ListaCompras.Models` | `namespace ListasCompras.Data` + `using ListasCompras.Models` |
| `Program.cs` | `using ListaCompras.Data` | `using ListasCompras.Data` |

### Resultado
- **Antes:** 3 erros, 2 avisos  
- **Depois:** 0 erros, 2 avisos (aviso de vulnerabilidade em `SQLitePCLRaw.lib.e_sqlite3` v2.1.11 — pendente de atualização de pacote)

---

## Avisos Pendentes

### NU1903 – Vulnerabilidade Alta em SQLitePCLRaw.lib.e_sqlite3 v2.1.11
- **CVE:** [GHSA-2m69-gcr7-jv3q](https://github.com/advisories/GHSA-2m69-gcr7-jv3q)
- **Pacote afetado:** `Microsoft.EntityFrameworkCore.Sqlite` puxa `SQLitePCLRaw.lib.e_sqlite3` v2.1.11
- **Ação recomendada:** Aguardar patch do EF Core 10 ou fixar versão segura via `PackageReference` direto no `.csproj`

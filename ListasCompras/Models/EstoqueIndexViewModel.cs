namespace ListasCompras.Models;

public class EstoqueIndexViewModel
{
    public List<ProdutoEstoque> Produtos { get; set; } = new();
}

public class ProdutoEstoque
{
    public string Codigo { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public int SaldoAtual { get; set; }
    public int EstoqueMinimo { get; set; }
    public decimal CustoUnitario { get; set; }
    public decimal PrecoVenda { get; set; }
}

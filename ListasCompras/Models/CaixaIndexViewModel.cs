namespace ListasCompras.Models;

public class CaixaIndexViewModel
{
    public List<ProdutoCaixa> Produtos { get; set; } = new();
}

public class ProdutoCaixa
{
    public string Codigo { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public decimal PrecoUnitario { get; set; }
}

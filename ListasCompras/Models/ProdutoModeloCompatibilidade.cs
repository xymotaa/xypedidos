namespace ListasCompras.Models;

public class ProdutoModeloCompatibilidade
{
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;

    public int ModeloCelularId { get; set; }
    public ModeloCelular ModeloCelular { get; set; } = null!;
}
namespace ListasCompras.Models;

public class ItemListaCompra
{
    public int Id { get; set; }
    public int Quantidade { get; set; }
    public string? Observacao { get; set; }

    public int ListaCompraId { get; set; }
    public ListaCompra ListaCompra { get; set; } = null!;

    public int ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;

    // Nullable: só preenchido se o produto requer modelo (capinha, película)
    public int? ModeloCelularId { get; set; }
    public ModeloCelular? ModeloCelular { get; set; }
}
namespace ListasCompras.Models;

public class ReposicaoIndexViewModel
{
    public ListaCompra Lista { get; set; } = null!;
    public List<Categoria> Categorias { get; set; } = new();
    public List<MarcaCelular> Marcas { get; set; } = new();
    public List<ModeloCelular> Modelos { get; set; } = new();
    public List<Produto> Produtos { get; set; } = new();
}

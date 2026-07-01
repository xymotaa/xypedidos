namespace ListasCompras.Models;

public class ListaCompra
{
    public int Id { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public string Status { get; set; } = "Aberta"; // Aberta | Finalizada

    public ICollection<ItemListaCompra> Itens { get; set; } = new List<ItemListaCompra>();
}
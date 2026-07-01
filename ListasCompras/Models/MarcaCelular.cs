namespace ListasCompras.Models;

public class MarcaCelular
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty; // Samsung, Apple, Motorola...

    public ICollection<ModeloCelular> Modelos { get; set; } = new List<ModeloCelular>();
}
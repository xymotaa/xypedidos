namespace ListasCompras.Models;
public class Categoria
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public bool RequerModelo { get; set; } // true = capinha, pelicula | false = fone, pendrive
    public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}
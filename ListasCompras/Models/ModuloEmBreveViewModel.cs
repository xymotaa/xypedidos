namespace ListasCompras.Models;

// Tela genérica para módulos do ERP ainda não construídos.
public class ModuloEmBreveViewModel
{
    public string Icone { get; set; } = "";
    public string Nome { get; set; } = "";
    public string Descricao { get; set; } = "";
    public List<string> Recursos { get; set; } = new();
}

namespace ListasCompras.Models;

public class ConfiguracaoLoja
{
    public int Id { get; set; }
    public string NomeLoja { get; set; } = "Minha Loja";
    public string? LogoBase64 { get; set; } // data URL (ex: data:image/png;base64,...)
}

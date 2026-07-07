using ListasCompras.Data;
using ListasCompras.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListasCompras.Controllers;

public class CaixaController : LojaControllerBase
{
    public CaixaController(AppDbContext context) : base(context) { }

    public IActionResult Index()
    {
        return View("EmBreve", new ModuloEmBreveViewModel
        {
            Icone = "💵",
            Nome = "Caixa",
            Descricao = "Registre vendas e controle as entradas e saídas do dia.",
            Recursos = new()
            {
                "Abertura e fechamento de caixa",
                "Registro de vendas e formas de pagamento",
                "Sangria e suprimento",
                "Resumo do movimento diário",
            },
        });
    }
}

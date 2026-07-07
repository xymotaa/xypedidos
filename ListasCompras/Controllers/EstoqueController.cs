using ListasCompras.Data;
using ListasCompras.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListasCompras.Controllers;

public class EstoqueController : LojaControllerBase
{
    public EstoqueController(AppDbContext context) : base(context) { }

    public IActionResult Index()
    {
        return View("EmBreve", new ModuloEmBreveViewModel
        {
            Icone = "📦",
            Nome = "Estoque",
            Descricao = "Acompanhe as quantidades em loja e o que precisa repor.",
            Recursos = new()
            {
                "Saldo atual por produto e modelo",
                "Entradas e saídas de mercadoria",
                "Alertas de estoque baixo",
                "Ligação direta com a lista de compras",
            },
        });
    }
}

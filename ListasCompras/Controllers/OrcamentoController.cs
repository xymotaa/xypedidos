using ListasCompras.Data;
using ListasCompras.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListasCompras.Controllers;

public class OrcamentoController : LojaControllerBase
{
    public OrcamentoController(AppDbContext context) : base(context) { }

    public IActionResult Index()
    {
        return View("EmBreve", new ModuloEmBreveViewModel
        {
            Icone = "🧾",
            Nome = "Orçamento",
            Descricao = "Monte orçamentos para clientes e acompanhe o fechamento.",
            Recursos = new()
            {
                "Orçamentos com produtos e serviços",
                "Geração de PDF para o cliente",
                "Controle de aprovados e recusados",
                "Conversão de orçamento em venda",
            },
        });
    }
}

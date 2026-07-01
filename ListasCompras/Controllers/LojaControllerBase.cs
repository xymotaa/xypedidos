using ListasCompras.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ListasCompras.Controllers;

public abstract class LojaControllerBase : Controller
{
    protected readonly AppDbContext Context;

    protected LojaControllerBase(AppDbContext context)
    {
        Context = context;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var config = Context.ConfiguracoesLoja.FirstOrDefault();
        ViewData["NomeLoja"] = config?.NomeLoja ?? "Minha Loja";
        ViewData["LogoLoja"] = config?.LogoBase64;
        base.OnActionExecuting(context);
    }
}

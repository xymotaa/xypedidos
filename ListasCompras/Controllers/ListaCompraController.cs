using ListasCompras.Data;
using ListasCompras.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ListasCompras.Controllers;

public class ListaCompraController : LojaControllerBase
{
    public ListaCompraController(AppDbContext context) : base(context) { }

    private ListaCompra ObterListaAberta()
    {
        var lista = Context.ListasCompra
            .Include(l => l.Itens).ThenInclude(i => i.Produto).ThenInclude(p => p.Categoria)
            .Include(l => l.Itens).ThenInclude(i => i.ModeloCelular).ThenInclude(m => m!.MarcaCelular)
            .FirstOrDefault(l => l.Status == "Aberta");

        if (lista == null)
        {
            lista = new ListaCompra { Status = "Aberta" };
            Context.ListasCompra.Add(lista);
            Context.SaveChanges();
        }

        return lista;
    }

    public IActionResult Index()
    {
        var vm = new ReposicaoIndexViewModel
        {
            Lista = ObterListaAberta(),
            Categorias = Context.Categorias.OrderBy(c => c.Nome).ToList(),
            Marcas = Context.MarcasCelular.OrderBy(m => m.Nome).ToList(),
            Modelos = Context.ModelosCelular.OrderBy(m => m.Nome).ToList(),
            Produtos = Context.Produtos.OrderBy(p => p.Nome).ToList(),
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AdicionarItem(int produtoId, int? modeloCelularId, int quantidade, string? observacao)
    {
        var lista = ObterListaAberta();
        Context.ItensListaCompra.Add(new ItemListaCompra
        {
            ListaCompraId = lista.Id,
            ProdutoId = produtoId,
            ModeloCelularId = modeloCelularId,
            Quantidade = quantidade,
            Observacao = observacao,
        });
        Context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditarItem(int id, int quantidade, string? observacao)
    {
        var item = Context.ItensListaCompra.Find(id);
        if (item != null)
        {
            item.Quantidade = quantidade;
            item.Observacao = observacao;
            Context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RemoverItem(int id)
    {
        var item = Context.ItensListaCompra.Find(id);
        if (item != null)
        {
            Context.ItensListaCompra.Remove(item);
            Context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult FinalizarLista(int id)
    {
        var lista = Context.ListasCompra.Find(id);
        if (lista != null && lista.Status == "Aberta")
        {
            lista.Status = "Finalizada";
            Context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ExcluirLista(int id)
    {
        var lista = Context.ListasCompra
            .Include(l => l.Itens)
            .FirstOrDefault(l => l.Id == id && l.Status == "Aberta");

        if (lista != null)
        {
            Context.ItensListaCompra.RemoveRange(lista.Itens);
            Context.ListasCompra.Remove(lista);
            Context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }

    public IActionResult GerarPdf(int listaId)
    {
        var lista = Context.ListasCompra
            .Include(l => l.Itens).ThenInclude(i => i.Produto).ThenInclude(p => p.Categoria)
            .Include(l => l.Itens).ThenInclude(i => i.ModeloCelular).ThenInclude(m => m!.MarcaCelular)
            .FirstOrDefault(l => l.Id == listaId);

        if (lista == null) return NotFound();

        return View(lista);
    }
}

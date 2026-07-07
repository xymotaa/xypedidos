using ListasCompras.Data;
using ListasCompras.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListasCompras.Controllers;

public class CaixaController : LojaControllerBase
{
    public CaixaController(AppDbContext context) : base(context) { }

    public IActionResult Index()
    {
        return View(new CaixaIndexViewModel
        {
            Produtos = new()
            {
                new() { Codigo = "78910101", Nome = "Capinha Silicone iPhone 13", PrecoUnitario = 39.90m },
                new() { Codigo = "45678912", Nome = "Película 3D Samsung A13", PrecoUnitario = 24.90m },
                new() { Codigo = "11223344", Nome = "Fone de Ouvido Bluetooth", PrecoUnitario = 89.90m },
                new() { Codigo = "55667788", Nome = "Carregador Turbo 20W", PrecoUnitario = 49.90m },
                new() { Codigo = "99001122", Nome = "Cabo USB-C 1 Metro", PrecoUnitario = 19.90m },
                new() { Codigo = "33445566", Nome = "Pendrive 32GB", PrecoUnitario = 34.90m },
                new() { Codigo = "22334455", Nome = "Fonte de Alimentação 5V", PrecoUnitario = 29.90m },
                new() { Codigo = "66778899", Nome = "Suporte Veicular para Celular", PrecoUnitario = 44.90m },
            },
        });
    }
}

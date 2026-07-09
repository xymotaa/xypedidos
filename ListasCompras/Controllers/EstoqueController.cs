using ListasCompras.Data;
using ListasCompras.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListasCompras.Controllers;

public class EstoqueController : LojaControllerBase
{
    public EstoqueController(AppDbContext context) : base(context) { }

    public IActionResult Index()
    {
        return View(new EstoqueIndexViewModel
        {
            Produtos = new()
            {
                new() { Codigo = "78910101", Nome = "Capinha Silicone iPhone 13", Categoria = "Capinha", SaldoAtual = 34, EstoqueMinimo = 10, CustoUnitario = 18.50m, PrecoVenda = 39.90m },
                new() { Codigo = "45678912", Nome = "Película 3D Samsung A13", Categoria = "Película", SaldoAtual = 6, EstoqueMinimo = 15, CustoUnitario = 9.00m, PrecoVenda = 24.90m },
                new() { Codigo = "11223344", Nome = "Fone de Ouvido Bluetooth", Categoria = "Fone", SaldoAtual = 0, EstoqueMinimo = 5, CustoUnitario = 42.00m, PrecoVenda = 89.90m },
                new() { Codigo = "55667788", Nome = "Carregador Turbo 20W", Categoria = "Carregador", SaldoAtual = 21, EstoqueMinimo = 8, CustoUnitario = 22.00m, PrecoVenda = 49.90m },
                new() { Codigo = "99001122", Nome = "Cabo USB-C 1 Metro", Categoria = "Cabo", SaldoAtual = 3, EstoqueMinimo = 10, CustoUnitario = 8.50m, PrecoVenda = 19.90m },
                new() { Codigo = "33445566", Nome = "Pendrive 32GB", Categoria = "Acessório", SaldoAtual = 12, EstoqueMinimo = 6, CustoUnitario = 16.00m, PrecoVenda = 34.90m },
                new() { Codigo = "22334455", Nome = "Fonte de Alimentação 5V", Categoria = "Carregador", SaldoAtual = 0, EstoqueMinimo = 4, CustoUnitario = 14.00m, PrecoVenda = 29.90m },
                new() { Codigo = "66778899", Nome = "Suporte Veicular para Celular", Categoria = "Acessório", SaldoAtual = 17, EstoqueMinimo = 5, CustoUnitario = 20.00m, PrecoVenda = 44.90m },
                new() { Codigo = "10293847", Nome = "Capinha Silicone Samsung A54", Categoria = "Capinha", SaldoAtual = 8, EstoqueMinimo = 10, CustoUnitario = 17.00m, PrecoVenda = 37.90m },
                new() { Codigo = "56473829", Nome = "Película de Vidro iPhone 14", Categoria = "Película", SaldoAtual = 28, EstoqueMinimo = 12, CustoUnitario = 7.50m, PrecoVenda = 22.90m },
            },
        });
    }
}

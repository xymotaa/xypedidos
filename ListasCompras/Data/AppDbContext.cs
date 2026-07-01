using ListasCompras.Models;
using Microsoft.EntityFrameworkCore;

namespace ListasCompras.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<MarcaCelular> MarcasCelular { get; set; }
    public DbSet<ModeloCelular> ModelosCelular { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<ProdutoModeloCompatibilidade> ProdutoModeloCompatibilidades { get; set; }
    public DbSet<ListaCompra> ListasCompra { get; set; }
    public DbSet<ItemListaCompra> ItensListaCompra { get; set; }
    public DbSet<ConfiguracaoLoja> ConfiguracoesLoja { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Chave composta da tabela pivot (produto <-> modelo)
        modelBuilder.Entity<ProdutoModeloCompatibilidade>()
            .HasKey(p => new { p.ProdutoId, p.ModeloCelularId });

        // Produto -> Compatibilidades
        modelBuilder.Entity<ProdutoModeloCompatibilidade>()
            .HasOne(p => p.Produto)
            .WithMany(p => p.Compatibilidades)
            .HasForeignKey(p => p.ProdutoId);

        // ModeloCelular -> Compatibilidades
        modelBuilder.Entity<ProdutoModeloCompatibilidade>()
            .HasOne(p => p.ModeloCelular)
            .WithMany(m => m.Compatibilidades)
            .HasForeignKey(p => p.ModeloCelularId);
    }
}
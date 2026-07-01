using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ListasCompras.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    RequerModelo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListasCompra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListasCompra", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarcasCelular",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcasCelular", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModelosCelular",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    MarcaCelularId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelosCelular", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModelosCelular_MarcasCelular_MarcaCelularId",
                        column: x => x.MarcaCelularId,
                        principalTable: "MarcasCelular",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItensListaCompra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    Observacao = table.Column<string>(type: "TEXT", nullable: true),
                    ListaCompraId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProdutoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ModeloCelularId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensListaCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensListaCompra_ListasCompra_ListaCompraId",
                        column: x => x.ListaCompraId,
                        principalTable: "ListasCompra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensListaCompra_ModelosCelular_ModeloCelularId",
                        column: x => x.ModeloCelularId,
                        principalTable: "ModelosCelular",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItensListaCompra_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoModeloCompatibilidades",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ModeloCelularId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoModeloCompatibilidades", x => new { x.ProdutoId, x.ModeloCelularId });
                    table.ForeignKey(
                        name: "FK_ProdutoModeloCompatibilidades_ModelosCelular_ModeloCelularId",
                        column: x => x.ModeloCelularId,
                        principalTable: "ModelosCelular",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutoModeloCompatibilidades_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItensListaCompra_ListaCompraId",
                table: "ItensListaCompra",
                column: "ListaCompraId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensListaCompra_ModeloCelularId",
                table: "ItensListaCompra",
                column: "ModeloCelularId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensListaCompra_ProdutoId",
                table: "ItensListaCompra",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelosCelular_MarcaCelularId",
                table: "ModelosCelular",
                column: "MarcaCelularId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoModeloCompatibilidades_ModeloCelularId",
                table: "ProdutoModeloCompatibilidades",
                column: "ModeloCelularId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensListaCompra");

            migrationBuilder.DropTable(
                name: "ProdutoModeloCompatibilidades");

            migrationBuilder.DropTable(
                name: "ListasCompra");

            migrationBuilder.DropTable(
                name: "ModelosCelular");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "MarcasCelular");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}

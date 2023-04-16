using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Eventos.IO.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(150)", nullable: false),
                    ClassLevelCascadeMode = table.Column<int>(type: "int", nullable: false),
                    RuleLevelCascadeMode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizadores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(150)", nullable: false),
                    CPF = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    ClassLevelCascadeMode = table.Column<int>(type: "int", nullable: false),
                    RuleLevelCascadeMode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(150)", nullable: false),
                    DescricaoCurta = table.Column<string>(type: "varchar(max)", nullable: false),
                    DescricaoLonga = table.Column<string>(type: "varchar(max)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gratuito = table.Column<bool>(type: "bit", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Excluido = table.Column<bool>(type: "bit", nullable: false),
                    Online = table.Column<bool>(type: "bit", nullable: false),
                    NomeEmpresa = table.Column<string>(type: "varchar(150)", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EnderecoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassLevelCascadeMode = table.Column<int>(type: "int", nullable: false),
                    RuleLevelCascadeMode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Eventos_Organizadores_OrganizadorId",
                        column: x => x.OrganizadorId,
                        principalTable: "Organizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Logradouro = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Numero = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Complemento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Bairro = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    CEP = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    Cidade = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClassLevelCascadeMode = table.Column<int>(type: "int", nullable: false),
                    RuleLevelCascadeMode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enderecos_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_EventoId",
                table: "Enderecos",
                column: "EventoId",
                unique: true,
                filter: "[EventoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_CategoriaId",
                table: "Eventos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_OrganizadorId",
                table: "Eventos",
                column: "OrganizadorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Organizadores");
        }
    }
}

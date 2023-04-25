using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluxoCaixa.Infrastructure.Data.Migrations.FluxoCaixa
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAIXAS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SALDO = table.Column<decimal>(type: "money", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAIXAS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LANCAMENTOS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TIPOLANCAMENTO = table.Column<byte>(type: "tinyint", nullable: false),
                    VALOR = table.Column<decimal>(type: "money", nullable: false),
                    DATALANCAMENTO = table.Column<DateTime>(type: "date", nullable: false),
                    HORALANCAMENTO = table.Column<TimeSpan>(type: "time", nullable: false),
                    IDCAIXA = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LANCAMENTO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LANCAMENTO_CAIXAS_CAIXAID",
                        column: x => x.IDCAIXA,
                        principalTable: "CAIXAS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LOJAS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "varchar(255)", nullable: false),
                    CNPJ = table.Column<string>(type: "varchar(14)", nullable: false),
                    IDCAIXA = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOJA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CAIXAS_LOJA_LOJAID",
                        column: x => x.IDCAIXA,
                        principalTable: "CAIXAS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USUARIOS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "varchar(255)", nullable: false),
                    EMAIL = table.Column<string>(type: "varchar(255)", nullable: false),
                    IDLOJA = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIOS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USUARIOS_LOJA_LOJAID",
                        column: x => x.IDLOJA,
                        principalTable: "LOJAS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LANCAMENTOS_DATALANCAMENTO",
                table: "LANCAMENTOS",
                column: "DATALANCAMENTO");

            migrationBuilder.CreateIndex(
                name: "IX_LANCAMENTOS_IDCAIXA",
                table: "LANCAMENTOS",
                column: "IDCAIXA");

            migrationBuilder.CreateIndex(
                name: "IX_LANCAMENTOS_TIPOLANCAMENTO",
                table: "LANCAMENTOS",
                column: "TIPOLANCAMENTO");

            migrationBuilder.CreateIndex(
                name: "IX_LOJAS_IDCAIXA",
                table: "LOJAS",
                column: "IDCAIXA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USUARIOS_IDLOJA",
                table: "USUARIOS",
                column: "IDLOJA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LANCAMENTOS");

            migrationBuilder.DropTable(
                name: "USUARIOS");

            migrationBuilder.DropTable(
                name: "LOJAS");

            migrationBuilder.DropTable(
                name: "CAIXAS");
        }
    }
}

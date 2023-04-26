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
                name: "RELATORIOS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    STATUS = table.Column<byte>(type: "tinyint", nullable: false),
                    CAMINHOARQUIVO = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RELATORIOS", x => x.ID);
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
                    table.PrimaryKey("PK_LANCAMENTOS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LANCAMENTOS_CAIXAS_CAIXAID",
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
                name: "RELATORIO_METADADOS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VALOR = table.Column<string>(type: "text", nullable: false),
                    IDRELATORIO = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RELATORIOMETADADOS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RELATORIOS_RELATORIOMETADADOS_METADADOSID",
                        column: x => x.IDRELATORIO,
                        principalTable: "RELATORIOS",
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
                name: "IX_RELATORIO_METADADOS_IDRELATORIO",
                table: "RELATORIO_METADADOS",
                column: "IDRELATORIO",
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
                name: "RELATORIO_METADADOS");

            migrationBuilder.DropTable(
                name: "USUARIOS");

            migrationBuilder.DropTable(
                name: "RELATORIOS");

            migrationBuilder.DropTable(
                name: "LOJAS");

            migrationBuilder.DropTable(
                name: "CAIXAS");
        }
    }
}

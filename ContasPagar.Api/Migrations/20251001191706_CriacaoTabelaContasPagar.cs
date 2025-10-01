using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ContasPagar.Api.Migrations
{
    public partial class CriacaoTabelaContasPagar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contas_pagar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    ValorOriginal = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    DataVencimento = table.Column<DateTime>(nullable: false),
                    DataPagamento = table.Column<DateTime>(nullable: false),
                    ValorCorrigido = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    QuantidadeDiasAtraso = table.Column<int>(nullable: false),
                    PercentualMultaAplicado = table.Column<decimal>(type: "decimal(5, 2)", nullable: false),
                    PercentualJurosDiaAplicado = table.Column<decimal>(type: "decimal(5, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contas_pagar", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contas_pagar");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CurrencyConverter.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Currency_Name = table.Column<string>(nullable: false),
                    Currency_Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Currency_Name);
                });

            migrationBuilder.CreateTable(
                name: "Conversion",
                columns: table => new
                {
                    Conversion_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Conv_Input_Value = table.Column<double>(nullable: false),
                    Conv_Input_Currency = table.Column<string>(nullable: true),
                    Conv_Output_Value = table.Column<double>(nullable: false),
                    Conv_Output_Currency = table.Column<string>(nullable: true),
                    Conv_Date = table.Column<DateTime>(nullable: false),
                    CurrencyModelCurrency_Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversion", x => x.Conversion_ID);
                    table.ForeignKey(
                        name: "FK_Conversion_Currency_CurrencyModelCurrency_Name",
                        column: x => x.CurrencyModelCurrency_Name,
                        principalTable: "Currency",
                        principalColumn: "Currency_Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversion_CurrencyModelCurrency_Name",
                table: "Conversion",
                column: "CurrencyModelCurrency_Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conversion");

            migrationBuilder.DropTable(
                name: "Currency");
        }
    }
}

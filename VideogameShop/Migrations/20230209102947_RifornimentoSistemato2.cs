using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideogameShop.Migrations
{
    /// <inheritdoc />
    public partial class RifornimentoSistemato2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rifornimenti_Fornitore_FornitoreId",
                table: "Rifornimenti");

            migrationBuilder.AlterColumn<int>(
                name: "FornitoreId",
                table: "Rifornimenti",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Rifornimenti_Fornitore_FornitoreId",
                table: "Rifornimenti",
                column: "FornitoreId",
                principalTable: "Fornitore",
                principalColumn: "FornitoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rifornimenti_Fornitore_FornitoreId",
                table: "Rifornimenti");

            migrationBuilder.AlterColumn<int>(
                name: "FornitoreId",
                table: "Rifornimenti",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rifornimenti_Fornitore_FornitoreId",
                table: "Rifornimenti",
                column: "FornitoreId",
                principalTable: "Fornitore",
                principalColumn: "FornitoreId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

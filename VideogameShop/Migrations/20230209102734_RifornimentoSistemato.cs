using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideogameShop.Migrations
{
    /// <inheritdoc />
    public partial class RifornimentoSistemato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rifornimenti_Fornitore_FornitoreId",
                table: "Rifornimenti");

            migrationBuilder.DropForeignKey(
                name: "FK_Rifornimenti_Videogiochi_VideogiocoId",
                table: "Rifornimenti");

            migrationBuilder.AlterColumn<int>(
                name: "VideogiocoId",
                table: "Rifornimenti",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Rifornimenti_Videogiochi_VideogiocoId",
                table: "Rifornimenti",
                column: "VideogiocoId",
                principalTable: "Videogiochi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rifornimenti_Fornitore_FornitoreId",
                table: "Rifornimenti");

            migrationBuilder.DropForeignKey(
                name: "FK_Rifornimenti_Videogiochi_VideogiocoId",
                table: "Rifornimenti");

            migrationBuilder.AlterColumn<int>(
                name: "VideogiocoId",
                table: "Rifornimenti",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Rifornimenti_Videogiochi_VideogiocoId",
                table: "Rifornimenti",
                column: "VideogiocoId",
                principalTable: "Videogiochi",
                principalColumn: "Id");
        }
    }
}

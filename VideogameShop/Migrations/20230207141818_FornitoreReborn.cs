using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideogameShop.Migrations
{
    /// <inheritdoc />
    public partial class FornitoreReborn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rifornimenti_Videogiochi_VideogiocoId",
                table: "Rifornimenti");

            migrationBuilder.DropColumn(
                name: "FornitoreIndirizzo",
                table: "Fornitore");

            migrationBuilder.DropColumn(
                name: "FornitoreNumero",
                table: "Fornitore");

            migrationBuilder.AlterColumn<int>(
                name: "VideogiocoId",
                table: "Rifornimenti",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Rifornimenti_Videogiochi_VideogiocoId",
                table: "Rifornimenti",
                column: "VideogiocoId",
                principalTable: "Videogiochi",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "FornitoreIndirizzo",
                table: "Fornitore",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FornitoreNumero",
                table: "Fornitore",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Rifornimenti_Videogiochi_VideogiocoId",
                table: "Rifornimenti",
                column: "VideogiocoId",
                principalTable: "Videogiochi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

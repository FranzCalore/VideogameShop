using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideogameShop.Migrations
{
    /// <inheritdoc />
    public partial class Fornitori : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeFornitore",
                table: "Rifornimenti");

            migrationBuilder.AddColumn<int>(
                name: "FornitoreId",
                table: "Rifornimenti",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Fornitore",
                columns: table => new
                {
                    FornitoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FornitoreNome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    FornitoreIndirizzo = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    FornitoreNumero = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornitore", x => x.FornitoreId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rifornimenti_FornitoreId",
                table: "Rifornimenti",
                column: "FornitoreId");

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

            migrationBuilder.DropTable(
                name: "Fornitore");

            migrationBuilder.DropIndex(
                name: "IX_Rifornimenti_FornitoreId",
                table: "Rifornimenti");

            migrationBuilder.DropColumn(
                name: "FornitoreId",
                table: "Rifornimenti");

            migrationBuilder.AddColumn<string>(
                name: "NomeFornitore",
                table: "Rifornimenti",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

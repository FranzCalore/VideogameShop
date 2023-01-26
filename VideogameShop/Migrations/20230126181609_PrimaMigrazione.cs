using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideogameShop.Migrations
{
    /// <inheritdoc />
    public partial class PrimaMigrazione : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tipologia",
                columns: table => new
                {
                    TipologiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipologiaNome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipologia", x => x.TipologiaId);
                });

            migrationBuilder.CreateTable(
                name: "Videogiochi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Descrizione = table.Column<string>(type: "text", maxLength: 500, nullable: false),
                    Foto = table.Column<string>(type: "varchar(300)", nullable: false),
                    Prezzo = table.Column<double>(type: "float", nullable: false),
                    TipologiaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videogiochi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videogiochi_Tipologia_TipologiaId",
                        column: x => x.TipologiaId,
                        principalTable: "Tipologia",
                        principalColumn: "TipologiaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Videogiochi_TipologiaId",
                table: "Videogiochi",
                column: "TipologiaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Videogiochi");

            migrationBuilder.DropTable(
                name: "Tipologia");
        }
    }
}

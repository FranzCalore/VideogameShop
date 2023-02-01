using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideogameShop.Migrations
{
    /// <inheritdoc />
    public partial class ModelloConsole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsoleVideogioco",
                columns: table => new
                {
                    ListaConsoleId = table.Column<int>(type: "int", nullable: false),
                    ListaGiochiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsoleVideogioco", x => new { x.ListaConsoleId, x.ListaGiochiId });
                    table.ForeignKey(
                        name: "FK_ConsoleVideogioco_Consoles_ListaConsoleId",
                        column: x => x.ListaConsoleId,
                        principalTable: "Consoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsoleVideogioco_Videogiochi_ListaGiochiId",
                        column: x => x.ListaGiochiId,
                        principalTable: "Videogiochi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsoleVideogioco_ListaGiochiId",
                table: "ConsoleVideogioco",
                column: "ListaGiochiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsoleVideogioco");

            migrationBuilder.DropTable(
                name: "Consoles");
        }
    }
}

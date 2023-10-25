using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InduMovel.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaID);
                });

            migrationBuilder.CreateTable(
                name: "Moveis",
                columns: table => new
                {
                    MovelId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Cor = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ImagemUrl = table.Column<string>(type: "TEXT", nullable: true),
                    ImagemCurta = table.Column<string>(type: "TEXT", nullable: true),
                    Valor = table.Column<double>(type: "decimal(10,2)", nullable: false),
                    EmProducao = table.Column<bool>(type: "INTEGER", nullable: false),
                    Promocao = table.Column<bool>(type: "INTEGER", nullable: false),
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moveis", x => x.MovelId);
                    table.ForeignKey(
                        name: "FK_Moveis_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Moveis_CategoriaId",
                table: "Moveis",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Moveis");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}

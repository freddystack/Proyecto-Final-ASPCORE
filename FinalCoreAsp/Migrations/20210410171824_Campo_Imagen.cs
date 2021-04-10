using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalCoreAsp.Migrations
{
    public partial class Campo_Imagen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Autores",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AutorDTO",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorDTO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LibroDTO",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    AutorId = table.Column<int>(nullable: false),
                    AutorDTOId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibroDTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibroDTO_AutorDTO_AutorDTOId",
                        column: x => x.AutorDTOId,
                        principalTable: "AutorDTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LibroDTO_AutorDTOId",
                table: "LibroDTO",
                column: "AutorDTOId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibroDTO");

            migrationBuilder.DropTable(
                name: "AutorDTO");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Autores");
        }
    }
}

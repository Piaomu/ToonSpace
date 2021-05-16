using Microsoft.EntityFrameworkCore.Migrations;

namespace ToonSpace.data.migration
{
    public partial class _006 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "genre",
                table: "Genre",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Genre",
                newName: "genre");
        }
    }
}

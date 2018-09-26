using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductScanner.Database.Migrations
{
    public partial class PositionPropertiesNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PositionYR",
                table: "PhotoObject",
                newName: "PositionYMin");

            migrationBuilder.RenameColumn(
                name: "PositionYL",
                table: "PhotoObject",
                newName: "PositionYMax");

            migrationBuilder.RenameColumn(
                name: "PositionXR",
                table: "PhotoObject",
                newName: "PositionXMin");

            migrationBuilder.RenameColumn(
                name: "PositionXL",
                table: "PhotoObject",
                newName: "PositionXMax");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PositionYMin",
                table: "PhotoObject",
                newName: "PositionYR");

            migrationBuilder.RenameColumn(
                name: "PositionYMax",
                table: "PhotoObject",
                newName: "PositionYL");

            migrationBuilder.RenameColumn(
                name: "PositionXMin",
                table: "PhotoObject",
                newName: "PositionXR");

            migrationBuilder.RenameColumn(
                name: "PositionXMax",
                table: "PhotoObject",
                newName: "PositionXL");
        }
    }
}

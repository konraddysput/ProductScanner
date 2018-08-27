using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductScanner.Database.Migrations
{
    public partial class PhotoObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnalysedFilePath",
                table: "Photo",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PhotoObject",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Score = table.Column<double>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    PositionXL = table.Column<double>(nullable: false),
                    PositionYL = table.Column<double>(nullable: false),
                    PositionXR = table.Column<double>(nullable: false),
                    PositionYR = table.Column<double>(nullable: false),
                    PhotoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoObject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoObject_Photo_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoObject_PhotoId",
                table: "PhotoObject",
                column: "PhotoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoObject");

            migrationBuilder.DropColumn(
                name: "AnalysedFilePath",
                table: "Photo");
        }
    }
}

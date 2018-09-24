using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductScanner.Database.Migrations
{
    public partial class PhotoReasonerData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhotoData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    PhotoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoData_PhotoObject_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "PhotoObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotoType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true),
                    PhotoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoType_PhotoObject_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "PhotoObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoData_PhotoId",
                table: "PhotoData",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoType_PhotoId",
                table: "PhotoType",
                column: "PhotoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoData");

            migrationBuilder.DropTable(
                name: "PhotoType");
        }
    }
}

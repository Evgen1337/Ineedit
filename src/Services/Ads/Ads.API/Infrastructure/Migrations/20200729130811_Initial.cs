using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ads.API.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "AdTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ads",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    AdTypeId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ads_AdTypes_AdTypeId",
                        column: x => x.AdTypeId,
                        principalSchema: "dbo",
                        principalTable: "AdTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ads_AdTypeId",
                schema: "dbo",
                table: "Ads",
                column: "AdTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ads",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AdTypes",
                schema: "dbo");
        }
    }
}

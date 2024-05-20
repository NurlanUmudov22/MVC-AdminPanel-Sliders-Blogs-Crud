using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiorello_PB101.Migrations
{
    public partial class CreatedExpertTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Experts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experts", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 15, 0, 40, 32, 90, DateTimeKind.Local).AddTicks(285));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 15, 0, 40, 32, 90, DateTimeKind.Local).AddTicks(288));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 15, 0, 40, 32, 90, DateTimeKind.Local).AddTicks(290));

            migrationBuilder.InsertData(
                table: "Experts",
                columns: new[] { "Id", "CreatedDate", "Image", "Name", "Position", "SoftDeleted" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 15, 0, 40, 32, 90, DateTimeKind.Local).AddTicks(440), "h3-team-img-1.png ", "CRYSTAL BROOKS", "FLORIST", false },
                    { 2, new DateTime(2024, 5, 15, 0, 40, 32, 90, DateTimeKind.Local).AddTicks(444), "h3-team-img-2.png ", "SHIRLEY HARRIS", "Manager\r\n\r\n", false },
                    { 3, new DateTime(2024, 5, 15, 0, 40, 32, 90, DateTimeKind.Local).AddTicks(445), "h3-team-img-3.png ", "BEVERLY CLARK", "Florist", false },
                    { 4, new DateTime(2024, 5, 15, 0, 40, 32, 90, DateTimeKind.Local).AddTicks(447), "h3-team-img-4.png ", "AMANDA WATKINS ", "Florist", false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Experts");

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 14, 23, 35, 42, 996, DateTimeKind.Local).AddTicks(4219));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 14, 23, 35, 42, 996, DateTimeKind.Local).AddTicks(4222));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 14, 23, 35, 42, 996, DateTimeKind.Local).AddTicks(4225));
        }
    }
}

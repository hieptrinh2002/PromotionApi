using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PromotionApi.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateExpire = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Condition = table.Column<double>(type: "double", nullable: false),
                    Discount = table.Column<float>(type: "float", nullable: false),
                    IdMer = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Promotions",
                columns: new[] { "Id", "Code", "Condition", "DateExpire", "DateStart", "Discount", "IdMer" },
                values: new object[,]
                {
                    { "05b81e12-76fd-4f2f-93a3-31771ac40dd2", "HOLIDAY2023", 100.0, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.25f, "39ae71a3-1f29-4c5e-a0e8-12e06b70f7b5" },
                    { "7130a7c3-3896-495d-b58a-cadfbce4ef78", "FALLSALE2023", 75.0, new DateTime(2023, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.2f, "39ae71a3-1f29-4c5e-a0e8-12e06b70f7b5" },
                    { "fdc011be-2fd5-4b96-8c96-943caa9bb663", "SUMMER2023", 50.0, new DateTime(2023, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.15f, "39ae71a3-1f29-4c5e-a0e8-12e06b70f7b5" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promotions");
        }
    }
}

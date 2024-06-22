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
                    QuantityAvailable = table.Column<int>(type: "int", nullable: false),
                    Condition = table.Column<double>(type: "double", nullable: false),
                    Discount = table.Column<float>(type: "float", nullable: false),
                    MerchantId = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Promotions",
                columns: new[] { "Id", "Code", "Condition", "DateExpire", "DateStart", "Discount", "MerchantId", "QuantityAvailable" },
                values: new object[,]
                {
                    { "07965e75-5e8e-498f-ad3e-91ab366bbb3a", "HOLIDAY2023", 950000.0, new DateTime(2024, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 25f, "2236b29d-c850-4c6e-bb29-629be5eace69", 200 },
                    { "634f7816-e921-4873-b7d6-d583c717ded0", "FALLSALE2023", 750000.0, new DateTime(2024, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20f, "2236b29d-c850-4c6e-bb29-629be5eace69", 400 },
                    { "fb063534-11fe-40a5-ae59-ecc53bbc6eb7", "SUMMER2023", 850000.0, new DateTime(2024, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15f, "2236b29d-c850-4c6e-bb29-629be5eace69", 40 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_Code",
                table: "Promotions",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promotions");
        }
    }
}

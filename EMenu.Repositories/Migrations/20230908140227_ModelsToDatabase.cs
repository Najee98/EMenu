using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMenu.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class ModelsToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    attributeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    attributeName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.attributeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    categoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    categoryName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    categoryDescription = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.categoryId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    imageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    imageURL = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.imageId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    productId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    productName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    productDescription = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    productPrice = table.Column<double>(type: "double", nullable: true),
                    productImageId = table.Column<int>(type: "int", nullable: false),
                    imageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.productId);
                    table.ForeignKey(
                        name: "FK_Products_Images_imageId",
                        column: x => x.imageId,
                        principalTable: "Images",
                        principalColumn: "imageId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProductVariants",
                columns: table => new
                {
                    productVariantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    variantCreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    prodctId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    attributeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariants", x => x.productVariantId);
                    table.ForeignKey(
                        name: "FK_ProductVariants_Attributes_attributeId",
                        column: x => x.attributeId,
                        principalTable: "Attributes",
                        principalColumn: "attributeId");
                    table.ForeignKey(
                        name: "FK_ProductVariants_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VariantAttributes",
                columns: table => new
                {
                    productVariantId = table.Column<int>(type: "int", nullable: false),
                    attributeId = table.Column<int>(type: "int", nullable: false),
                    attributeDescription = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantAttributes", x => new { x.productVariantId, x.attributeId, x.attributeDescription });
                    table.ForeignKey(
                        name: "FK_VariantAttributes_Attributes_attributeId",
                        column: x => x.attributeId,
                        principalTable: "Attributes",
                        principalColumn: "attributeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VariantAttributes_ProductVariants_productVariantId",
                        column: x => x.productVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "productVariantId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VariantImages",
                columns: table => new
                {
                    productVariantId = table.Column<int>(type: "int", nullable: false),
                    imageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantImages", x => new { x.productVariantId, x.imageId });
                    table.ForeignKey(
                        name: "FK_VariantImages_Images_imageId",
                        column: x => x.imageId,
                        principalTable: "Images",
                        principalColumn: "imageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VariantImages_ProductVariants_productVariantId",
                        column: x => x.productVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "productVariantId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Products_imageId",
                table: "Products",
                column: "imageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_attributeId",
                table: "ProductVariants",
                column: "attributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_productId",
                table: "ProductVariants",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantAttributes_attributeId",
                table: "VariantAttributes",
                column: "attributeId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantImages_imageId",
                table: "VariantImages",
                column: "imageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "VariantAttributes");

            migrationBuilder.DropTable(
                name: "VariantImages");

            migrationBuilder.DropTable(
                name: "ProductVariants");

            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}

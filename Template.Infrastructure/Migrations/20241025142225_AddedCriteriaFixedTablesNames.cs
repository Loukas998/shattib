using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCriteriaFixedTablesNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_products_ProductId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_products_subcategories_SubCategoryId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_productspecifications_products_ProductId",
                table: "productspecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_productspecifications_specifications_SpecificationId",
                table: "productspecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_subcategories_categories_CategoryId",
                table: "subcategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_subcategories",
                table: "subcategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_specifications",
                table: "specifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productspecifications",
                table: "productspecifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_products",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.RenameTable(
                name: "subcategories",
                newName: "Subcategories");

            migrationBuilder.RenameTable(
                name: "specifications",
                newName: "Specifications");

            migrationBuilder.RenameTable(
                name: "productspecifications",
                newName: "Productspecifications");

            migrationBuilder.RenameTable(
                name: "products",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_subcategories_CategoryId",
                table: "Subcategories",
                newName: "IX_Subcategories_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_productspecifications_SpecificationId",
                table: "Productspecifications",
                newName: "IX_Productspecifications_SpecificationId");

            migrationBuilder.RenameIndex(
                name: "IX_products_SubCategoryId",
                table: "Products",
                newName: "IX_Products_SubCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subcategories",
                table: "Subcategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specifications",
                table: "Specifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Productspecifications",
                table: "Productspecifications",
                columns: new[] { "ProductId", "SpecificationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Criterias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criterias", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CriteriaId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Attachment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Criterias_CriteriaId",
                        column: x => x.CriteriaId,
                        principalTable: "Criterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CriteriaItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CriteriaId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    State = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Amount = table.Column<float>(type: "float", nullable: false),
                    MeasurementUnit = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriteriaItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriteriaItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CriteriaItems_Criterias_CriteriaId",
                        column: x => x.CriteriaId,
                        principalTable: "Criterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Image = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CriteriaId = table.Column<int>(type: "int", nullable: false),
                    Accepted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Criterias_CriteriaId",
                        column: x => x.CriteriaId,
                        principalTable: "Criterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CriteriaImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CriteriaCategoryId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriteriaImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriteriaImages_CriteriaItems_CriteriaCategoryId",
                        column: x => x.CriteriaCategoryId,
                        principalTable: "CriteriaItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CriteriaId",
                table: "Comments",
                column: "CriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_CriteriaImages_CriteriaCategoryId",
                table: "CriteriaImages",
                column: "CriteriaCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CriteriaItems_CategoryId",
                table: "CriteriaItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CriteriaItems_CriteriaId",
                table: "CriteriaItems",
                column: "CriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CriteriaId",
                table: "Invoices",
                column: "CriteriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Subcategories_SubCategoryId",
                table: "Products",
                column: "SubCategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productspecifications_Products_ProductId",
                table: "Productspecifications",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productspecifications_Specifications_SpecificationId",
                table: "Productspecifications",
                column: "SpecificationId",
                principalTable: "Specifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subcategories_Categories_CategoryId",
                table: "Subcategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Subcategories_SubCategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Productspecifications_Products_ProductId",
                table: "Productspecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Productspecifications_Specifications_SpecificationId",
                table: "Productspecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcategories_Categories_CategoryId",
                table: "Subcategories");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "CriteriaImages");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "CriteriaItems");

            migrationBuilder.DropTable(
                name: "Criterias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subcategories",
                table: "Subcategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specifications",
                table: "Specifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Productspecifications",
                table: "Productspecifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Subcategories",
                newName: "subcategories");

            migrationBuilder.RenameTable(
                name: "Specifications",
                newName: "specifications");

            migrationBuilder.RenameTable(
                name: "Productspecifications",
                newName: "productspecifications");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "products");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "categories");

            migrationBuilder.RenameIndex(
                name: "IX_Subcategories_CategoryId",
                table: "subcategories",
                newName: "IX_subcategories_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Productspecifications_SpecificationId",
                table: "productspecifications",
                newName: "IX_productspecifications_SpecificationId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SubCategoryId",
                table: "products",
                newName: "IX_products_SubCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_subcategories",
                table: "subcategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_specifications",
                table: "specifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productspecifications",
                table: "productspecifications",
                columns: new[] { "ProductId", "SpecificationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_products",
                table: "products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_subcategories_SubCategoryId",
                table: "products",
                column: "SubCategoryId",
                principalTable: "subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productspecifications_products_ProductId",
                table: "productspecifications",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productspecifications_specifications_SpecificationId",
                table: "productspecifications",
                column: "SpecificationId",
                principalTable: "specifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_subcategories_categories_CategoryId",
                table: "subcategories",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

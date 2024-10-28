using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedNullabilities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpecificationId",
                table: "Specifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_SpecificationId",
                table: "Specifications",
                column: "SpecificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Specifications_Specifications_SpecificationId",
                table: "Specifications",
                column: "SpecificationId",
                principalTable: "Specifications",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specifications_Specifications_SpecificationId",
                table: "Specifications");

            migrationBuilder.DropIndex(
                name: "IX_Specifications_SpecificationId",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "SpecificationId",
                table: "Specifications");
        }
    }
}

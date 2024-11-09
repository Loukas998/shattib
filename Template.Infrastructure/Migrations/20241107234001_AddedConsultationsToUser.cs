using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedConsultationsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Consultations");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Consultations",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_UserId",
                table: "Consultations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_AspNetUsers_UserId",
                table: "Consultations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_AspNetUsers_UserId",
                table: "Consultations");

            migrationBuilder.DropIndex(
                name: "IX_Consultations_UserId",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Consultations");

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "Consultations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Consultations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}

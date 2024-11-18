using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedSpecifiedMeasAndSomeFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsultationTopic",
                table: "Consultations");

            migrationBuilder.RenameColumn(
                name: "Meaurements",
                table: "Products",
                newName: "Measurements");

            migrationBuilder.CreateTable(
                name: "SpecifiedMeasurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Height = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Width = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Measurement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecifiedMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecifiedMeasurements_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpecifiedMeasurements_UserId",
                table: "SpecifiedMeasurements",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpecifiedMeasurements");

            migrationBuilder.RenameColumn(
                name: "Measurements",
                table: "Products",
                newName: "Meaurements");

            migrationBuilder.AddColumn<string>(
                name: "ConsultationTopic",
                table: "Consultations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

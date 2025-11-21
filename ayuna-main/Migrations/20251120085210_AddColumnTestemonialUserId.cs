using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ayuna_main.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnTestemonialUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "testimonials",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_testimonials_UserId",
                table: "testimonials",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_testimonials_AspNetUsers_UserId",
                table: "testimonials",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_testimonials_AspNetUsers_UserId",
                table: "testimonials");

            migrationBuilder.DropIndex(
                name: "IX_testimonials_UserId",
                table: "testimonials");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "testimonials");
        }
    }
}

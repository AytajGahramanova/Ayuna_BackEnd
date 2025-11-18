using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ayuna_main.Migrations
{
    /// <inheritdoc />
    public partial class AddProductIsLikeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isLike",
                table: "products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isLike",
                table: "products");
        }
    }
}

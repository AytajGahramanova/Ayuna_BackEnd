using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ayuna_main.Migrations
{
    /// <inheritdoc />
    public partial class AddTestimonialTableColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "testimonials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageOne",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageThree",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageTwo",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "detailDescription",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_testimonials_ProductId",
                table: "testimonials",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_detailDescription_ProductId",
                table: "detailDescription",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_detailDescription_products_ProductId",
                table: "detailDescription",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_testimonials_products_ProductId",
                table: "testimonials",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_detailDescription_products_ProductId",
                table: "detailDescription");

            migrationBuilder.DropForeignKey(
                name: "FK_testimonials_products_ProductId",
                table: "testimonials");

            migrationBuilder.DropIndex(
                name: "IX_testimonials_ProductId",
                table: "testimonials");

            migrationBuilder.DropIndex(
                name: "IX_detailDescription_ProductId",
                table: "detailDescription");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "testimonials");

            migrationBuilder.DropColumn(
                name: "ImageOne",
                table: "products");

            migrationBuilder.DropColumn(
                name: "ImageThree",
                table: "products");

            migrationBuilder.DropColumn(
                name: "ImageTwo",
                table: "products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "detailDescription");
        }
    }
}

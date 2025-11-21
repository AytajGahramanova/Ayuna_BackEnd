using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ayuna_main.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueFromTestimonials : Migration
    {

		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex(
				name: "IX_testimonials_ProductId",
				table: "testimonials");

			migrationBuilder.CreateIndex(
				name: "IX_testimonials_ProductId",
				table: "testimonials",
				column: "ProductId");
		}
		/// <inheritdoc />


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

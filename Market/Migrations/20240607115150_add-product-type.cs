using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Migrations
{
    /// <inheritdoc />
    public partial class addproducttype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "productType",
                table: "Products",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "productType",
                table: "Products");
        }
    }
}

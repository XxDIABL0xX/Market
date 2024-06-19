using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Migrations
{
    /// <inheritdoc />
    public partial class changeproducttype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Products",
                newName: "Namber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Namber",
                table: "Products",
                newName: "Quantity");
        }
    }
}

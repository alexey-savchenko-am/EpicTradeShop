using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnIsUpgradeable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RAMIsUpgradable",
                table: "LaptopProducts",
                newName: "RAMIsUpgradeable");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RAMIsUpgradeable",
                table: "LaptopProducts",
                newName: "RAMIsUpgradable");
        }
    }
}

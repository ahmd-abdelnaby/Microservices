using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixquantityname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Qauntity",
                table: "Inventorys",
                newName: "Quantity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Inventorys",
                newName: "Qauntity");
        }
    }
}

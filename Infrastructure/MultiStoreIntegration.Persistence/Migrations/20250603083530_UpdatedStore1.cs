using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiStoreIntegration.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedStore1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Sales",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Sales",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Sales",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Sales",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Sales");
        }
    }
}

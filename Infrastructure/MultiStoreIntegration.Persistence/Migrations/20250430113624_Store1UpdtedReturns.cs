using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiStoreIntegration.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Store1UpdtedReturns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Returns",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerPhone",
                table: "Returns",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "TotalPrice",
                table: "Returns",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Returns");

            migrationBuilder.DropColumn(
                name: "CustomerPhone",
                table: "Returns");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Returns");
        }
    }
}

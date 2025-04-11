using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IronCenter.Desktop.Migrations
{
    /// <inheritdoc />
    public partial class newproperty2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Sales",
                newName: "Income");

            migrationBuilder.AddColumn<decimal>(
                name: "Belefit",
                table: "Sales",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Belefit",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "Income",
                table: "Sales",
                newName: "TotalPrice");
        }
    }
}

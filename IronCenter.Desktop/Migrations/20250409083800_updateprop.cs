using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IronCenter.Desktop.Migrations
{
    /// <inheritdoc />
    public partial class updateprop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Storages_StorageId",
                table: "Sale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sale",
                table: "Sale");

            migrationBuilder.DropColumn(
                name: "SrorageId",
                table: "Sale");

            migrationBuilder.RenameTable(
                name: "Sale",
                newName: "Sales");

            migrationBuilder.RenameIndex(
                name: "IX_Sale_StorageId",
                table: "Sales",
                newName: "IX_Sales_StorageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sales",
                table: "Sales",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Storages_StorageId",
                table: "Sales",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Storages_StorageId",
                table: "Sales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sales",
                table: "Sales");

            migrationBuilder.RenameTable(
                name: "Sales",
                newName: "Sale");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_StorageId",
                table: "Sale",
                newName: "IX_Sale_StorageId");

            migrationBuilder.AddColumn<long>(
                name: "SrorageId",
                table: "Sale",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sale",
                table: "Sale",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Storages_StorageId",
                table: "Sale",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

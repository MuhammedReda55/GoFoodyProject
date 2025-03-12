using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BL.Migrations
{
    /// <inheritdoc />
    public partial class MenuMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TbMenuItems",
                newName: "FoodName");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "TbRestaurants",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TbMenuItems",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400);

            migrationBuilder.AddColumn<string>(
                name: "ProductImage",
                table: "TbMenuItems",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "TbMenuItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sale",
                table: "TbMenuItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "TbMenuItems",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "TbMenuItems",
                type: "decimal(8,2)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbRestaurants_OwnerId",
                table: "TbRestaurants",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbRestaurants_AspNetUsers_OwnerId",
                table: "TbRestaurants",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbRestaurants_AspNetUsers_OwnerId",
                table: "TbRestaurants");

            migrationBuilder.DropIndex(
                name: "IX_TbRestaurants_OwnerId",
                table: "TbRestaurants");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "TbRestaurants");

            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "TbMenuItems");

            migrationBuilder.DropColumn(
                name: "Qty",
                table: "TbMenuItems");

            migrationBuilder.DropColumn(
                name: "Sale",
                table: "TbMenuItems");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "TbMenuItems");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "TbMenuItems");

            migrationBuilder.RenameColumn(
                name: "FoodName",
                table: "TbMenuItems",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TbMenuItems",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400,
                oldNullable: true);
        }
    }
}

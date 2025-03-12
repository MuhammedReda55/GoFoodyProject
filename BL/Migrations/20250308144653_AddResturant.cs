using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BL.Migrations
{
    /// <inheritdoc />
    public partial class AddResturant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbRestaurants_AspNetUsers_OwnerId",
                table: "TbRestaurants");

            migrationBuilder.DropIndex(
                name: "IX_TbRestaurants_OwnerId",
                table: "TbRestaurants");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "TbRestaurants",
                newName: "OwnerName");

            migrationBuilder.AddColumn<string>(
                name: "RestaurantImage",
                table: "TbRestaurants",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RestaurantImage",
                table: "TbRestaurants");

            migrationBuilder.RenameColumn(
                name: "OwnerName",
                table: "TbRestaurants",
                newName: "OwnerId");

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
    }
}

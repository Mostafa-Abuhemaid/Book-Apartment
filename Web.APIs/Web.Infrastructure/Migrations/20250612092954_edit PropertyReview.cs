using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editPropertyReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyReviews_AspNetUsers_AppUserId",
                table: "PropertyReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyReviews_Properties_PropertyId1",
                table: "PropertyReviews");

            migrationBuilder.DropIndex(
                name: "IX_PropertyReviews_AppUserId",
                table: "PropertyReviews");

            migrationBuilder.DropIndex(
                name: "IX_PropertyReviews_PropertyId1",
                table: "PropertyReviews");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "PropertyReviews");

            migrationBuilder.DropColumn(
                name: "PropertyId1",
                table: "PropertyReviews");

            migrationBuilder.RenameColumn(
                name: "gender",
                table: "AspNetUsers",
                newName: "Gender");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "AspNetUsers",
                newName: "gender");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "PropertyReviews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PropertyId1",
                table: "PropertyReviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyReviews_AppUserId",
                table: "PropertyReviews",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyReviews_PropertyId1",
                table: "PropertyReviews",
                column: "PropertyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyReviews_AspNetUsers_AppUserId",
                table: "PropertyReviews",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyReviews_Properties_PropertyId1",
                table: "PropertyReviews",
                column: "PropertyId1",
                principalTable: "Properties",
                principalColumn: "Id");
        }
    }
}

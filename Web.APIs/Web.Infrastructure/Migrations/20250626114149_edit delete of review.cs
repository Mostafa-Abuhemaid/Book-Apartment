using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editdeleteofreview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyReviews_Properties_PropertyId",
                table: "PropertyReviews");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyReviews_Properties_PropertyId",
                table: "PropertyReviews",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyReviews_Properties_PropertyId",
                table: "PropertyReviews");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyReviews_Properties_PropertyId",
                table: "PropertyReviews",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

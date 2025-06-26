using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editdeleteoffavoriteandappointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Properties_PropertyId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Properties_PropertyId",
                table: "Favorites");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Properties_PropertyId",
                table: "Appointments",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Properties_PropertyId",
                table: "Favorites",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Properties_PropertyId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Properties_PropertyId",
                table: "Favorites");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Properties_PropertyId",
                table: "Appointments",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Properties_PropertyId",
                table: "Favorites",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

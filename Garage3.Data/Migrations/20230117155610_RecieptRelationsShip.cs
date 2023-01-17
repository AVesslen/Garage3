using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage3.Data.Migrations
{
    /// <inheritdoc />
    public partial class RecieptRelationsShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Receipt",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Receipt",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_MemberId",
                table: "Receipt",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_VehicleId",
                table: "Receipt",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_Member_MemberId",
                table: "Receipt",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_Vehicle_VehicleId",
                table: "Receipt",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_Member_MemberId",
                table: "Receipt");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_Vehicle_VehicleId",
                table: "Receipt");

            migrationBuilder.DropIndex(
                name: "IX_Receipt_MemberId",
                table: "Receipt");

            migrationBuilder.DropIndex(
                name: "IX_Receipt_VehicleId",
                table: "Receipt");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Receipt");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Receipt");
        }
    }
}

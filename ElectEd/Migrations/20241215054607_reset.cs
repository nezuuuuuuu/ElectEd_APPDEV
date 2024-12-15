using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectEd.Migrations
{
    /// <inheritdoc />
    public partial class reset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VoteSlips_ElectionId",
                table: "VoteSlips",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_ElectionId",
                table: "Positions",
                column: "ElectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Elections_ElectionId",
                table: "Positions",
                column: "ElectionId",
                principalTable: "Elections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoteSlips_Elections_ElectionId",
                table: "VoteSlips",
                column: "ElectionId",
                principalTable: "Elections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Elections_ElectionId",
                table: "Positions");

            migrationBuilder.DropForeignKey(
                name: "FK_VoteSlips_Elections_ElectionId",
                table: "VoteSlips");

            migrationBuilder.DropIndex(
                name: "IX_VoteSlips_ElectionId",
                table: "VoteSlips");

            migrationBuilder.DropIndex(
                name: "IX_Positions_ElectionId",
                table: "Positions");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTP_Project.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Users_CreatorId",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Users_CreatorId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_CreatorId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Meals_CreatorId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Meals");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Workouts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Meals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_UserId",
                table: "Workouts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_UserId",
                table: "Meals",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Users_UserId",
                table: "Meals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Users_UserId",
                table: "Workouts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Users_UserId",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Users_UserId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_UserId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Meals_UserId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Meals");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Workouts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Meals",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_CreatorId",
                table: "Workouts",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_CreatorId",
                table: "Meals",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Users_CreatorId",
                table: "Meals",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Users_CreatorId",
                table: "Workouts",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

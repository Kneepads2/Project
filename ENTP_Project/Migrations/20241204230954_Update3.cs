using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTP_Project.Migrations
{
    /// <inheritdoc />
    public partial class Update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealModel_Users_UserId",
                table: "MealModel");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutModel_Users_UserId",
                table: "WorkoutModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutModel",
                table: "WorkoutModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealModel",
                table: "MealModel");

            migrationBuilder.RenameTable(
                name: "WorkoutModel",
                newName: "Workouts");

            migrationBuilder.RenameTable(
                name: "MealModel",
                newName: "Meals");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutModel_UserId",
                table: "Workouts",
                newName: "IX_Workouts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MealModel_UserId",
                table: "Meals",
                newName: "IX_Meals_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workouts",
                table: "Workouts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meals",
                table: "Meals",
                column: "Id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workouts",
                table: "Workouts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meals",
                table: "Meals");

            migrationBuilder.RenameTable(
                name: "Workouts",
                newName: "WorkoutModel");

            migrationBuilder.RenameTable(
                name: "Meals",
                newName: "MealModel");

            migrationBuilder.RenameIndex(
                name: "IX_Workouts_UserId",
                table: "WorkoutModel",
                newName: "IX_WorkoutModel_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Meals_UserId",
                table: "MealModel",
                newName: "IX_MealModel_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutModel",
                table: "WorkoutModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealModel",
                table: "MealModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MealModel_Users_UserId",
                table: "MealModel",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutModel_Users_UserId",
                table: "WorkoutModel",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

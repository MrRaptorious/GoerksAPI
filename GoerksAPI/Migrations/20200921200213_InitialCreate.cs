using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoerksAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityCatalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastChange = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ActivityType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityCatalog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastChange = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Measurement",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastChange = table.Column<DateTime>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Weight = table.Column<float>(nullable: false),
                    Height = table.Column<float>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measurement_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Workout",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastChange = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workout_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CardioActivity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastChange = table.Column<DateTime>(nullable: false),
                    ActivityTypeId = table.Column<Guid>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    Done = table.Column<bool>(nullable: false),
                    SortIndex = table.Column<int>(nullable: false),
                    Distance = table.Column<float>(nullable: false),
                    WorkoutId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardioActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardioActivity_ActivityCatalog_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "ActivityCatalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardioActivity_Workout_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StrenghtActivitySet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastChange = table.Column<DateTime>(nullable: false),
                    SortIndex = table.Column<int>(nullable: false),
                    WorkoutId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrenghtActivitySet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StrenghtActivitySet_Workout_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StrenghtActivity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastChange = table.Column<DateTime>(nullable: false),
                    ActivityTypeId = table.Column<Guid>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    Done = table.Column<bool>(nullable: false),
                    SortIndex = table.Column<int>(nullable: false),
                    Reps = table.Column<int>(nullable: false),
                    StrenghtActivitySetId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrenghtActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StrenghtActivity_ActivityCatalog_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "ActivityCatalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StrenghtActivity_StrenghtActivitySet_StrenghtActivitySetId",
                        column: x => x.StrenghtActivitySetId,
                        principalTable: "StrenghtActivitySet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardioActivity_ActivityTypeId",
                table: "CardioActivity",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CardioActivity_WorkoutId",
                table: "CardioActivity",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurement_UserId",
                table: "Measurement",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StrenghtActivity_ActivityTypeId",
                table: "StrenghtActivity",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StrenghtActivity_StrenghtActivitySetId",
                table: "StrenghtActivity",
                column: "StrenghtActivitySetId");

            migrationBuilder.CreateIndex(
                name: "IX_StrenghtActivitySet_WorkoutId",
                table: "StrenghtActivitySet",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Workout_UserId",
                table: "Workout",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardioActivity");

            migrationBuilder.DropTable(
                name: "Measurement");

            migrationBuilder.DropTable(
                name: "StrenghtActivity");

            migrationBuilder.DropTable(
                name: "ActivityCatalog");

            migrationBuilder.DropTable(
                name: "StrenghtActivitySet");

            migrationBuilder.DropTable(
                name: "Workout");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

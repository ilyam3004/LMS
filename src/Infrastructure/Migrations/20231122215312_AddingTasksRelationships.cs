using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingTasksRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("4260cd4e-fb3b-4321-b867-95288e41d79b"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("5bed5959-11e4-48f3-8a46-e1b2757c2b57"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("a16e4095-9c92-47be-9e38-80bcee39418a"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("a55dfae8-844e-4be3-b7bd-f3efc9dd0188"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("ed5c126c-e1f8-4c0d-a16d-23f381d5a286"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("eea793c2-505b-46cb-a60a-e478a0a68fe5"));

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    TaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MaxGrade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Task_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("4566107b-758d-46fa-9719-522a8826c8d0"), "Electrical Engineering", "Group B" },
                    { new Guid("62e38c19-c3f7-4ff6-80de-ccb15f238711"), "Mathematics", "Group E" },
                    { new Guid("7f9fb6ad-e039-469e-8316-c90e09d59aef"), "Mechanical Engineering", "Group C" },
                    { new Guid("a24b49f5-4577-4eb6-9236-1213662d5c43"), "Chemistry", "Group F" },
                    { new Guid("dbbbdafb-2b34-4a98-a49e-50797389916b"), "Physics", "Group D" },
                    { new Guid("f2cfc642-44aa-4ed9-9285-97b88d55b272"), "Computer Science", "Group A" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_SubjectId",
                table: "Task",
                column: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("4566107b-758d-46fa-9719-522a8826c8d0"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("62e38c19-c3f7-4ff6-80de-ccb15f238711"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("7f9fb6ad-e039-469e-8316-c90e09d59aef"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("a24b49f5-4577-4eb6-9236-1213662d5c43"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("dbbbdafb-2b34-4a98-a49e-50797389916b"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("f2cfc642-44aa-4ed9-9285-97b88d55b272"));

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("4260cd4e-fb3b-4321-b867-95288e41d79b"), "Electrical Engineering", "Group B" },
                    { new Guid("5bed5959-11e4-48f3-8a46-e1b2757c2b57"), "Physics", "Group D" },
                    { new Guid("a16e4095-9c92-47be-9e38-80bcee39418a"), "Mechanical Engineering", "Group C" },
                    { new Guid("a55dfae8-844e-4be3-b7bd-f3efc9dd0188"), "Computer Science", "Group A" },
                    { new Guid("ed5c126c-e1f8-4c0d-a16d-23f381d5a286"), "Chemistry", "Group F" },
                    { new Guid("eea793c2-505b-46cb-a60a-e478a0a68fe5"), "Mathematics", "Group E" }
                });
        }
    }
}

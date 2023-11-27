using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingTaskComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("204d0439-a0de-41d6-92bc-56715ac660ba"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("2be714c2-60bb-4f17-bd2d-af622f1f6ff7"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("90925c1f-3fb1-4c9d-a927-d8708d09b2fc"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("ae29fe80-39d6-42c8-9c86-3602277ecbb7"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("b769d722-f4cd-4ba7-8a71-35d7a8dc8cfe"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("c27ede98-1239-476f-a36d-5a1c8201c863"));

            migrationBuilder.CreateTable(
                name: "TaskComments",
                columns: table => new
                {
                    TaskCommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Comment = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskComments", x => x.TaskCommentId);
                    table.ForeignKey(
                        name: "FK_TaskComments_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("01e8e2a5-c7c9-443c-b99c-e2220680cf8d"), "Mathematics", "Group E" },
                    { new Guid("046e0488-0dab-46e0-a013-5e13175e1564"), "Computer Science", "Group A" },
                    { new Guid("521fedae-7cc8-4ff7-adc9-693c6cbe4946"), "Mechanical Engineering", "Group C" },
                    { new Guid("aab5f7bc-6733-42d4-95ec-7a5fe14a1e8b"), "Electrical Engineering", "Group B" },
                    { new Guid("b1ab675c-3daf-4b13-a00a-e766b8419f22"), "Chemistry", "Group F" },
                    { new Guid("d82f7608-f9ab-467e-806a-79758b9eb449"), "Physics", "Group D" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskComments_TaskId",
                table: "TaskComments",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskComments_UserId",
                table: "TaskComments",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskComments");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("01e8e2a5-c7c9-443c-b99c-e2220680cf8d"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("046e0488-0dab-46e0-a013-5e13175e1564"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("521fedae-7cc8-4ff7-adc9-693c6cbe4946"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("aab5f7bc-6733-42d4-95ec-7a5fe14a1e8b"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("b1ab675c-3daf-4b13-a00a-e766b8419f22"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("d82f7608-f9ab-467e-806a-79758b9eb449"));

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("204d0439-a0de-41d6-92bc-56715ac660ba"), "Electrical Engineering", "Group B" },
                    { new Guid("2be714c2-60bb-4f17-bd2d-af622f1f6ff7"), "Mathematics", "Group E" },
                    { new Guid("90925c1f-3fb1-4c9d-a927-d8708d09b2fc"), "Chemistry", "Group F" },
                    { new Guid("ae29fe80-39d6-42c8-9c86-3602277ecbb7"), "Mechanical Engineering", "Group C" },
                    { new Guid("b769d722-f4cd-4ba7-8a71-35d7a8dc8cfe"), "Computer Science", "Group A" },
                    { new Guid("c27ede98-1239-476f-a36d-5a1c8201c863"), "Physics", "Group D" }
                });
        }
    }
}

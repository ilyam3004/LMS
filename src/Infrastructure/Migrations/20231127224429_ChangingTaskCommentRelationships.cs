using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangingTaskCommentRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskComments_Tasks_TaskId",
                table: "TaskComments");

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

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "TaskComments",
                newName: "StudentTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskComments_TaskId",
                table: "TaskComments",
                newName: "IX_TaskComments_StudentTaskId");

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("6b3e13bb-d858-4a7f-bf6f-578eec805619"), "Mechanical Engineering", "Group C" },
                    { new Guid("8802a678-8395-4827-897a-c85c1c02e0c3"), "Electrical Engineering", "Group B" },
                    { new Guid("9daa3884-a4da-4545-ad93-3662de384865"), "Computer Science", "Group A" },
                    { new Guid("a375292b-a848-4853-b713-2a31aa9511d4"), "Physics", "Group D" },
                    { new Guid("c49e481a-875c-4a44-992c-539fa41af32a"), "Chemistry", "Group F" },
                    { new Guid("f8e8c246-fc54-449f-a4aa-0c8e73897f07"), "Mathematics", "Group E" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TaskComments_StudentTasks_StudentTaskId",
                table: "TaskComments",
                column: "StudentTaskId",
                principalTable: "StudentTasks",
                principalColumn: "StudentTaskId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskComments_StudentTasks_StudentTaskId",
                table: "TaskComments");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("6b3e13bb-d858-4a7f-bf6f-578eec805619"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("8802a678-8395-4827-897a-c85c1c02e0c3"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("9daa3884-a4da-4545-ad93-3662de384865"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("a375292b-a848-4853-b713-2a31aa9511d4"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("c49e481a-875c-4a44-992c-539fa41af32a"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("f8e8c246-fc54-449f-a4aa-0c8e73897f07"));

            migrationBuilder.RenameColumn(
                name: "StudentTaskId",
                table: "TaskComments",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskComments_StudentTaskId",
                table: "TaskComments",
                newName: "IX_TaskComments_TaskId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_TaskComments_Tasks_TaskId",
                table: "TaskComments",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

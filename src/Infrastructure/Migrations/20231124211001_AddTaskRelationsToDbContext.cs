using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskRelationsToDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentTask_Students_StudentId",
                table: "StudentTask");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentTask_Task_TaskId",
                table: "StudentTask");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Subjects_SubjectId",
                table: "Task");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Task",
                table: "Task");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentTask",
                table: "StudentTask");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("400f7da0-eef7-4e6d-97db-6cc188e883a0"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("5820c032-3853-4b8f-b57a-48cf1c194e60"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("7efa13a5-f46c-4c2c-acbd-9b59127a4dbd"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("9dc986c1-966b-461b-92f5-48e717ee44b7"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("b7d27f9f-e353-4054-b0d6-05041393a4c7"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("bbdd6f48-f9ad-4214-8f29-6c1d13a12ac4"));

            migrationBuilder.RenameTable(
                name: "Task",
                newName: "Tasks");

            migrationBuilder.RenameTable(
                name: "StudentTask",
                newName: "StudentTasks");

            migrationBuilder.RenameIndex(
                name: "IX_Task_SubjectId",
                table: "Tasks",
                newName: "IX_Tasks_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentTask_TaskId",
                table: "StudentTasks",
                newName: "IX_StudentTasks_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentTask_StudentId",
                table: "StudentTasks",
                newName: "IX_StudentTasks_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentTasks",
                table: "StudentTasks",
                column: "StudentTaskId");

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("2e6a57ec-ec98-4466-82e2-744d0422a53e"), "Computer Science", "Group A" },
                    { new Guid("3e7e7eb7-5b06-4150-9456-472c011016d4"), "Physics", "Group D" },
                    { new Guid("8b85f69e-28d2-4160-abbe-a1d556ed214c"), "Mathematics", "Group E" },
                    { new Guid("b180e465-1f54-4253-993f-c16eb99b308e"), "Electrical Engineering", "Group B" },
                    { new Guid("e00db7db-54de-4dc6-a8bd-63886dfb5354"), "Chemistry", "Group F" },
                    { new Guid("fd870c7f-4a5c-4094-9660-69f9b7ef5a53"), "Mechanical Engineering", "Group C" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTasks_Students_StudentId",
                table: "StudentTasks",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTasks_Tasks_TaskId",
                table: "StudentTasks",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Subjects_SubjectId",
                table: "Tasks",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentTasks_Students_StudentId",
                table: "StudentTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentTasks_Tasks_TaskId",
                table: "StudentTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Subjects_SubjectId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentTasks",
                table: "StudentTasks");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("2e6a57ec-ec98-4466-82e2-744d0422a53e"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("3e7e7eb7-5b06-4150-9456-472c011016d4"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("8b85f69e-28d2-4160-abbe-a1d556ed214c"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("b180e465-1f54-4253-993f-c16eb99b308e"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("e00db7db-54de-4dc6-a8bd-63886dfb5354"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("fd870c7f-4a5c-4094-9660-69f9b7ef5a53"));

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "Task");

            migrationBuilder.RenameTable(
                name: "StudentTasks",
                newName: "StudentTask");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_SubjectId",
                table: "Task",
                newName: "IX_Task_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentTasks_TaskId",
                table: "StudentTask",
                newName: "IX_StudentTask_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentTasks_StudentId",
                table: "StudentTask",
                newName: "IX_StudentTask_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Task",
                table: "Task",
                column: "TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentTask",
                table: "StudentTask",
                column: "StudentTaskId");

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("400f7da0-eef7-4e6d-97db-6cc188e883a0"), "Electrical Engineering", "Group B" },
                    { new Guid("5820c032-3853-4b8f-b57a-48cf1c194e60"), "Mechanical Engineering", "Group C" },
                    { new Guid("7efa13a5-f46c-4c2c-acbd-9b59127a4dbd"), "Computer Science", "Group A" },
                    { new Guid("9dc986c1-966b-461b-92f5-48e717ee44b7"), "Mathematics", "Group E" },
                    { new Guid("b7d27f9f-e353-4054-b0d6-05041393a4c7"), "Physics", "Group D" },
                    { new Guid("bbdd6f48-f9ad-4214-8f29-6c1d13a12ac4"), "Chemistry", "Group F" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTask_Students_StudentId",
                table: "StudentTask",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTask_Task_TaskId",
                table: "StudentTask",
                column: "TaskId",
                principalTable: "Task",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Subjects_SubjectId",
                table: "Task",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

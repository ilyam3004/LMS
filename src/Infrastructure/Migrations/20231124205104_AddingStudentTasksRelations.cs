using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingStudentTasksRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupSubjects");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("0447414d-8b1a-4954-88ef-5418de21fe80"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("307e4a97-2d37-4daa-84af-e4658f649efd"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("7f4a4825-1482-430e-90f1-224f6be1463f"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("80f78c7f-296c-448c-abbf-d4610a8bf258"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("a8506a7b-a9ac-4dd7-907b-d0372703b012"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("d1823d90-6bb0-4e87-be88-f1c481d829d4"));

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Subjects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "StudentTask",
                columns: table => new
                {
                    StudentTaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileUrl = table.Column<string>(type: "text", nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Grade = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTask", x => x.StudentTaskId);
                    table.ForeignKey(
                        name: "FK_StudentTask_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentTask_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_GroupId",
                table: "Subjects",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTask_StudentId",
                table: "StudentTask",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTask_TaskId",
                table: "StudentTask",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Groups_GroupId",
                table: "Subjects",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Groups_GroupId",
                table: "Subjects");

            migrationBuilder.DropTable(
                name: "StudentTask");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_GroupId",
                table: "Subjects");

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

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Subjects");

            migrationBuilder.CreateTable(
                name: "GroupSubjects",
                columns: table => new
                {
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupSubjects", x => new { x.SubjectId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_GroupSubjects_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupSubjects_Subjects_SubjectId",
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
                    { new Guid("0447414d-8b1a-4954-88ef-5418de21fe80"), "Mathematics", "Group E" },
                    { new Guid("307e4a97-2d37-4daa-84af-e4658f649efd"), "Mechanical Engineering", "Group C" },
                    { new Guid("7f4a4825-1482-430e-90f1-224f6be1463f"), "Computer Science", "Group A" },
                    { new Guid("80f78c7f-296c-448c-abbf-d4610a8bf258"), "Physics", "Group D" },
                    { new Guid("a8506a7b-a9ac-4dd7-907b-d0372703b012"), "Chemistry", "Group F" },
                    { new Guid("d1823d90-6bb0-4e87-be88-f1c481d829d4"), "Electrical Engineering", "Group B" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupSubjects_GroupId",
                table: "GroupSubjects",
                column: "GroupId");
        }
    }
}

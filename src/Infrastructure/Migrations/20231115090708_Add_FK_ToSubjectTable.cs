using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_FK_ToSubjectTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupSubjects_Subject_SubjectId",
                table: "GroupSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Lecturers_LecturerId",
                table: "Subject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subject",
                table: "Subject");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("2ba13130-eb68-439e-9659-fd849011e1dd"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("2e539179-3ad1-439d-80d3-9b87ba4ee992"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("37e160fa-a4ad-4071-9718-e8dbc41a2f09"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("3d3f1176-41f0-41e2-9c1d-d703e82b6fc3"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("5c07bb8f-cdf5-4ec0-93c0-2498cafec5e3"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("5c774716-7b07-448b-bbc0-ee81f15309a2"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("65bec600-7e73-4bf7-8a47-1f7df1018fc6"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("848ab340-54e2-4a17-9dad-2674b251a550"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("a3e8d6bd-75f8-42df-9b9d-b0ad5cdd8b49"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("c9cae1d7-0a77-49fc-8840-f78fbc5051af"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("f3772db5-f34a-41cd-abfc-22b05d3675e1"));

            migrationBuilder.RenameTable(
                name: "Subject",
                newName: "Subjects");

            migrationBuilder.RenameIndex(
                name: "IX_Subject_LecturerId",
                table: "Subjects",
                newName: "IX_Subjects_LecturerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects",
                column: "SubjectId");

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("233beaf6-5b1d-4a5b-9f36-6103399fbfe5"), "Aerospace Engineering", "Group K" },
                    { new Guid("3bc56854-0d2e-455d-823e-7324f0fdba77"), "Mathematics", "Group E" },
                    { new Guid("56f84755-a70c-4fbc-8e7b-7c1b1002d5c4"), "Biology", "Group G" },
                    { new Guid("5c0530b5-ac03-4cdd-a836-481a53581a83"), "Computer Science", "Group A" },
                    { new Guid("81d8614e-4495-4323-bfd6-ab419ee70b80"), "Environmental Science", "Group I" },
                    { new Guid("8d54f894-5ef8-47ef-b954-597e6d9c5fe1"), "Chemistry", "Group F" },
                    { new Guid("b4edc1e0-fb0e-43a7-92fa-9a074a84e2af"), "Civil Engineering", "Group H" },
                    { new Guid("d4cc04b4-075d-4fe1-a767-fbdf2c447049"), "Electrical Engineering", "Group B" },
                    { new Guid("e3bdf84f-e23f-485a-888c-e1e0f6fa9fb2"), "Mechanical Engineering", "Group C" },
                    { new Guid("f1d75f6d-d20c-455a-a586-c3436c5f8a48"), "Information Technology", "Group J" },
                    { new Guid("fc7c4af8-2962-4c64-a9bf-0233a1fdca77"), "Physics", "Group D" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupSubjects_Subjects_SubjectId",
                table: "GroupSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Lecturers_LecturerId",
                table: "Subjects",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "LecturerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupSubjects_Subjects_SubjectId",
                table: "GroupSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Lecturers_LecturerId",
                table: "Subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("233beaf6-5b1d-4a5b-9f36-6103399fbfe5"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("3bc56854-0d2e-455d-823e-7324f0fdba77"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("56f84755-a70c-4fbc-8e7b-7c1b1002d5c4"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("5c0530b5-ac03-4cdd-a836-481a53581a83"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("81d8614e-4495-4323-bfd6-ab419ee70b80"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("8d54f894-5ef8-47ef-b954-597e6d9c5fe1"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("b4edc1e0-fb0e-43a7-92fa-9a074a84e2af"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("d4cc04b4-075d-4fe1-a767-fbdf2c447049"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("e3bdf84f-e23f-485a-888c-e1e0f6fa9fb2"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("f1d75f6d-d20c-455a-a586-c3436c5f8a48"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("fc7c4af8-2962-4c64-a9bf-0233a1fdca77"));

            migrationBuilder.RenameTable(
                name: "Subjects",
                newName: "Subject");

            migrationBuilder.RenameIndex(
                name: "IX_Subjects_LecturerId",
                table: "Subject",
                newName: "IX_Subject_LecturerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subject",
                table: "Subject",
                column: "SubjectId");

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("2ba13130-eb68-439e-9659-fd849011e1dd"), "Aerospace Engineering", "Group K" },
                    { new Guid("2e539179-3ad1-439d-80d3-9b87ba4ee992"), "Civil Engineering", "Group H" },
                    { new Guid("37e160fa-a4ad-4071-9718-e8dbc41a2f09"), "Environmental Science", "Group I" },
                    { new Guid("3d3f1176-41f0-41e2-9c1d-d703e82b6fc3"), "Chemistry", "Group F" },
                    { new Guid("5c07bb8f-cdf5-4ec0-93c0-2498cafec5e3"), "Information Technology", "Group J" },
                    { new Guid("5c774716-7b07-448b-bbc0-ee81f15309a2"), "Computer Science", "Group A" },
                    { new Guid("65bec600-7e73-4bf7-8a47-1f7df1018fc6"), "Mechanical Engineering", "Group C" },
                    { new Guid("848ab340-54e2-4a17-9dad-2674b251a550"), "Electrical Engineering", "Group B" },
                    { new Guid("a3e8d6bd-75f8-42df-9b9d-b0ad5cdd8b49"), "Physics", "Group D" },
                    { new Guid("c9cae1d7-0a77-49fc-8840-f78fbc5051af"), "Mathematics", "Group E" },
                    { new Guid("f3772db5-f34a-41cd-abfc-22b05d3675e1"), "Biology", "Group G" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupSubjects_Subject_SubjectId",
                table: "GroupSubjects",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Lecturers_LecturerId",
                table: "Subject",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "LecturerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

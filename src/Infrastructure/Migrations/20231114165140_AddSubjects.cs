using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSubjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("147de086-46ab-4137-b62d-2c811d04dcdc"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("19bb9322-311b-4fc2-9ad3-3dfe33599d6c"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("58bf5cf6-71f8-4ce0-b620-e76b6632ce3f"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("60255ebc-2e37-405d-af73-44572dcb9d87"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("6afa4354-1510-45fb-a8f4-f60b0dadb829"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("71053a7d-7833-42a3-878c-1f51dec47e7b"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("803c6305-3501-48a1-a3f6-54df0c9cb480"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("9441bc7c-bb5c-4b1c-b0be-783893b3e855"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("da519030-9eaf-429d-a83c-f95b3aedd5cf"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("ee6ec3bd-6beb-496a-9d6d-03519bfebb9d"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("fa8e4cec-8270-4920-94fd-807af7648b3d"));

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.SubjectId);
                });

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
                        name: "FK_GroupSubjects_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LecturerSubjects",
                columns: table => new
                {
                    LecturerId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturerSubjects", x => new { x.SubjectId, x.LecturerId });
                    table.ForeignKey(
                        name: "FK_LecturerSubjects_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "LecturerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LecturerSubjects_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("1ee68487-561c-413a-bd9e-eb1b47b308d0"), "Mechanical Engineering", "Group C" },
                    { new Guid("2c866674-5c28-49e6-9894-9c43ad33ef81"), "Environmental Science", "Group I" },
                    { new Guid("34df5e16-707d-4c48-8d79-c72634547ecc"), "Mathematics", "Group E" },
                    { new Guid("53399b0d-6e71-4e56-a0e4-65224877880f"), "Aerospace Engineering", "Group K" },
                    { new Guid("55e24f9c-18f4-4b94-b946-35ac8f71831c"), "Computer Science", "Group A" },
                    { new Guid("70e1f044-98a1-4557-a964-f049773f79b8"), "Civil Engineering", "Group H" },
                    { new Guid("a4204827-10cc-4104-99d0-bd0cdcaf14ff"), "Physics", "Group D" },
                    { new Guid("ac86f4cd-f829-4fd6-a6f3-b33c3236776b"), "Information Technology", "Group J" },
                    { new Guid("b02f971c-0604-48bd-8a5e-8e6b9c983437"), "Biology", "Group G" },
                    { new Guid("bcf40c82-e978-4337-91e4-8e889a045180"), "Chemistry", "Group F" },
                    { new Guid("f8c489f2-30ba-4c87-a2b1-9e746fa445cc"), "Electrical Engineering", "Group B" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupSubjects_GroupId",
                table: "GroupSubjects",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LecturerSubjects_LecturerId",
                table: "LecturerSubjects",
                column: "LecturerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupSubjects");

            migrationBuilder.DropTable(
                name: "LecturerSubjects");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("1ee68487-561c-413a-bd9e-eb1b47b308d0"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("2c866674-5c28-49e6-9894-9c43ad33ef81"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("34df5e16-707d-4c48-8d79-c72634547ecc"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("53399b0d-6e71-4e56-a0e4-65224877880f"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("55e24f9c-18f4-4b94-b946-35ac8f71831c"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("70e1f044-98a1-4557-a964-f049773f79b8"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("a4204827-10cc-4104-99d0-bd0cdcaf14ff"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("ac86f4cd-f829-4fd6-a6f3-b33c3236776b"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("b02f971c-0604-48bd-8a5e-8e6b9c983437"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("bcf40c82-e978-4337-91e4-8e889a045180"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("f8c489f2-30ba-4c87-a2b1-9e746fa445cc"));

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("147de086-46ab-4137-b62d-2c811d04dcdc"), "Environmental Science", "Group I" },
                    { new Guid("19bb9322-311b-4fc2-9ad3-3dfe33599d6c"), "Computer Science", "Group A" },
                    { new Guid("58bf5cf6-71f8-4ce0-b620-e76b6632ce3f"), "Mathematics", "Group E" },
                    { new Guid("60255ebc-2e37-405d-af73-44572dcb9d87"), "Electrical Engineering", "Group B" },
                    { new Guid("6afa4354-1510-45fb-a8f4-f60b0dadb829"), "Chemistry", "Group F" },
                    { new Guid("71053a7d-7833-42a3-878c-1f51dec47e7b"), "Mechanical Engineering", "Group C" },
                    { new Guid("803c6305-3501-48a1-a3f6-54df0c9cb480"), "Physics", "Group D" },
                    { new Guid("9441bc7c-bb5c-4b1c-b0be-783893b3e855"), "Aerospace Engineering", "Group K" },
                    { new Guid("da519030-9eaf-429d-a83c-f95b3aedd5cf"), "Civil Engineering", "Group H" },
                    { new Guid("ee6ec3bd-6beb-496a-9d6d-03519bfebb9d"), "Biology", "Group G" },
                    { new Guid("fa8e4cec-8270-4920-94fd-807af7648b3d"), "Information Technology", "Group J" }
                });
        }
    }
}

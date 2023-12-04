using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSubjectDescriptionLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Subjects",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(400)",
                oldMaxLength: 400);

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("10edc0ec-1358-4c77-b4e0-8a50536880b0"), "Physics", "Group D" },
                    { new Guid("326483ed-4472-481c-bcba-b00825cf5653"), "Computer Science", "Group A" },
                    { new Guid("365730a9-e457-4e89-a353-6dbf8d6aac02"), "Mathematics", "Group E" },
                    { new Guid("577a7bc7-1f7d-4cd1-bd9c-0b8926534d05"), "Mechanical Engineering", "Group C" },
                    { new Guid("a95faafe-b825-435a-ac61-f0fa560e80bd"), "Chemistry", "Group F" },
                    { new Guid("c4ed2185-a810-440f-b599-78937824d4bd"), "Electrical Engineering", "Group B" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("10edc0ec-1358-4c77-b4e0-8a50536880b0"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("326483ed-4472-481c-bcba-b00825cf5653"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("365730a9-e457-4e89-a353-6dbf8d6aac02"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("577a7bc7-1f7d-4cd1-bd9c-0b8926534d05"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("a95faafe-b825-435a-ac61-f0fa560e80bd"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("c4ed2185-a810-440f-b599-78937824d4bd"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Subjects",
                type: "character varying(400)",
                maxLength: 400,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

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
        }
    }
}

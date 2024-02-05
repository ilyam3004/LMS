using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCourseFieldFromStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Course",
                table: "Students");

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("13171360-85ed-488c-872d-158064608da6"), "Electrical Engineering", "Group B" },
                    { new Guid("1d6ccd8f-3b8b-438b-b0a2-c69c088cb062"), "Mathematics", "Group E" },
                    { new Guid("797bde11-43d8-40a9-9216-3ac335237835"), "Physics", "Group D" },
                    { new Guid("9d97fef4-d588-494a-86c8-275e6826b963"), "Chemistry", "Group F" },
                    { new Guid("d99d97a7-604c-4271-adf3-8e07f2959442"), "Mechanical Engineering", "Group C" },
                    { new Guid("ef812d23-3574-4285-a897-2458578c2a24"), "Computer Science", "Group A" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("13171360-85ed-488c-872d-158064608da6"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("1d6ccd8f-3b8b-438b-b0a2-c69c088cb062"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("797bde11-43d8-40a9-9216-3ac335237835"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("9d97fef4-d588-494a-86c8-275e6826b963"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("d99d97a7-604c-4271-adf3-8e07f2959442"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("ef812d23-3574-4285-a897-2458578c2a24"));

            migrationBuilder.AddColumn<int>(
                name: "Course",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
    }
}

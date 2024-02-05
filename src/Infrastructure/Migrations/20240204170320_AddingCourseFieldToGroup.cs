using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingCourseFieldToGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "Groups",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Course", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("0515b5ba-9353-49fc-b571-05cbef8badbe"), 3, "Mechanical Engineering", "Group C" },
                    { new Guid("20e6f083-b6f9-4969-984b-91fb0652d697"), 1, "Computer Science", "Group A" },
                    { new Guid("23f02866-16a5-41d0-bdcf-ce5d9538c1e9"), 6, "Chemistry", "Group F" },
                    { new Guid("5847b5f3-9002-4cf8-995e-61046f1d0ec8"), 5, "Mathematics", "Group E" },
                    { new Guid("94da5209-b330-427f-a5f9-326fe1562a2d"), 2, "Electrical Engineering", "Group B" },
                    { new Guid("c81e88f2-fa84-4304-93ed-2999ea23e95e"), 4, "Physics", "Group D" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("0515b5ba-9353-49fc-b571-05cbef8badbe"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("20e6f083-b6f9-4969-984b-91fb0652d697"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("23f02866-16a5-41d0-bdcf-ce5d9538c1e9"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("5847b5f3-9002-4cf8-995e-61046f1d0ec8"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("94da5209-b330-427f-a5f9-326fe1562a2d"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("c81e88f2-fa84-4304-93ed-2999ea23e95e"));

            migrationBuilder.DropColumn(
                name: "Course",
                table: "Groups");

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
    }
}

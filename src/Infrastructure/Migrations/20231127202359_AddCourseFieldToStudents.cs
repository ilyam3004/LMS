using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseFieldToStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("0cbd7397-1c21-4998-bd30-7f01004daa2f"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("71d43514-35a7-4e9d-aba3-2195391ba76a"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("dbfa1cc7-4891-4194-a621-8c29d91009c2"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("f6e6c779-15da-420b-a00f-10c469f6c825"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("fcc8f716-dacc-4359-9104-3da1a9045f5e"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("fd23bc35-5dd4-4a4a-bee1-ef17800f333f"));

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
                    { new Guid("204d0439-a0de-41d6-92bc-56715ac660ba"), "Electrical Engineering", "Group B" },
                    { new Guid("2be714c2-60bb-4f17-bd2d-af622f1f6ff7"), "Mathematics", "Group E" },
                    { new Guid("90925c1f-3fb1-4c9d-a927-d8708d09b2fc"), "Chemistry", "Group F" },
                    { new Guid("ae29fe80-39d6-42c8-9c86-3602277ecbb7"), "Mechanical Engineering", "Group C" },
                    { new Guid("b769d722-f4cd-4ba7-8a71-35d7a8dc8cfe"), "Computer Science", "Group A" },
                    { new Guid("c27ede98-1239-476f-a36d-5a1c8201c863"), "Physics", "Group D" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Course",
                table: "Students");

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("0cbd7397-1c21-4998-bd30-7f01004daa2f"), "Computer Science", "Group A" },
                    { new Guid("71d43514-35a7-4e9d-aba3-2195391ba76a"), "Physics", "Group D" },
                    { new Guid("dbfa1cc7-4891-4194-a621-8c29d91009c2"), "Chemistry", "Group F" },
                    { new Guid("f6e6c779-15da-420b-a00f-10c469f6c825"), "Mathematics", "Group E" },
                    { new Guid("fcc8f716-dacc-4359-9104-3da1a9045f5e"), "Mechanical Engineering", "Group C" },
                    { new Guid("fd23bc35-5dd4-4a4a-bee1-ef17800f333f"), "Electrical Engineering", "Group B" }
                });
        }
    }
}

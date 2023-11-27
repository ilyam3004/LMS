using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeOrdinalNameNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("04d6d36a-f3d7-495f-8cdd-244b2c4e5b6b"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("21c0b108-d282-45fc-8fe1-3dc59f536698"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("222dabf3-9132-491c-9158-ec5449b411bf"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("4874e7fb-49f1-4cf3-81c4-43b21b093cee"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("5b1c38f9-1cf6-4153-b24a-691145f8aeea"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("7529d857-3fcd-42a7-9e8a-3429cd27082e"));

            migrationBuilder.AlterColumn<string>(
                name: "OrdinalFileName",
                table: "StudentTasks",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "OrdinalFileName",
                table: "StudentTasks",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("04d6d36a-f3d7-495f-8cdd-244b2c4e5b6b"), "Chemistry", "Group F" },
                    { new Guid("21c0b108-d282-45fc-8fe1-3dc59f536698"), "Electrical Engineering", "Group B" },
                    { new Guid("222dabf3-9132-491c-9158-ec5449b411bf"), "Physics", "Group D" },
                    { new Guid("4874e7fb-49f1-4cf3-81c4-43b21b093cee"), "Mechanical Engineering", "Group C" },
                    { new Guid("5b1c38f9-1cf6-4153-b24a-691145f8aeea"), "Mathematics", "Group E" },
                    { new Guid("7529d857-3fcd-42a7-9e8a-3429cd27082e"), "Computer Science", "Group A" }
                });
        }
    }
}

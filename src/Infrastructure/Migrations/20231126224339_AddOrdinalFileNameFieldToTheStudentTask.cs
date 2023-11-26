using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrdinalFileNameFieldToTheStudentTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("14efcac2-e48c-46e8-bc5c-fdbb20151d0f"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("3769a4ee-b2af-457b-9af5-4d48e8e9407f"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("5f49d627-a4ae-4ca8-ad84-d3df8e8094a0"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("76111824-58da-4232-8b91-7cbccad1d624"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("ad1e9de5-623c-4a62-8ed0-c18790f61389"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("c9eb0c9d-aadf-4345-8421-9a557ff90f6f"));

            migrationBuilder.AddColumn<string>(
                name: "OrdinalFileName",
                table: "StudentTasks",
                type: "text",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "OrdinalFileName",
                table: "StudentTasks");

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("14efcac2-e48c-46e8-bc5c-fdbb20151d0f"), "Mechanical Engineering", "Group C" },
                    { new Guid("3769a4ee-b2af-457b-9af5-4d48e8e9407f"), "Chemistry", "Group F" },
                    { new Guid("5f49d627-a4ae-4ca8-ad84-d3df8e8094a0"), "Mathematics", "Group E" },
                    { new Guid("76111824-58da-4232-8b91-7cbccad1d624"), "Electrical Engineering", "Group B" },
                    { new Guid("ad1e9de5-623c-4a62-8ed0-c18790f61389"), "Computer Science", "Group A" },
                    { new Guid("c9eb0c9d-aadf-4345-8421-9a557ff90f6f"), "Physics", "Group D" }
                });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeGradeRequiredInStudentTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<int>(
                name: "Grade",
                table: "StudentTasks",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "Grade",
                table: "StudentTasks",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

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
        }
    }
}

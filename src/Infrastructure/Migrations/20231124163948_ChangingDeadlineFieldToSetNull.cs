using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangingDeadlineFieldToSetNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("4566107b-758d-46fa-9719-522a8826c8d0"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("62e38c19-c3f7-4ff6-80de-ccb15f238711"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("7f9fb6ad-e039-469e-8316-c90e09d59aef"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("a24b49f5-4577-4eb6-9236-1213662d5c43"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("dbbbdafb-2b34-4a98-a49e-50797389916b"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("f2cfc642-44aa-4ed9-9285-97b88d55b272"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "Task",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "Task",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("4566107b-758d-46fa-9719-522a8826c8d0"), "Electrical Engineering", "Group B" },
                    { new Guid("62e38c19-c3f7-4ff6-80de-ccb15f238711"), "Mathematics", "Group E" },
                    { new Guid("7f9fb6ad-e039-469e-8316-c90e09d59aef"), "Mechanical Engineering", "Group C" },
                    { new Guid("a24b49f5-4577-4eb6-9236-1213662d5c43"), "Chemistry", "Group F" },
                    { new Guid("dbbbdafb-2b34-4a98-a49e-50797389916b"), "Physics", "Group D" },
                    { new Guid("f2cfc642-44aa-4ed9-9285-97b88d55b272"), "Computer Science", "Group A" }
                });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeGroupInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("4260cd4e-fb3b-4321-b867-95288e41d79b"), "Electrical Engineering", "Group B" },
                    { new Guid("5bed5959-11e4-48f3-8a46-e1b2757c2b57"), "Physics", "Group D" },
                    { new Guid("a16e4095-9c92-47be-9e38-80bcee39418a"), "Mechanical Engineering", "Group C" },
                    { new Guid("a55dfae8-844e-4be3-b7bd-f3efc9dd0188"), "Computer Science", "Group A" },
                    { new Guid("ed5c126c-e1f8-4c0d-a16d-23f381d5a286"), "Chemistry", "Group F" },
                    { new Guid("eea793c2-505b-46cb-a60a-e478a0a68fe5"), "Mathematics", "Group E" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("4260cd4e-fb3b-4321-b867-95288e41d79b"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("5bed5959-11e4-48f3-8a46-e1b2757c2b57"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("a16e4095-9c92-47be-9e38-80bcee39418a"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("a55dfae8-844e-4be3-b7bd-f3efc9dd0188"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("ed5c126c-e1f8-4c0d-a16d-23f381d5a286"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("eea793c2-505b-46cb-a60a-e478a0a68fe5"));

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
        }
    }
}

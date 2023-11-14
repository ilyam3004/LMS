using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_FK_ToSubjectTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("05af42ed-e910-47b9-a8be-9ed175f835a2"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("1513132f-6ffd-4859-b791-50b28b05b879"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("1c63770b-020a-47b5-a633-d75067b223a3"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("488ec798-b1ae-40b5-a99b-7f1482a826d8"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("4ddcf3ec-9826-49fb-ab15-49c6eade3fe3"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("88c74764-d46a-4e52-b677-9c799bdfda57"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("8f91b42a-7c0a-4e53-a98d-1d6cbd657e10"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("c2a85c30-aaca-4d7c-80aa-6346dc45fdf0"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("e022eb28-e79f-4106-8708-b5f0a298c0e4"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("e7d10a8e-cef7-4d16-bee6-d76f7690971c"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("f97f6197-1142-4867-bcec-3104f7f3b600"));

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("2ba13130-eb68-439e-9659-fd849011e1dd"), "Aerospace Engineering", "Group K" },
                    { new Guid("2e539179-3ad1-439d-80d3-9b87ba4ee992"), "Civil Engineering", "Group H" },
                    { new Guid("37e160fa-a4ad-4071-9718-e8dbc41a2f09"), "Environmental Science", "Group I" },
                    { new Guid("3d3f1176-41f0-41e2-9c1d-d703e82b6fc3"), "Chemistry", "Group F" },
                    { new Guid("5c07bb8f-cdf5-4ec0-93c0-2498cafec5e3"), "Information Technology", "Group J" },
                    { new Guid("5c774716-7b07-448b-bbc0-ee81f15309a2"), "Computer Science", "Group A" },
                    { new Guid("65bec600-7e73-4bf7-8a47-1f7df1018fc6"), "Mechanical Engineering", "Group C" },
                    { new Guid("848ab340-54e2-4a17-9dad-2674b251a550"), "Electrical Engineering", "Group B" },
                    { new Guid("a3e8d6bd-75f8-42df-9b9d-b0ad5cdd8b49"), "Physics", "Group D" },
                    { new Guid("c9cae1d7-0a77-49fc-8840-f78fbc5051af"), "Mathematics", "Group E" },
                    { new Guid("f3772db5-f34a-41cd-abfc-22b05d3675e1"), "Biology", "Group G" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("2ba13130-eb68-439e-9659-fd849011e1dd"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("2e539179-3ad1-439d-80d3-9b87ba4ee992"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("37e160fa-a4ad-4071-9718-e8dbc41a2f09"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("3d3f1176-41f0-41e2-9c1d-d703e82b6fc3"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("5c07bb8f-cdf5-4ec0-93c0-2498cafec5e3"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("5c774716-7b07-448b-bbc0-ee81f15309a2"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("65bec600-7e73-4bf7-8a47-1f7df1018fc6"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("848ab340-54e2-4a17-9dad-2674b251a550"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("a3e8d6bd-75f8-42df-9b9d-b0ad5cdd8b49"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("c9cae1d7-0a77-49fc-8840-f78fbc5051af"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("f3772db5-f34a-41cd-abfc-22b05d3675e1"));

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("05af42ed-e910-47b9-a8be-9ed175f835a2"), "Mathematics", "Group E" },
                    { new Guid("1513132f-6ffd-4859-b791-50b28b05b879"), "Civil Engineering", "Group H" },
                    { new Guid("1c63770b-020a-47b5-a633-d75067b223a3"), "Chemistry", "Group F" },
                    { new Guid("488ec798-b1ae-40b5-a99b-7f1482a826d8"), "Information Technology", "Group J" },
                    { new Guid("4ddcf3ec-9826-49fb-ab15-49c6eade3fe3"), "Electrical Engineering", "Group B" },
                    { new Guid("88c74764-d46a-4e52-b677-9c799bdfda57"), "Mechanical Engineering", "Group C" },
                    { new Guid("8f91b42a-7c0a-4e53-a98d-1d6cbd657e10"), "Computer Science", "Group A" },
                    { new Guid("c2a85c30-aaca-4d7c-80aa-6346dc45fdf0"), "Environmental Science", "Group I" },
                    { new Guid("e022eb28-e79f-4106-8708-b5f0a298c0e4"), "Physics", "Group D" },
                    { new Guid("e7d10a8e-cef7-4d16-bee6-d76f7690971c"), "Biology", "Group G" },
                    { new Guid("f97f6197-1142-4867-bcec-3104f7f3b600"), "Aerospace Engineering", "Group K" }
                });
        }
    }
}

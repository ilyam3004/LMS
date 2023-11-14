using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSubjectFieldConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("1ee68487-561c-413a-bd9e-eb1b47b308d0"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("2c866674-5c28-49e6-9894-9c43ad33ef81"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("34df5e16-707d-4c48-8d79-c72634547ecc"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("53399b0d-6e71-4e56-a0e4-65224877880f"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("55e24f9c-18f4-4b94-b946-35ac8f71831c"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("70e1f044-98a1-4557-a964-f049773f79b8"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("a4204827-10cc-4104-99d0-bd0cdcaf14ff"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("ac86f4cd-f829-4fd6-a6f3-b33c3236776b"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("b02f971c-0604-48bd-8a5e-8e6b9c983437"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("bcf40c82-e978-4337-91e4-8e889a045180"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("f8c489f2-30ba-4c87-a2b1-9e746fa445cc"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subject",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Subject",
                type: "character varying(400)",
                maxLength: 400,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("22c9f609-4ccc-43fa-bf55-e44f533e2a7f"), "Environmental Science", "Group I" },
                    { new Guid("38d81154-add0-4672-b8aa-f79847057386"), "Electrical Engineering", "Group B" },
                    { new Guid("469e7a85-bf0e-45fe-bbc7-477e8146bdc7"), "Physics", "Group D" },
                    { new Guid("636ed61e-0393-432e-b311-3dca06ca40d6"), "Mathematics", "Group E" },
                    { new Guid("65fc595a-c6e1-4f6c-8a63-bee1de7d1167"), "Aerospace Engineering", "Group K" },
                    { new Guid("7e7872bc-2f9e-4360-b158-f60e32854ccb"), "Civil Engineering", "Group H" },
                    { new Guid("92674f21-92f5-414a-bbd7-df4435d5f96f"), "Biology", "Group G" },
                    { new Guid("b1d71b4b-d942-48c5-8132-5d717642334c"), "Mechanical Engineering", "Group C" },
                    { new Guid("c337f151-a387-4c47-875e-cb4d135ce746"), "Chemistry", "Group F" },
                    { new Guid("d26d6917-23b2-4b4f-9d10-5e3541e094cd"), "Computer Science", "Group A" },
                    { new Guid("ed211300-41fd-4513-8a04-98bb890c72a0"), "Information Technology", "Group J" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("22c9f609-4ccc-43fa-bf55-e44f533e2a7f"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("38d81154-add0-4672-b8aa-f79847057386"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("469e7a85-bf0e-45fe-bbc7-477e8146bdc7"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("636ed61e-0393-432e-b311-3dca06ca40d6"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("65fc595a-c6e1-4f6c-8a63-bee1de7d1167"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("7e7872bc-2f9e-4360-b158-f60e32854ccb"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("92674f21-92f5-414a-bbd7-df4435d5f96f"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("b1d71b4b-d942-48c5-8132-5d717642334c"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("c337f151-a387-4c47-875e-cb4d135ce746"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("d26d6917-23b2-4b4f-9d10-5e3541e094cd"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("ed211300-41fd-4513-8a04-98bb890c72a0"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subject",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Subject",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(400)",
                oldMaxLength: 400);

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("1ee68487-561c-413a-bd9e-eb1b47b308d0"), "Mechanical Engineering", "Group C" },
                    { new Guid("2c866674-5c28-49e6-9894-9c43ad33ef81"), "Environmental Science", "Group I" },
                    { new Guid("34df5e16-707d-4c48-8d79-c72634547ecc"), "Mathematics", "Group E" },
                    { new Guid("53399b0d-6e71-4e56-a0e4-65224877880f"), "Aerospace Engineering", "Group K" },
                    { new Guid("55e24f9c-18f4-4b94-b946-35ac8f71831c"), "Computer Science", "Group A" },
                    { new Guid("70e1f044-98a1-4557-a964-f049773f79b8"), "Civil Engineering", "Group H" },
                    { new Guid("a4204827-10cc-4104-99d0-bd0cdcaf14ff"), "Physics", "Group D" },
                    { new Guid("ac86f4cd-f829-4fd6-a6f3-b33c3236776b"), "Information Technology", "Group J" },
                    { new Guid("b02f971c-0604-48bd-8a5e-8e6b9c983437"), "Biology", "Group G" },
                    { new Guid("bcf40c82-e978-4337-91e4-8e889a045180"), "Chemistry", "Group F" },
                    { new Guid("f8c489f2-30ba-4c87-a2b1-9e746fa445cc"), "Electrical Engineering", "Group B" }
                });
        }
    }
}

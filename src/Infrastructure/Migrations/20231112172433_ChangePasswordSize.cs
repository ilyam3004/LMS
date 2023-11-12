using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangePasswordSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("02c79aa1-4c08-4714-8cfa-065615bf523a"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("306815ec-19f0-435a-9d78-221459590ac7"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("72e3c55f-1de1-4c51-b29d-c5f4a8dd62bd"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("84a3e8e3-ccd9-4c03-9a69-292bbb09ffd0"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("8a595e7e-7a77-4cb3-93d0-d00a5551176c"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("8e198820-4aa5-4285-a858-ccb48c4a7ccf"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("a10ed067-9239-44eb-9466-154b0b98aea4"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("ac918518-8ff0-40c4-8e61-8819101f1c29"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("c0179c37-618f-491a-b275-16d19cd85a15"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("d4c5c34a-1983-442c-9a6c-f7e90e2eac3d"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("dc669cea-fd57-4739-97d1-6d12299c9372"));

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("147de086-46ab-4137-b62d-2c811d04dcdc"), "Environmental Science", "Group I" },
                    { new Guid("19bb9322-311b-4fc2-9ad3-3dfe33599d6c"), "Computer Science", "Group A" },
                    { new Guid("58bf5cf6-71f8-4ce0-b620-e76b6632ce3f"), "Mathematics", "Group E" },
                    { new Guid("60255ebc-2e37-405d-af73-44572dcb9d87"), "Electrical Engineering", "Group B" },
                    { new Guid("6afa4354-1510-45fb-a8f4-f60b0dadb829"), "Chemistry", "Group F" },
                    { new Guid("71053a7d-7833-42a3-878c-1f51dec47e7b"), "Mechanical Engineering", "Group C" },
                    { new Guid("803c6305-3501-48a1-a3f6-54df0c9cb480"), "Physics", "Group D" },
                    { new Guid("9441bc7c-bb5c-4b1c-b0be-783893b3e855"), "Aerospace Engineering", "Group K" },
                    { new Guid("da519030-9eaf-429d-a83c-f95b3aedd5cf"), "Civil Engineering", "Group H" },
                    { new Guid("ee6ec3bd-6beb-496a-9d6d-03519bfebb9d"), "Biology", "Group G" },
                    { new Guid("fa8e4cec-8270-4920-94fd-807af7648b3d"), "Information Technology", "Group J" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("147de086-46ab-4137-b62d-2c811d04dcdc"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("19bb9322-311b-4fc2-9ad3-3dfe33599d6c"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("58bf5cf6-71f8-4ce0-b620-e76b6632ce3f"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("60255ebc-2e37-405d-af73-44572dcb9d87"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("6afa4354-1510-45fb-a8f4-f60b0dadb829"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("71053a7d-7833-42a3-878c-1f51dec47e7b"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("803c6305-3501-48a1-a3f6-54df0c9cb480"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("9441bc7c-bb5c-4b1c-b0be-783893b3e855"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("da519030-9eaf-429d-a83c-f95b3aedd5cf"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("ee6ec3bd-6beb-496a-9d6d-03519bfebb9d"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("fa8e4cec-8270-4920-94fd-807af7648b3d"));

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("02c79aa1-4c08-4714-8cfa-065615bf523a"), "Chemistry", "Group F" },
                    { new Guid("306815ec-19f0-435a-9d78-221459590ac7"), "Aerospace Engineering", "Group K" },
                    { new Guid("72e3c55f-1de1-4c51-b29d-c5f4a8dd62bd"), "Physics", "Group D" },
                    { new Guid("84a3e8e3-ccd9-4c03-9a69-292bbb09ffd0"), "Computer Science", "Group A" },
                    { new Guid("8a595e7e-7a77-4cb3-93d0-d00a5551176c"), "Information Technology", "Group J" },
                    { new Guid("8e198820-4aa5-4285-a858-ccb48c4a7ccf"), "Environmental Science", "Group I" },
                    { new Guid("a10ed067-9239-44eb-9466-154b0b98aea4"), "Mathematics", "Group E" },
                    { new Guid("ac918518-8ff0-40c4-8e61-8819101f1c29"), "Civil Engineering", "Group H" },
                    { new Guid("c0179c37-618f-491a-b275-16d19cd85a15"), "Biology", "Group G" },
                    { new Guid("d4c5c34a-1983-442c-9a6c-f7e90e2eac3d"), "Mechanical Engineering", "Group C" },
                    { new Guid("dc669cea-fd57-4739-97d1-6d12299c9372"), "Electrical Engineering", "Group B" }
                });
        }
    }
}

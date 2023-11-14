using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserSubjectRelationthisps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LecturerSubjects");

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

            migrationBuilder.AddColumn<Guid>(
                name: "LecturerId",
                table: "Subject",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Department", "Name" },
                values: new object[,]
                {
                    { new Guid("28455a1d-41fc-4ad4-9721-a4e3ffb604cd"), "Aerospace Engineering", "Group K" },
                    { new Guid("31daa61e-ba85-44c8-b3db-495b55472015"), "Computer Science", "Group A" },
                    { new Guid("5499b1e9-4b32-478e-96de-60bb2ef864a1"), "Environmental Science", "Group I" },
                    { new Guid("63f2d370-22d3-4a41-beee-520b645e6f45"), "Electrical Engineering", "Group B" },
                    { new Guid("a229deb6-c52e-4e2c-b523-76b5354df2a1"), "Information Technology", "Group J" },
                    { new Guid("d37c38f3-56d5-41e1-aa5b-2838fb8549da"), "Mathematics", "Group E" },
                    { new Guid("e18411c4-e0e2-4c77-8794-33548acbad8a"), "Civil Engineering", "Group H" },
                    { new Guid("e5c2f458-2a37-4b34-804a-ef4a46799560"), "Mechanical Engineering", "Group C" },
                    { new Guid("e8129daa-e023-46bd-ab6e-188caaf004b7"), "Chemistry", "Group F" },
                    { new Guid("f7902cb2-b999-4d45-aa26-ab574b40a339"), "Physics", "Group D" },
                    { new Guid("feb2b70a-578b-415a-80f4-a0fffa8e0e0a"), "Biology", "Group G" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subject_LecturerId",
                table: "Subject",
                column: "LecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Lecturers_LecturerId",
                table: "Subject",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "LecturerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Lecturers_LecturerId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_LecturerId",
                table: "Subject");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("28455a1d-41fc-4ad4-9721-a4e3ffb604cd"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("31daa61e-ba85-44c8-b3db-495b55472015"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("5499b1e9-4b32-478e-96de-60bb2ef864a1"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("63f2d370-22d3-4a41-beee-520b645e6f45"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("a229deb6-c52e-4e2c-b523-76b5354df2a1"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("d37c38f3-56d5-41e1-aa5b-2838fb8549da"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("e18411c4-e0e2-4c77-8794-33548acbad8a"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("e5c2f458-2a37-4b34-804a-ef4a46799560"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("e8129daa-e023-46bd-ab6e-188caaf004b7"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("f7902cb2-b999-4d45-aa26-ab574b40a339"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("feb2b70a-578b-415a-80f4-a0fffa8e0e0a"));

            migrationBuilder.DropColumn(
                name: "LecturerId",
                table: "Subject");

            migrationBuilder.CreateTable(
                name: "LecturerSubjects",
                columns: table => new
                {
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    LecturerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturerSubjects", x => new { x.SubjectId, x.LecturerId });
                    table.ForeignKey(
                        name: "FK_LecturerSubjects_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "LecturerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LecturerSubjects_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_LecturerSubjects_LecturerId",
                table: "LecturerSubjects",
                column: "LecturerId");
        }
    }
}

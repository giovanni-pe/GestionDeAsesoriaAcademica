using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResearchGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResearchLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ResearchGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearchLines_ResearchGroups_ResearchGroupId",
                        column: x => x.ResearchGroupId,
                        principalTable: "ResearchGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    LastLoggedinDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalendarTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalendarTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Professors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResearchGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCoordinator = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Professors_ResearchGroups_ResearchGroupId",
                        column: x => x.ResearchGroupId,
                        principalTable: "ResearchGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Professors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserCalendars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CalendarId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalendarName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IframeUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCalendars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCalendars_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdvisoryContracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfessorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResearchLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThesisTopic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfessorMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvisoryContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvisoryContracts_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvisoryContracts_ResearchLines_ResearchLineId",
                        column: x => x.ResearchLineId,
                        principalTable: "ResearchLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvisoryContracts_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfessorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CalendarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfessorProgress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentProgress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    GoogleEventId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ResearchGroups",
                columns: new[] { "Id", "Code", "Deleted", "Name" },
                values: new object[,]
                {
                    { new Guid("7e3892c0-9374-49fa-a3fd-53db637a40a5"), "SW123", false, "INGENIERIA DE SOFTWARE" },
                    { new Guid("7e3892c0-9374-49fa-a3fd-53db637a40a7"), "RESEGTI", false, "RESEGTI" }
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Deleted", "Name" },
                values: new object[] { new Guid("b542bf25-134c-47a2-a0df-84ed14d03c4a"), false, "Admin Tenant" });

            migrationBuilder.InsertData(
                table: "ResearchLines",
                columns: new[] { "Id", "Code", "Deleted", "Name", "ResearchGroupId" },
                values: new object[,]
                {
                    { new Guid("7e3892c0-9374-49fa-a3fd-53db637a4010"), "IoT", false, "Internet de las Cosas", new Guid("7e3892c0-9374-49fa-a3fd-53db637a40a7") },
                    { new Guid("7e3892c0-9374-49fa-a3fd-53db637a40a8"), "ASW123", false, "Arquitectura de Software", new Guid("7e3892c0-9374-49fa-a3fd-53db637a40a5") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Deleted", "Email", "FirstName", "LastLoggedinDate", "LastName", "Password", "Role", "Status", "TenantId" },
                values: new object[,]
                {
                    { new Guid("7e3892c0-9374-49fa-a3fd-53db637a40a1"), false, "ronald.ibarra@unas.edu.pe", "Ronald", null, "Ibarra Zapata", "$2a$12$Blal/uiFIJdYsCLTMUik/egLbfg3XhbnxBC6Sb5IKz2ZYhiU/MzL2", 1, 0, new Guid("b542bf25-134c-47a2-a0df-84ed14d03c4a") },
                    { new Guid("7e3892c0-9374-49fa-a3fd-53db637a40a2"), false, "gardyn.olivera@unas.edu.pe", "Gardyn", null, "Olivera Ruiz", "$2a$12$Blal/uiFIJdYsCLTMUik/egLbfg3XhbnxBC6Sb5IKz2ZYhiU/MzL2", 1, 0, new Guid("b542bf25-134c-47a2-a0df-84ed14d03c4a") },
                    { new Guid("7e3892c0-9374-49fa-a3fd-53db637a40a3"), false, "giovanni.perez@unas.edu.pe", "Giovanni", null, "Perez Espinoza", "$2a$12$Blal/uiFIJdYsCLTMUik/egLbfg3XhbnxBC6Sb5IKz2ZYhiU/MzL2", 1, 0, new Guid("b542bf25-134c-47a2-a0df-84ed14d03c4a") },
                    { new Guid("7e3892c0-9374-49fa-a3fd-53db637a40a4"), false, "luz.cabia@unas.edu.pe", "Luz Lisbeth", null, "Cabia Adriano", "$2a$12$Blal/uiFIJdYsCLTMUik/egLbfg3XhbnxBC6Sb5IKz2ZYhiU/MzL2", 1, 0, new Guid("b542bf25-134c-47a2-a0df-84ed14d03c4a") },
                    { new Guid("7e3892c0-9374-49fa-a3fd-53db637a40ae"), false, "admin@email.com", "Admin", null, "User", "$2a$12$Blal/uiFIJdYsCLTMUik/egLbfg3XhbnxBC6Sb5IKz2ZYhiU/MzL2", 0, 0, new Guid("b542bf25-134c-47a2-a0df-84ed14d03c4a") }
                });

            migrationBuilder.InsertData(
                table: "Professors",
                columns: new[] { "Id", "Deleted", "IsCoordinator", "ResearchGroupId", "UserId" },
                values: new object[,]
                {
                    { new Guid("7e3892c0-9374-49fa-a3fd-53db637a4011"), false, false, new Guid("7e3892c0-9374-49fa-a3fd-53db637a40a7"), new Guid("7e3892c0-9374-49fa-a3fd-53db637a40a2") },
                    { new Guid("7e3892c0-9374-49fa-a3fd-53db637a40b1"), false, false, new Guid("7e3892c0-9374-49fa-a3fd-53db637a40a5"), new Guid("7e3892c0-9374-49fa-a3fd-53db637a40a1") }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Code", "Deleted", "UserId" },
                values: new object[,]
                {
                    { new Guid("7e3892c0-9374-49fa-a3fd-53db637a40a1"), "0020210008", false, new Guid("7e3892c0-9374-49fa-a3fd-53db637a40a4") },
                    { new Guid("7e3892c0-9374-49fa-a3fd-53db637a40ad"), "0020210008", false, new Guid("7e3892c0-9374-49fa-a3fd-53db637a40a3") }
                });

            migrationBuilder.InsertData(
                table: "AdvisoryContracts",
                columns: new[] { "Id", "DateCreated", "Deleted", "Message", "ProfessorId", "ProfessorMessage", "ResearchLineId", "Status", "StudentId", "ThesisTopic" },
                values: new object[,]
                {
                    { new Guid("7e3892c0-9374-49fa-a3fd-53db637a4033"), new DateTime(2024, 8, 3, 4, 35, 29, 371, DateTimeKind.Local).AddTicks(5831), false, "Me dirijo a usted con el propósito de solicitar sasesoría para mi tesis de grado", new Guid("7e3892c0-9374-49fa-a3fd-53db637a4011"), "i", new Guid("7e3892c0-9374-49fa-a3fd-53db637a4010"), 1, new Guid("7e3892c0-9374-49fa-a3fd-53db637a40a1"), "Wifi 802.22, de alrgo alcance en zonas rurales" },
                    { new Guid("7e3892c0-9374-49fa-a3fd-53db637a4034"), new DateTime(2024, 8, 3, 4, 35, 29, 371, DateTimeKind.Local).AddTicks(5845), false, "Me dirijo a usted con el propósito de solicitar sasesoría para mi tesis de grado", new Guid("7e3892c0-9374-49fa-a3fd-53db637a4011"), "i", new Guid("7e3892c0-9374-49fa-a3fd-53db637a4010"), 0, new Guid("7e3892c0-9374-49fa-a3fd-53db637a40ad"), "Sensores Iot y sus aplicaciones en la agricultura" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvisoryContracts_ProfessorId",
                table: "AdvisoryContracts",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvisoryContracts_ResearchLineId",
                table: "AdvisoryContracts",
                column: "ResearchLineId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvisoryContracts_StudentId",
                table: "AdvisoryContracts",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ProfessorId",
                table: "Appointments",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_StudentId",
                table: "Appointments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarTokens_UserId",
                table: "CalendarTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Professors_ResearchGroupId",
                table: "Professors",
                column: "ResearchGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Professors_UserId",
                table: "Professors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchLines_ResearchGroupId",
                table: "ResearchLines",
                column: "ResearchGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCalendars_UserId",
                table: "UserCalendars",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TenantId",
                table: "Users",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvisoryContracts");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "CalendarTokens");

            migrationBuilder.DropTable(
                name: "UserCalendars");

            migrationBuilder.DropTable(
                name: "ResearchLines");

            migrationBuilder.DropTable(
                name: "Professors");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "ResearchGroups");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}

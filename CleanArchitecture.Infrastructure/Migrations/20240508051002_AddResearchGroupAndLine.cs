using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddResearchGroupAndLine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "ResearchLines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "ResearchGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "ResearchGroups",
                keyColumn: "Id",
                keyValue: new Guid("b542bf25-134c-47a2-a0df-84ed14d03c41"),
                column: "Code",
                value: "SW123");

            migrationBuilder.UpdateData(
                table: "ResearchLines",
                keyColumn: "Id",
                keyValue: new Guid("b542bf25-134c-47a2-a0df-84ed14d03c42"),
                column: "Code",
                value: "ASW123");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "ResearchLines");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "ResearchGroups");
        }
    }
}

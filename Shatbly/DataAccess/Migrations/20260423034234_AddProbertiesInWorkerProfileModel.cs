using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shatbly.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddProbertiesInWorkerProfileModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CVPath",
                table: "WorkerProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HRNotes",
                table: "WorkerProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InterviewDate",
                table: "WorkerProfiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "WorkerProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVPath",
                table: "WorkerProfiles");

            migrationBuilder.DropColumn(
                name: "HRNotes",
                table: "WorkerProfiles");

            migrationBuilder.DropColumn(
                name: "InterviewDate",
                table: "WorkerProfiles");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "WorkerProfiles");
        }
    }
}

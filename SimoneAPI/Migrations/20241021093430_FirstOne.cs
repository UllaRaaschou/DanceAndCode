using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SimoneAPI.Migrations
{
    /// <inheritdoc />
    public partial class FirstOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DancerDataModels",
                columns: table => new
                {
                    DancerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    TimeOfBirth = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DancerDataModels", x => x.DancerId);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    StaffId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.StaffId);
                });

            migrationBuilder.CreateTable(
                name: "TeamDataModels",
                columns: table => new
                {
                    TeamId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SceduledTime = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamDataModels", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "WorkingHours",
                columns: table => new
                {
                    WorkingHoursId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StaffId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    ChosenValueOfWorkingHours = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHours", x => x.WorkingHoursId);
                    table.ForeignKey(
                        name: "FK_WorkingHours_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamDancerRelations",
                columns: table => new
                {
                    TeamDancerRelationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TeamId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DancerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsTrialLesson = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamDancerRelations", x => x.TeamDancerRelationId);
                    table.ForeignKey(
                        name: "FK_TeamDancerRelations_DancerDataModels_DancerId",
                        column: x => x.DancerId,
                        principalTable: "DancerDataModels",
                        principalColumn: "DancerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamDancerRelations_TeamDataModels_TeamId",
                        column: x => x.TeamId,
                        principalTable: "TeamDataModels",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    AttendanceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TeamDancerRelationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsPresent = table.Column<bool>(type: "INTEGER", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.AttendanceId);
                    table.ForeignKey(
                        name: "FK_Attendances_TeamDancerRelations_TeamDancerRelationId",
                        column: x => x.TeamDancerRelationId,
                        principalTable: "TeamDancerRelations",
                        principalColumn: "TeamDancerRelationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DancerDataModels",
                columns: new[] { "DancerId", "Name", "TimeOfBirth" },
                values: new object[,]
                {
                    { new Guid("c5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"), "Petra", new DateOnly(2013, 1, 1) },
                    { new Guid("d5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"), "Silje", new DateOnly(2014, 1, 1) }
                });

            migrationBuilder.InsertData(
                table: "TeamDataModels",
                columns: new[] { "TeamId", "Name", "Number", "SceduledTime" },
                values: new object[,]
                {
                    { new Guid("a5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"), "Hiphop1", 1, "Mandag 16:00 - 16:45" },
                    { new Guid("b5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"), "MGP", 2, "Tirsdag 15:15 - 16:00" }
                });

            migrationBuilder.InsertData(
                table: "TeamDancerRelations",
                columns: new[] { "TeamDancerRelationId", "DancerId", "IsTrialLesson", "TeamId" },
                values: new object[,]
                {
                    { new Guid("06e58b50-c32b-4a29-b6d3-0d0691c52c3a"), new Guid("d5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"), false, new Guid("b5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48") },
                    { new Guid("36b66eb1-41e6-430d-9750-e251c764ce60"), new Guid("c5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"), false, new Guid("a5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48") },
                    { new Guid("c73693a0-78d8-4def-9866-9fbab844f016"), new Guid("d5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"), false, new Guid("a5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_TeamDancerRelationId",
                table: "Attendances",
                column: "TeamDancerRelationId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamDancerRelations_DancerId",
                table: "TeamDancerRelations",
                column: "DancerId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamDancerRelations_TeamId",
                table: "TeamDancerRelations",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHours_StaffId",
                table: "WorkingHours",
                column: "StaffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "WorkingHours");

            migrationBuilder.DropTable(
                name: "TeamDancerRelations");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "DancerDataModels");

            migrationBuilder.DropTable(
                name: "TeamDataModels");
        }
    }
}

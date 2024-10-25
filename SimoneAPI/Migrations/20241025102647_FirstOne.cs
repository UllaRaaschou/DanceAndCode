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
                    ScheduledTime = table.Column<string>(type: "TEXT", nullable: false)
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
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Mathias", new DateOnly(2012, 5, 12) },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Emma", new DateOnly(2011, 8, 23) },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Sofie", new DateOnly(2013, 11, 19) },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "Lucas", new DateOnly(2012, 3, 15) },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "Ida", new DateOnly(2014, 6, 29) },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "Oliver", new DateOnly(2010, 4, 20) },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "Maja", new DateOnly(2013, 10, 2) },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "Marie", new DateOnly(2012, 7, 25) },
                    { new Guid("00000000-0000-0000-0000-000000000011"), "Sebastian", new DateOnly(2014, 5, 5) },
                    { new Guid("00000000-0000-0000-0000-000000000012"), "Nora", new DateOnly(2013, 12, 1) },
                    { new Guid("00000000-0000-0000-0000-000000000013"), "Oscar", new DateOnly(2010, 1, 17) },
                    { new Guid("00000000-0000-0000-0000-000000000014"), "Clara", new DateOnly(2012, 2, 6) },
                    { new Guid("00000000-0000-0000-0000-000000000015"), "Magnus", new DateOnly(2011, 8, 30) },
                    { new Guid("00000000-0000-0000-0000-000000000016"), "Signe", new DateOnly(2013, 3, 18) },
                    { new Guid("00000000-0000-0000-0000-000000000017"), "Thea", new DateOnly(2014, 9, 12) },
                    { new Guid("00000000-0000-0000-0000-000000000018"), "Emil", new DateOnly(2010, 5, 14) },
                    { new Guid("00000000-0000-0000-0000-000000000019"), "Freya", new DateOnly(2013, 4, 22) },
                    { new Guid("00000000-0000-0000-0000-000000000020"), "Mikkel", new DateOnly(2012, 11, 8) },
                    { new Guid("00000000-0000-0000-0000-000000000021"), "Liv", new DateOnly(2011, 7, 30) },
                    { new Guid("00000000-0000-0000-0000-000000000022"), "Søren", new DateOnly(2010, 2, 19) },
                    { new Guid("00000000-0000-0000-0000-000000000023"), "Sara", new DateOnly(2013, 6, 4) },
                    { new Guid("00000000-0000-0000-0000-000000000024"), "Joakim", new DateOnly(2012, 3, 26) },
                    { new Guid("00000000-0000-0000-0000-000000000025"), "Tilde", new DateOnly(2014, 10, 30) },
                    { new Guid("00000000-0000-0000-0000-000000000026"), "Mikkel", new DateOnly(2012, 11, 8) },
                    { new Guid("00000000-0000-0000-0000-000000000027"), "Liv", new DateOnly(2011, 7, 30) },
                    { new Guid("00000000-0000-0000-0000-000000000028"), "Søren", new DateOnly(2010, 2, 19) },
                    { new Guid("00000000-0000-0000-0000-000000000029"), "Sara", new DateOnly(2013, 6, 4) },
                    { new Guid("00000000-0000-0000-0000-000000000030"), "Joakim", new DateOnly(2012, 3, 26) },
                    { new Guid("00000000-0000-0000-0000-000000000031"), "Tilde", new DateOnly(2014, 10, 30) },
                    { new Guid("00000000-1000-0000-0000-000000000000"), "Frederik", new DateOnly(2011, 9, 9) }
                });

            migrationBuilder.InsertData(
                table: "TeamDataModels",
                columns: new[] { "TeamId", "Name", "Number", "ScheduledTime" },
                values: new object[,]
                {
                    { new Guid("07000000-0000-0000-0000-000000000000"), "Showdance 3", 10, "Torsdag 17:15 - 18:00" },
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Hiphop1", 1, "Mandag 16:00 - 16:45" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "MGP", 2, "Tirsdag 15:15 - 16:00" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Ballet", 3, "Onsdag 17:00 - 17:45" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Hiphop2", 4, "Torsdag 16:00 - 16:45" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Streetdance", 5, "Fredag 15:00 - 15:45" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Argentinsk Tango", 6, "Lørdag 11:00 - 11:45" },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "Salsa", 7, "Mandag 18:00 - 18:45" },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "Showdance 1", 8, "Tirsdag 17:00 - 17:45" },
                    { new Guid("99999999-9999-9999-9999-999999999999"), "Showdance 2", 9, "Onsdag 18:00 - 18:45" }
                });

            migrationBuilder.InsertData(
                table: "TeamDancerRelations",
                columns: new[] { "TeamDancerRelationId", "DancerId", "IsTrialLesson", "TeamId" },
                values: new object[,]
                {
                    { new Guid("10111111-1111-1111-1111-111111111110"), new Guid("00000000-0000-0000-0000-000000000018"), false, new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("11111111-1111-1111-1111-111111111100"), new Guid("00000000-0000-0000-0000-000000000003"), false, new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("11111111-1111-1111-1111-111111111101"), new Guid("00000000-0000-0000-0000-000000000004"), false, new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("11111111-1111-1111-1111-111111111102"), new Guid("00000000-0000-0000-0000-000000000005"), false, new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("11111111-1111-1111-1111-111111111103"), new Guid("00000000-0000-0000-0000-000000000006"), false, new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("11111111-1111-1111-1111-111111111104"), new Guid("00000000-0000-0000-0000-000000000007"), false, new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("11111111-1111-1111-1111-111111111105"), new Guid("00000000-0000-0000-0000-000000000008"), false, new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("11111111-1111-1111-1111-111111111106"), new Guid("00000000-0000-0000-0000-000000000009"), false, new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("11111111-1111-1111-1111-111111111107"), new Guid("00000000-0000-0000-0000-000000000010"), false, new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("11111111-1111-1111-1111-111111111108"), new Guid("00000000-0000-0000-0000-000000000011"), false, new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("11111111-1111-1111-1111-111111111109"), new Guid("00000000-0000-0000-0000-000000000012"), false, new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("11111111-1111-1111-1111-111111111110"), new Guid("00000000-0000-0000-0000-000000000013"), false, new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000014"), false, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("11111111-1111-1111-1111-111111111112"), new Guid("00000000-0000-0000-0000-000000000015"), false, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("11111111-1111-1111-1111-111111111113"), new Guid("00000000-0000-0000-0000-000000000016"), false, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("11111111-1111-1111-1111-111111111114"), new Guid("00000000-0000-0000-0000-000000000017"), false, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("11111111-1111-1111-1111-111111111115"), new Guid("00000000-0000-0000-0000-000000000018"), false, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("11111111-1111-1111-1111-111111111116"), new Guid("00000000-0000-0000-0000-000000000019"), false, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("11111111-1111-1111-1111-111111111117"), new Guid("00000000-0000-0000-0000-000000000020"), false, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("11111111-1111-1111-1111-111111111118"), new Guid("00000000-0000-0000-0000-000000000021"), false, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("11111111-1111-1111-1111-111111111119"), new Guid("00000000-0000-0000-0000-000000000022"), false, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("11111111-1111-1111-1111-111111111120"), new Guid("00000000-0000-0000-0000-000000000023"), false, new Guid("77777777-7777-7777-7777-777777777777") },
                    { new Guid("11111111-1111-1111-1111-111111111121"), new Guid("00000000-0000-0000-0000-000000000024"), false, new Guid("77777777-7777-7777-7777-777777777777") },
                    { new Guid("11111111-1111-1111-1111-111111111122"), new Guid("00000000-0000-0000-0000-000000000025"), false, new Guid("77777777-7777-7777-7777-777777777777") },
                    { new Guid("11111111-1111-1111-1111-111111111123"), new Guid("00000000-0000-0000-0000-000000000026"), false, new Guid("88888888-8888-8888-8888-888888888888") },
                    { new Guid("11111111-1111-1111-1111-111111111124"), new Guid("00000000-0000-0000-0000-000000000027"), false, new Guid("88888888-8888-8888-8888-888888888888") },
                    { new Guid("11111111-1111-1111-1111-111111111125"), new Guid("00000000-0000-0000-0000-000000000028"), false, new Guid("88888888-8888-8888-8888-888888888888") },
                    { new Guid("11111111-1111-1111-1111-111111111126"), new Guid("00000000-0000-0000-0000-000000000029"), false, new Guid("99999999-9999-9999-9999-999999999999") },
                    { new Guid("11111111-1111-1111-1111-111111111127"), new Guid("00000000-0000-0000-0000-000000000030"), false, new Guid("99999999-9999-9999-9999-999999999999") },
                    { new Guid("11111111-1111-1111-1111-111111111128"), new Guid("00000000-0000-0000-0000-000000000031"), false, new Guid("99999999-9999-9999-9999-999999999999") },
                    { new Guid("11111111-1111-1111-1111-711111111101"), new Guid("00000000-0000-0000-0000-000000000007"), false, new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("13111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000025"), false, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("14111111-1111-1111-1111-111111111112"), new Guid("00000000-0000-0000-0000-000000000030"), false, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("15111111-1111-1111-1111-111111111113"), new Guid("00000000-0000-0000-0000-000000000028"), false, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("16111111-1111-1111-1111-111111111114"), new Guid("00000000-0000-0000-0000-000000000029"), false, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("17111111-1111-1111-1111-111111111115"), new Guid("00000000-0000-0000-0000-000000000023"), false, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("18111111-1111-1111-1111-111111111116"), new Guid("00000000-0000-0000-0000-000000000014"), false, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("19111111-1111-1111-1111-111111111117"), new Guid("00000000-0000-0000-0000-000000000027"), false, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("20111111-1111-1111-1111-111111111118"), new Guid("00000000-0000-0000-0000-000000000027"), false, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("21111111-1111-1111-1111-111111111102"), new Guid("00000000-0000-0000-0000-000000000009"), false, new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("21111111-1111-1111-1111-111111111119"), new Guid("00000000-0000-0000-0000-000000000021"), false, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("21111111-1111-1111-1111-111111111120"), new Guid("00000000-0000-0000-0000-000000000029"), false, new Guid("77777777-7777-7777-7777-777777777777") },
                    { new Guid("21111111-1111-1111-1111-111111111121"), new Guid("00000000-0000-0000-0000-000000000023"), false, new Guid("77777777-7777-7777-7777-777777777777") },
                    { new Guid("21111111-1111-1111-1111-111111111122"), new Guid("00000000-0000-0000-0000-000000000014"), false, new Guid("77777777-7777-7777-7777-777777777777") },
                    { new Guid("21111111-1111-1111-1111-111111111123"), new Guid("00000000-0000-0000-0000-000000000009"), false, new Guid("88888888-8888-8888-8888-888888888888") },
                    { new Guid("21111111-1111-1111-1111-111111111124"), new Guid("00000000-0000-0000-0000-000000000008"), false, new Guid("88888888-8888-8888-8888-888888888888") },
                    { new Guid("21111111-1111-1111-1111-111111111125"), new Guid("00000000-0000-0000-0000-000000000004"), false, new Guid("88888888-8888-8888-8888-888888888888") },
                    { new Guid("21111111-1111-1111-1111-111111111126"), new Guid("00000000-0000-0000-0000-000000000006"), false, new Guid("99999999-9999-9999-9999-999999999999") },
                    { new Guid("21111111-1111-1111-1111-111111111127"), new Guid("00000000-0000-0000-0000-000000000025"), false, new Guid("99999999-9999-9999-9999-999999999999") },
                    { new Guid("21111111-1111-1111-1111-111111111128"), new Guid("00000000-0000-0000-0000-000000000018"), false, new Guid("99999999-9999-9999-9999-999999999999") },
                    { new Guid("31111111-1111-1111-1111-111111111103"), new Guid("00000000-0000-0000-0000-000000000022"), false, new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("41111111-1111-1111-1111-111111111104"), new Guid("00000000-0000-0000-0000-000000000011"), false, new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("51111111-1111-1111-1111-111111111105"), new Guid("00000000-0000-0000-0000-000000000016"), false, new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("61111111-1111-1111-1111-111111111106"), new Guid("00000000-0000-0000-0000-000000000022"), false, new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("71111111-1111-1111-1111-111111111107"), new Guid("00000000-0000-0000-0000-000000000015"), false, new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("81111111-1111-1111-1111-111111111108"), new Guid("00000000-0000-0000-0000-000000000018"), false, new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("91111111-1111-1111-1111-111111111109"), new Guid("00000000-0000-0000-0000-000000000017"), false, new Guid("33333333-3333-3333-3333-333333333333") }
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

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
                name: "CalendarDataModels",
                columns: table => new
                {
                    CalendarId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SummerHolidayStart = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    SummerHolidayEnd = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    AutumnHolidayStart = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    AutumnHolidayEnd = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    ChristmasHolidayStart = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    ChristmasHolidayEnd = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    WintherHolidayStart = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    WintherHolidayEnd = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    EasterHolidayStart = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    EasterHolidayEnd = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    ChristmasShow = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    RecitalShow = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarDataModels", x => x.CalendarId);
                });

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
                    TimeOfBirth = table.Column<DateOnly>(type: "TEXT", nullable: false)
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
                    ScheduledTime = table.Column<string>(type: "TEXT", nullable: false),
                    DayOfWeek = table.Column<int>(type: "INTEGER", nullable: false)
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
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Loen1 = table.Column<double>(type: "REAL", nullable: false),
                    Loen2 = table.Column<double>(type: "REAL", nullable: false),
                    Loen3 = table.Column<double>(type: "REAL", nullable: false),
                    Loen4 = table.Column<double>(type: "REAL", nullable: false),
                    IsVikar = table.Column<bool>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false)
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
                    TeamId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DancerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsTrialLesson = table.Column<bool>(type: "INTEGER", nullable: false),
                    LastDanceDate = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamDancerRelations", x => new { x.DancerId, x.TeamId });
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
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    IsPresent = table.Column<bool>(type: "INTEGER", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: false),
                    DancerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TeamId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.AttendanceId);
                    table.ForeignKey(
                        name: "FK_Attendances_TeamDancerRelations_DancerId_TeamId",
                        columns: x => new { x.DancerId, x.TeamId },
                        principalTable: "TeamDancerRelations",
                        principalColumns: new[] { "DancerId", "TeamId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CalendarDataModels",
                columns: new[] { "CalendarId", "AutumnHolidayEnd", "AutumnHolidayStart", "ChristmasHolidayEnd", "ChristmasHolidayStart", "ChristmasShow", "CreatedDate", "EasterHolidayEnd", "EasterHolidayStart", "RecitalShow", "SummerHolidayEnd", "SummerHolidayStart", "WintherHolidayEnd", "WintherHolidayStart" },
                values: new object[] { new Guid("00000000-1111-0000-0000-000000000000"), new DateOnly(2024, 10, 21), new DateOnly(2024, 10, 17), new DateOnly(2025, 1, 2), new DateOnly(2024, 12, 23), new DateOnly(2024, 12, 10), new DateTime(2024, 11, 28, 13, 17, 6, 237, DateTimeKind.Utc).AddTicks(4453), new DateOnly(2025, 4, 17), new DateOnly(2025, 4, 10), new DateOnly(2025, 6, 10), new DateOnly(2024, 8, 7), new DateOnly(2024, 6, 27), new DateOnly(2025, 2, 17), new DateOnly(2025, 2, 13) });

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
                table: "Staffs",
                columns: new[] { "StaffId", "Name", "Role", "TimeOfBirth" },
                values: new object[] { new Guid("d7a499eb-65d8-4a62-bdd2-91c65e45e89c"), "John Ding", 1, new DateOnly(1980, 1, 1) });

            migrationBuilder.InsertData(
                table: "TeamDataModels",
                columns: new[] { "TeamId", "DayOfWeek", "Name", "Number", "ScheduledTime" },
                values: new object[,]
                {
                    { new Guid("07000000-0000-0000-0000-000000000000"), 4, "Showdance 3", 10, "Torsdag 17:15 - 18:00" },
                    { new Guid("11111111-1111-1111-1111-111111111111"), 1, "Hiphop1", 1, "Mandag 16:00 - 16:45" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 2, "MGP", 2, "Tirsdag 15:15 - 16:00" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 3, "Ballet", 3, "Onsdag 17:00 - 17:45" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), 4, "Hiphop2", 4, "Torsdag 16:00 - 16:45" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), 5, "Streetdance", 5, "Fredag 15:00 - 15:45" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), 6, "Argentinsk Tango", 6, "Lørdag 11:00 - 11:45" },
                    { new Guid("77777777-7777-7777-7777-777777777777"), 1, "Salsa", 7, "Mandag 18:00 - 18:45" },
                    { new Guid("88888888-8888-8888-8888-888888888888"), 2, "Showdance 1", 8, "Tirsdag 17:00 - 17:45" },
                    { new Guid("99999999-9999-9999-9999-999999999999"), 3, "Showdance 2", 9, "Onsdag 18:00 - 18:45" }
                });

            migrationBuilder.InsertData(
                table: "TeamDancerRelations",
                columns: new[] { "DancerId", "TeamId", "IsTrialLesson", "LastDanceDate" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("11111111-1111-1111-1111-111111111111"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("88888888-8888-8888-8888-888888888888"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("11111111-1111-1111-1111-111111111111"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("11111111-1111-1111-1111-111111111111"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("99999999-9999-9999-9999-999999999999"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("11111111-1111-1111-1111-111111111111"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("22222222-2222-2222-2222-222222222222"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("88888888-8888-8888-8888-888888888888"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new Guid("11111111-1111-1111-1111-111111111111"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new Guid("22222222-2222-2222-2222-222222222222"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new Guid("88888888-8888-8888-8888-888888888888"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000010"), new Guid("22222222-2222-2222-2222-222222222222"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000011"), new Guid("33333333-3333-3333-3333-333333333333"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000012"), new Guid("33333333-3333-3333-3333-333333333333"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000013"), new Guid("33333333-3333-3333-3333-333333333333"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000014"), new Guid("44444444-4444-4444-4444-444444444444"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000014"), new Guid("55555555-5555-5555-5555-555555555555"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000014"), new Guid("77777777-7777-7777-7777-777777777777"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000015"), new Guid("22222222-2222-2222-2222-222222222222"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000015"), new Guid("44444444-4444-4444-4444-444444444444"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000016"), new Guid("22222222-2222-2222-2222-222222222222"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000016"), new Guid("44444444-4444-4444-4444-444444444444"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000017"), new Guid("33333333-3333-3333-3333-333333333333"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000017"), new Guid("55555555-5555-5555-5555-555555555555"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000018"), new Guid("33333333-3333-3333-3333-333333333333"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000018"), new Guid("55555555-5555-5555-5555-555555555555"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000018"), new Guid("99999999-9999-9999-9999-999999999999"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000019"), new Guid("55555555-5555-5555-5555-555555555555"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000020"), new Guid("66666666-6666-6666-6666-666666666666"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000021"), new Guid("66666666-6666-6666-6666-666666666666"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000022"), new Guid("11111111-1111-1111-1111-111111111111"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000022"), new Guid("22222222-2222-2222-2222-222222222222"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000022"), new Guid("66666666-6666-6666-6666-666666666666"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000023"), new Guid("55555555-5555-5555-5555-555555555555"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000023"), new Guid("77777777-7777-7777-7777-777777777777"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000024"), new Guid("77777777-7777-7777-7777-777777777777"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000025"), new Guid("44444444-4444-4444-4444-444444444444"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000025"), new Guid("77777777-7777-7777-7777-777777777777"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000025"), new Guid("99999999-9999-9999-9999-999999999999"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000026"), new Guid("88888888-8888-8888-8888-888888888888"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000027"), new Guid("66666666-6666-6666-6666-666666666666"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000027"), new Guid("88888888-8888-8888-8888-888888888888"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000028"), new Guid("44444444-4444-4444-4444-444444444444"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000028"), new Guid("88888888-8888-8888-8888-888888888888"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000029"), new Guid("55555555-5555-5555-5555-555555555555"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000029"), new Guid("77777777-7777-7777-7777-777777777777"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000029"), new Guid("99999999-9999-9999-9999-999999999999"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000030"), new Guid("44444444-4444-4444-4444-444444444444"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000030"), new Guid("99999999-9999-9999-9999-999999999999"), false, new DateOnly(2025, 6, 10) },
                    { new Guid("00000000-0000-0000-0000-000000000031"), new Guid("99999999-9999-9999-9999-999999999999"), false, new DateOnly(2025, 6, 10) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_DancerId_TeamId",
                table: "Attendances",
                columns: new[] { "DancerId", "TeamId" });

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
                name: "CalendarDataModels");

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

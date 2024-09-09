using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SimoneAPI.Migrations
{
    /// <inheritdoc />
    public partial class firstOne : Migration
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
                    TimeOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DancerDataModels", x => x.DancerId);
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
                name: "TeamDancerRelations",
                columns: table => new
                {
                    TeamDancerRelationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TeamId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DancerId = table.Column<Guid>(type: "TEXT", nullable: false)
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

            migrationBuilder.InsertData(
                table: "DancerDataModels",
                columns: new[] { "DancerId", "Name", "TimeOfBirth" },
                values: new object[,]
                {
                    { new Guid("c5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"), "Petra", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"), "Silje", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
                columns: new[] { "TeamDancerRelationId", "DancerId", "TeamId" },
                values: new object[,]
                {
                    { new Guid("0b7681c5-6d18-440e-a578-f00c5573e553"), new Guid("c5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"), new Guid("a5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48") },
                    { new Guid("8c78c350-a137-48b2-ba04-e16cf29a718b"), new Guid("d5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"), new Guid("a5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48") },
                    { new Guid("e6b822b6-318d-406f-bd28-d30c4cddf6ab"), new Guid("d5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"), new Guid("b5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamDancerRelations_DancerId",
                table: "TeamDancerRelations",
                column: "DancerId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamDancerRelations_TeamId",
                table: "TeamDancerRelations",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamDancerRelations");

            migrationBuilder.DropTable(
                name: "DancerDataModels");

            migrationBuilder.DropTable(
                name: "TeamDataModels");
        }
    }
}

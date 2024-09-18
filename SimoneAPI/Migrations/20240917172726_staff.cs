using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SimoneAPI.Migrations
{
    /// <inheritdoc />
    public partial class staff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegisteredLesson_Teachers_StaffId",
                table: "RegisteredLesson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers");

            migrationBuilder.DeleteData(
                table: "TeamDancerRelations",
                keyColumn: "TeamDancerRelationId",
                keyValue: new Guid("20cbc2f7-68ed-4a0c-a401-08b4609b36cc"));

            migrationBuilder.DeleteData(
                table: "TeamDancerRelations",
                keyColumn: "TeamDancerRelationId",
                keyValue: new Guid("471b78d5-dd53-4fa9-bcb1-d7f51f2c165d"));

            migrationBuilder.DeleteData(
                table: "TeamDancerRelations",
                keyColumn: "TeamDancerRelationId",
                keyValue: new Guid("e9fb5d41-506f-40ab-8f3d-79c1d968782a"));

            migrationBuilder.RenameTable(
                name: "Teachers",
                newName: "Staffs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staffs",
                table: "Staffs",
                column: "StaffId");

            migrationBuilder.InsertData(
                table: "TeamDancerRelations",
                columns: new[] { "TeamDancerRelationId", "DancerId", "IsTrialLesson", "TeamId" },
                values: new object[,]
                {
                    { new Guid("3b6e0bc7-93bf-4046-a7e1-37031866cdc4"), new Guid("c5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"), false, new Guid("a5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48") },
                    { new Guid("4bfa37e8-2250-43bc-a39e-2669f9a8c1f8"), new Guid("d5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"), false, new Guid("b5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48") },
                    { new Guid("bf26d228-170b-466e-a573-da863d2b8a31"), new Guid("d5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"), false, new Guid("a5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RegisteredLesson_Staffs_StaffId",
                table: "RegisteredLesson",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "StaffId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegisteredLesson_Staffs_StaffId",
                table: "RegisteredLesson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staffs",
                table: "Staffs");

            migrationBuilder.DeleteData(
                table: "TeamDancerRelations",
                keyColumn: "TeamDancerRelationId",
                keyValue: new Guid("3b6e0bc7-93bf-4046-a7e1-37031866cdc4"));

            migrationBuilder.DeleteData(
                table: "TeamDancerRelations",
                keyColumn: "TeamDancerRelationId",
                keyValue: new Guid("4bfa37e8-2250-43bc-a39e-2669f9a8c1f8"));

            migrationBuilder.DeleteData(
                table: "TeamDancerRelations",
                keyColumn: "TeamDancerRelationId",
                keyValue: new Guid("bf26d228-170b-466e-a573-da863d2b8a31"));

            migrationBuilder.RenameTable(
                name: "Staffs",
                newName: "Teachers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "StaffId");

            migrationBuilder.InsertData(
                table: "TeamDancerRelations",
                columns: new[] { "TeamDancerRelationId", "DancerId", "IsTrialLesson", "TeamId" },
                values: new object[,]
                {
                    { new Guid("20cbc2f7-68ed-4a0c-a401-08b4609b36cc"), new Guid("d5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"), false, new Guid("b5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48") },
                    { new Guid("471b78d5-dd53-4fa9-bcb1-d7f51f2c165d"), new Guid("d5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"), false, new Guid("a5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48") },
                    { new Guid("e9fb5d41-506f-40ab-8f3d-79c1d968782a"), new Guid("c5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"), false, new Guid("a5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RegisteredLesson_Teachers_StaffId",
                table: "RegisteredLesson",
                column: "StaffId",
                principalTable: "Teachers",
                principalColumn: "StaffId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

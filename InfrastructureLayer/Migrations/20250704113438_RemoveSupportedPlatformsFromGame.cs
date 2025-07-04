using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayer.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSupportedPlatformsFromGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Platforms_SupportedPlatformsId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_SupportedPlatformsId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "SupportedPlatformsId",
                table: "Games");

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 4, 11, 34, 38, 323, DateTimeKind.Utc).AddTicks(107));

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 4, 11, 34, 38, 323, DateTimeKind.Utc).AddTicks(109));

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 4, 11, 34, 38, 323, DateTimeKind.Utc).AddTicks(110));

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 4, 11, 34, 38, 323, DateTimeKind.Utc).AddTicks(112));

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 4, 11, 34, 38, 323, DateTimeKind.Utc).AddTicks(113));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 4, 11, 34, 38, 322, DateTimeKind.Utc).AddTicks(9972));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 4, 11, 34, 38, 322, DateTimeKind.Utc).AddTicks(9974));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 4, 11, 34, 38, 322, DateTimeKind.Utc).AddTicks(9975));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 4, 11, 34, 38, 322, DateTimeKind.Utc).AddTicks(9976));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 4, 11, 34, 38, 322, DateTimeKind.Utc).AddTicks(9977));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 4, 11, 34, 38, 322, DateTimeKind.Utc).AddTicks(9978));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 4, 11, 34, 38, 322, DateTimeKind.Utc).AddTicks(9979));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 4, 11, 34, 38, 322, DateTimeKind.Utc).AddTicks(9979));

            migrationBuilder.UpdateData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 4, 11, 34, 38, 323, DateTimeKind.Utc).AddTicks(74));

            migrationBuilder.UpdateData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 4, 11, 34, 38, 323, DateTimeKind.Utc).AddTicks(75));

            migrationBuilder.UpdateData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 4, 11, 34, 38, 323, DateTimeKind.Utc).AddTicks(76));

            migrationBuilder.UpdateData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 4, 11, 34, 38, 323, DateTimeKind.Utc).AddTicks(77));

            migrationBuilder.UpdateData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 4, 11, 34, 38, 323, DateTimeKind.Utc).AddTicks(78));

            migrationBuilder.UpdateData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 4, 11, 34, 38, 323, DateTimeKind.Utc).AddTicks(79));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupportedPlatformsId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 1, 16, 28, 1, 237, DateTimeKind.Utc).AddTicks(7646));

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 1, 16, 28, 1, 237, DateTimeKind.Utc).AddTicks(7647));

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 1, 16, 28, 1, 237, DateTimeKind.Utc).AddTicks(7648));

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 1, 16, 28, 1, 237, DateTimeKind.Utc).AddTicks(7649));

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 1, 16, 28, 1, 237, DateTimeKind.Utc).AddTicks(7650));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 1, 16, 28, 1, 237, DateTimeKind.Utc).AddTicks(7494));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 1, 16, 28, 1, 237, DateTimeKind.Utc).AddTicks(7496));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 1, 16, 28, 1, 237, DateTimeKind.Utc).AddTicks(7497));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 1, 16, 28, 1, 237, DateTimeKind.Utc).AddTicks(7498));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 1, 16, 28, 1, 237, DateTimeKind.Utc).AddTicks(7499));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 1, 16, 28, 1, 237, DateTimeKind.Utc).AddTicks(7500));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 1, 16, 28, 1, 237, DateTimeKind.Utc).AddTicks(7500));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 1, 16, 28, 1, 237, DateTimeKind.Utc).AddTicks(7501));

            migrationBuilder.UpdateData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 1, 16, 28, 1, 237, DateTimeKind.Utc).AddTicks(7614));

            migrationBuilder.UpdateData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 1, 16, 28, 1, 237, DateTimeKind.Utc).AddTicks(7615));

            migrationBuilder.UpdateData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 1, 16, 28, 1, 237, DateTimeKind.Utc).AddTicks(7616));

            migrationBuilder.UpdateData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 1, 16, 28, 1, 237, DateTimeKind.Utc).AddTicks(7617));

            migrationBuilder.UpdateData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 1, 16, 28, 1, 237, DateTimeKind.Utc).AddTicks(7618));

            migrationBuilder.UpdateData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 1, 16, 28, 1, 237, DateTimeKind.Utc).AddTicks(7619));

            migrationBuilder.CreateIndex(
                name: "IX_Games_SupportedPlatformsId",
                table: "Games",
                column: "SupportedPlatformsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Platforms_SupportedPlatformsId",
                table: "Games",
                column: "SupportedPlatformsId",
                principalTable: "Platforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

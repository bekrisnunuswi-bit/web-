using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitnessCenter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedTrainerAvailabilities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TrainerAvailabilities",
                columns: new[] { "Id", "DayOfWeek", "EndTime", "StartTime", "TrainerId" },
                values: new object[,]
                {
                    { 1, 1, new TimeSpan(0, 22, 0, 0, 0), new TimeSpan(0, 14, 0, 0, 0), 1 },
                    { 2, 3, new TimeSpan(0, 22, 0, 0, 0), new TimeSpan(0, 14, 0, 0, 0), 1 },
                    { 3, 5, new TimeSpan(0, 22, 0, 0, 0), new TimeSpan(0, 14, 0, 0, 0), 1 },
                    { 4, 2, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 15, 0, 0, 0), 2 },
                    { 5, 4, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 15, 0, 0, 0), 2 },
                    { 6, 6, new TimeSpan(0, 21, 0, 0, 0), new TimeSpan(0, 15, 0, 0, 0), 2 },
                    { 7, 1, new TimeSpan(0, 15, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), 3 },
                    { 8, 3, new TimeSpan(0, 15, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), 3 },
                    { 9, 0, new TimeSpan(0, 14, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), 3 },
                    { 10, 1, new TimeSpan(0, 12, 0, 0, 0), new TimeSpan(0, 7, 0, 0, 0), 4 },
                    { 11, 2, new TimeSpan(0, 12, 0, 0, 0), new TimeSpan(0, 7, 0, 0, 0), 4 },
                    { 12, 3, new TimeSpan(0, 12, 0, 0, 0), new TimeSpan(0, 7, 0, 0, 0), 4 },
                    { 13, 4, new TimeSpan(0, 12, 0, 0, 0), new TimeSpan(0, 7, 0, 0, 0), 4 },
                    { 14, 5, new TimeSpan(0, 12, 0, 0, 0), new TimeSpan(0, 7, 0, 0, 0), 4 },
                    { 15, 1, new TimeSpan(0, 20, 0, 0, 0), new TimeSpan(0, 13, 0, 0, 0), 5 },
                    { 16, 2, new TimeSpan(0, 20, 0, 0, 0), new TimeSpan(0, 13, 0, 0, 0), 5 },
                    { 17, 3, new TimeSpan(0, 20, 0, 0, 0), new TimeSpan(0, 13, 0, 0, 0), 5 },
                    { 18, 4, new TimeSpan(0, 20, 0, 0, 0), new TimeSpan(0, 13, 0, 0, 0), 5 },
                    { 19, 5, new TimeSpan(0, 20, 0, 0, 0), new TimeSpan(0, 13, 0, 0, 0), 5 },
                    { 20, 1, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 18, 0, 0, 0), 6 },
                    { 21, 3, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 18, 0, 0, 0), 6 },
                    { 22, 5, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 18, 0, 0, 0), 6 },
                    { 23, 0, new TimeSpan(0, 22, 0, 0, 0), new TimeSpan(0, 16, 0, 0, 0), 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "TrainerAvailabilities",
                keyColumn: "Id",
                keyValue: 23);
        }
    }
}

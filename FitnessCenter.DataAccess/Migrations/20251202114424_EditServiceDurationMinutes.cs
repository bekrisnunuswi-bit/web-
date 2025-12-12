using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessCenter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EditServiceDurationMinutes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1,
                column: "DurationMinutes",
                value: 60);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 4,
                column: "MaxCapacity",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DurationMinutes", "MaxCapacity" },
                values: new object[] { 60, 10 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1,
                column: "DurationMinutes",
                value: 120);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 4,
                column: "MaxCapacity",
                value: 15);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DurationMinutes", "MaxCapacity" },
                values: new object[] { 90, 15 });
        }
    }
}

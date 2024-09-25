using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddedAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "RequestHistories");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Requests",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Requests");

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "RequestHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

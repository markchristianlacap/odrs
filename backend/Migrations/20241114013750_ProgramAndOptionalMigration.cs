using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class ProgramAndOptionalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StudentNumber",
                table: "Requests",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Section",
                table: "Requests",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "CampusId",
                table: "Programs",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_CampusId",
                table: "Programs",
                column: "CampusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Campuses_CampusId",
                table: "Programs",
                column: "CampusId",
                principalTable: "Campuses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Campuses_CampusId",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Programs_CampusId",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "CampusId",
                table: "Programs");

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "StudentNumber",
                keyValue: null,
                column: "StudentNumber",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "StudentNumber",
                table: "Requests",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Section",
                keyValue: null,
                column: "Section",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Section",
                table: "Requests",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}

﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase().Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder
                .CreateTable(
                    name: "ResetPasswords",
                    columns: table => new
                    {
                        Id = table.Column<Guid>(
                            type: "char(36)",
                            nullable: false,
                            collation: "ascii_general_ci"
                        ),
                        UserId = table.Column<Guid>(
                            type: "char(36)",
                            nullable: false,
                            collation: "ascii_general_ci"
                        ),
                        Token = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        ExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                        CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_ResetPasswords", x => x.Id);
                    }
                )
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder
                .CreateTable(
                    name: "Users",
                    columns: table => new
                    {
                        Id = table.Column<Guid>(
                            type: "char(36)",
                            nullable: false,
                            collation: "ascii_general_ci"
                        ),
                        FirstName = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        LastName = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        MiddleName = table
                            .Column<string>(type: "longtext", nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        ExtensionName = table
                            .Column<string>(type: "longtext", nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        Email = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        ContactNumber = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        Password = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        Role = table.Column<int>(type: "int", nullable: false),
                        IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                        CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                        UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Users", x => x.Id);
                    }
                )
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ResetPasswords");

            migrationBuilder.DropTable(name: "Users");
        }
    }
}
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NGC.DAL.Migrations
{
    public partial class First_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Email = table.Column<string>(nullable: true),
                    last_sent = table.Column<DateTime>(type: "datetime", nullable: true),
                    name = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true),
                    surname_1 = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    surname_2 = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Login = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true),
                    password = table.Column<string>(type: "varchar(132)", maxLength: 132, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}

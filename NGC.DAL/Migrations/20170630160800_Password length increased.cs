using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NGC.DAL.Migrations
{
    public partial class Passwordlengthincreased : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "user",
                type: "varchar(176)",
                maxLength: 176,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(132)",
                oldMaxLength: 132,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "user",
                type: "varchar(132)",
                maxLength: 132,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(176)",
                oldMaxLength: 176,
                oldNullable: true);
        }
    }
}

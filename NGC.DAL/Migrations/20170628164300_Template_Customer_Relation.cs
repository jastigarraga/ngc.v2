using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NGC.DAL.Migrations
{
    public partial class Template_Customer_Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChildrenCount",
                table: "customer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "customer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "IdTemplate",
                table: "customer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaritalState",
                table: "customer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_customer_IdTemplate",
                table: "customer",
                column: "IdTemplate");
            migrationBuilder.Sql("UPDATE customer SET IdTemplate=(SELECT id FROM emailtemplate WHERE id=(SELECT MIN(id) FROM emailtemplate))");
            migrationBuilder.AddForeignKey(
                name: "FK_customer_emailtemplate_IdTemplate",
                table: "customer",
                column: "IdTemplate",
                principalTable: "emailtemplate",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customer_emailtemplate_IdTemplate",
                table: "customer");

            migrationBuilder.DropIndex(
                name: "IX_customer_IdTemplate",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "ChildrenCount",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "IdTemplate",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "MaritalState",
                table: "customer");
        }
    }
}

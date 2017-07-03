using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NGC.DAL.Migrations
{
    public partial class Add_Images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "meraki_text_image",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Bytes = table.Column<byte[]>(nullable: true),
                    FontName = table.Column<string>(nullable: true),
                    Height = table.Column<double>(nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    Text = table.Column<string>(type: "varchar(100)", nullable: true),
                    Width = table.Column<double>(nullable: false),
                    X = table.Column<double>(nullable: false),
                    Y = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meraki_text_image", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "meraki_text_image");
        }
    }
}

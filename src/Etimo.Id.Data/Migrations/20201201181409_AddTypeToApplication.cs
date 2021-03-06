﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Etimo.Id.Data.Migrations
{
    public partial class AddTypeToApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Applications",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Applications");
        }
    }
}

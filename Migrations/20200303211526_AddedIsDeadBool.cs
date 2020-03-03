﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Tamagotchi.Migrations
{
    public partial class AddedIsDeadBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDead",
                table: "Pets",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDead",
                table: "Pets");
        }
    }
}

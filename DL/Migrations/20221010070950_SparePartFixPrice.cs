﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class SparePartFixPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FixPrice",
                table: "RepairOrderSpareParts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FixPrice",
                table: "RepairOrderSpareParts");
        }
    }
}

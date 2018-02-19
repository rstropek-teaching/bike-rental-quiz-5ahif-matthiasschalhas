using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RentalBike1.Migrations
{
    public partial class mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PriceFirstHour",
                table: "Bikes",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "decimal(20,2)");

            migrationBuilder.AlterColumn<double>(
                name: "PriceAddHour",
                table: "Bikes",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "decimal(20,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PriceFirstHour",
                table: "Bikes",
                type: "decimal(20,2)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "PriceAddHour",
                table: "Bikes",
                type: "decimal(20,2)",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}

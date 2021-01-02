using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManageExport_V2.Migrations
{
    public partial class addcolumnexpirecontractdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireContractDate",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpireContractDate",
                table: "User");
        }
    }
}

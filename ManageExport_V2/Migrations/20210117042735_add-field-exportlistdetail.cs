using Microsoft.EntityFrameworkCore.Migrations;

namespace ManageExport_V2.Migrations
{
    public partial class addfieldexportlistdetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExportNumber",
                table: "ExportListDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "ExportPrice",
                table: "ExportListDetail",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExportNumber",
                table: "ExportListDetail");

            migrationBuilder.DropColumn(
                name: "ExportPrice",
                table: "ExportListDetail");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace ManageExport_V2.Migrations
{
    public partial class editmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.AddColumn<int>(
                name: "ExportManagerId",
                table: "ExportProductBill",
                type: "int",
                nullable: false,
                defaultValue: 0);

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "ExportManagerId",
                table: "ExportProductBill");

          
        }
    }
}

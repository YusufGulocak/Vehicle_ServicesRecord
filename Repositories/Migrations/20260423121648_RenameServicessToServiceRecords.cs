using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class RenameServicessToServiceRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(name: "Servicess", newName: "ServiceRecords");
            migrationBuilder.RenameIndex(name: "IX_Servicess_VehicleId", table: "ServiceRecords", newName: "IX_ServiceRecords_VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(name: "ServiceRecords", newName: "Servicess");
            migrationBuilder.RenameIndex(name: "IX_ServiceRecords_VehicleId", table: "Servicess", newName: "IX_Servicess_VehicleId");
        }
    }
}

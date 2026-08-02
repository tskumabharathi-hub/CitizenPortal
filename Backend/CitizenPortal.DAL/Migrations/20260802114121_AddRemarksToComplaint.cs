using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CitizenPortal.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddRemarksToComplaint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Complaints",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Complaints");
        }
    }
}

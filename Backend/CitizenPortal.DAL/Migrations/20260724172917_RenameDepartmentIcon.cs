using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CitizenPortal.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenameDepartmentIcon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "icon",
                table: "Departments",
                newName: "Icon");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Icon",
                table: "Departments",
                newName: "icon");
        }
    }
}

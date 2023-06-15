using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TNWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedRegion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "Regions",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Regions",
                newName: "Guid");
        }
    }
}

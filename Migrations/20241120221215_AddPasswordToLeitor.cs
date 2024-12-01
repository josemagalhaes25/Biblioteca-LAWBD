using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAWBD_fase3.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordToLeitor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Leitores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Leitores");
        }
    }
}
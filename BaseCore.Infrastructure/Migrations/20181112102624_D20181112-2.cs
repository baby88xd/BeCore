using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseCore.Infrastructure.Migrations
{
    public partial class D201811122 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ParentID",
                table: "Sys_Navigations",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ParentID",
                table: "Sys_Navigations",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseCore.Infrastructure.Migrations
{
    public partial class S201810261 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sys_Buttons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Deleted = table.Column<bool>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    ButtonText = table.Column<string>(nullable: true),
                    Sortnum = table.Column<string>(nullable: true),
                    iconCls = table.Column<string>(nullable: true),
                    IconUrl = table.Column<string>(nullable: true),
                    ButtonTag = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    ButtonHtml = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Buttons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Deleted = table.Column<bool>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    DepartmentName = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: false),
                    Sortnum = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Departments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sys_Buttons");

            migrationBuilder.DropTable(
                name: "Sys_Departments");
        }
    }
}

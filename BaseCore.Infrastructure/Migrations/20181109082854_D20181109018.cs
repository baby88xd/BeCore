using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseCore.Infrastructure.Migrations
{
    public partial class D20181109018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sys_Navigations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Deleted = table.Column<bool>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    NavTitle = table.Column<string>(nullable: true),
                    Linkurl = table.Column<string>(nullable: true),
                    Sortnum = table.Column<string>(nullable: true),
                    iconCls = table.Column<string>(nullable: true),
                    iconUrl = table.Column<string>(nullable: true),
                    IsVisible = table.Column<string>(nullable: true),
                    ParentID = table.Column<string>(nullable: true),
                    NavTag = table.Column<string>(nullable: true),
                    BigImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Navigations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sys_Navigations");
        }
    }
}

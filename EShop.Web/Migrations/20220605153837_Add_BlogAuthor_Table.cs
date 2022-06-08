using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Web.Migrations
{
    public partial class Add_BlogAuthor_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogAuthors",
                columns: table => new
                {
                    BlogAuthorId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ShortDescription = table.Column<string>(maxLength: 800, nullable: false),
                    Image = table.Column<string>(maxLength: 60, nullable: false),
                    RegisterDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogAuthors", x => x.BlogAuthorId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogAuthors");
        }
    }
}

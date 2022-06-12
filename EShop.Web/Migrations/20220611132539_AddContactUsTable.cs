using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Web.Migrations
{
    public partial class AddContactUsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    ContactUsId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 13, nullable: true),
                    Message = table.Column<string>(maxLength: 800, nullable: false),
                    HasResponse = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.ContactUsId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactUs");
        }
    }
}

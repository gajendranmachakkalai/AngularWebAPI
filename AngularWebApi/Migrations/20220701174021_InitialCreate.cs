using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularWebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {   
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(100)", nullable: false),
                    EmailId = table.Column<string>(type: "varchar(300)", nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(10)", nullable: true),
                    Address = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "EmailId", "Password", "PhoneNumber", "UserName" },
                values: new object[,]
                {
                    { 1, "Chennai", "gajendran@gmail.com", "Test@123$", "8956231478", "Gajendran" },
                    { 2, "Coimbator", "gobi@gmail.com", "Test@123$", "7894561237", "Gobi" },
                    { 3, "Thirvanmiur", "vijey@gmail.com", "Test@123$", "4567892589", "vijeykumar" },
                    { 4, "Vadapalani", "rahul@gmail.com", "Test@123$", "9856237415", "Rahul" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmailId",
                table: "Users",
                column: "EmailId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

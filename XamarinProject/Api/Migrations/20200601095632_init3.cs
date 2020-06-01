using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(maxLength: 40, nullable: false),
                    Password = table.Column<string>(maxLength: 40, nullable: false),
                    IsOnline = table.Column<string>(maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_ID);
                });

            migrationBuilder.CreateTable(
                name: "Msgs",
                columns: table => new
                {
                    Message_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(maxLength: 1000, nullable: false),
                    User_ID_From = table.Column<int>(nullable: false),
                    User_ID_To = table.Column<int>(nullable: false),
                    Read = table.Column<bool>(nullable: false),
                    User_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Msgs", x => x.Message_ID);
                    table.ForeignKey(
                        name: "FK_Msgs_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Msgs_User_ID",
                table: "Msgs",
                column: "User_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Msgs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

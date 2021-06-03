using Microsoft.EntityFrameworkCore.Migrations;

namespace app.Migrations
{
    public partial class Followers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Followers",
                columns: table => new
                {
                    FollowerId = table.Column<int>(type: "int", nullable: false),
                    FollowedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Followers", x => new { x.FollowerId, x.FollowedId });
                    table.ForeignKey(
                        name: "FK_Followers_Users_FollowedId",
                        column: x => x.FollowedId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Followers_Users_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Followers_FollowedId",
                table: "Followers",
                column: "FollowedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Followers");
        }
    }
}

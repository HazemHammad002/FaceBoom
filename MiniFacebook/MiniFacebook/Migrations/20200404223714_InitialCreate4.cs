using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniFacebook.Migrations
{
    public partial class InitialCreate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Feeling",
                table: "Posts",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostState",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CommentState",
                table: "Comments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserState",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostState",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CommentState",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserState",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Feeling",
                table: "Posts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldDefaultValue: 0);
        }
    }
}

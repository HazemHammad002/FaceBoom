using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniFacebook.Migrations
{
    public partial class loginpull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 4, 5, 11, 5, 10, 886, DateTimeKind.Local).AddTicks(8865));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDate",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 4, 5, 11, 5, 10, 889, DateTimeKind.Local).AddTicks(1174));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 4, 5, 11, 5, 10, 886, DateTimeKind.Local).AddTicks(8865),
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDate",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 4, 5, 11, 5, 10, 889, DateTimeKind.Local).AddTicks(1174),
                oldClrType: typeof(DateTime));
        }
    }
}

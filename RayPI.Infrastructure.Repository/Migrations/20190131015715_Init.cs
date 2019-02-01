using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RayPI.Infrastructure.Repository.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateName = table.Column<string>(maxLength: 128, nullable: true),
                    CreateId = table.Column<long>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    UpdateName = table.Column<string>(maxLength: 128, nullable: true),
                    UpdateId = table.Column<long>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateName = table.Column<string>(maxLength: 128, nullable: true),
                    CreateId = table.Column<long>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    UpdateName = table.Column<string>(maxLength: 128, nullable: true),
                    UpdateId = table.Column<long>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Teacher");
        }
    }
}

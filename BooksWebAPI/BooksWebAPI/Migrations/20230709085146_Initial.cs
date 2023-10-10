using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Create_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Create_User = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Modify_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modify_User = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Codes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Type_Desc = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Create_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Create_User = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Modify_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modify_User = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Codes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    CName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Register_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Register_User = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Modify_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modify_User = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Bought_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1200)", maxLength: 1200, nullable: true),
                    Create_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Create_User = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Modify_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modify_User = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    ClassId = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    CodeId = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    MemberId = table.Column<string>(type: "nvarchar(12)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Codes_CodeId",
                        column: x => x.CodeId,
                        principalTable: "Codes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_ClassId",
                table: "Books",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CodeId",
                table: "Books",
                column: "CodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_MemberId",
                table: "Books",
                column: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Codes");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}

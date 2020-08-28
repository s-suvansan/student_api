using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace StudentApi.Migrations
{
    public partial class firstStudentMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentTable",
                columns: table => new
                {
                    SID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTable", x => x.SID);
                });

            migrationBuilder.CreateTable(
                name: "SportsTable",
                columns: table => new
                {
                    SportsID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SportsName = table.Column<string>(nullable: true),
                    StudID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportsTable", x => x.SportsID);
                    table.ForeignKey(
                        name: "FK_SportsTable_StudentTable_StudID",
                        column: x => x.StudID,
                        principalTable: "StudentTable",
                        principalColumn: "SID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectTable",
                columns: table => new
                {
                    SubjectID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SubjectTitle = table.Column<string>(nullable: true),
                    StudId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTable", x => x.SubjectID);
                    table.ForeignKey(
                        name: "FK_SubjectTable_StudentTable_StudId",
                        column: x => x.StudId,
                        principalTable: "StudentTable",
                        principalColumn: "SID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SportsTable_StudID",
                table: "SportsTable",
                column: "StudID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTable_StudId",
                table: "SubjectTable",
                column: "StudId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SportsTable");

            migrationBuilder.DropTable(
                name: "SubjectTable");

            migrationBuilder.DropTable(
                name: "StudentTable");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace consoleApp.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "course",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "education",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_education", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationCourse",
                columns: table => new
                {
                    EducationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationCourse", x => new { x.EducationId, x.CourseId });
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    EducationId = table.Column<int>(type: "INTEGER", nullable: false),
                    phoneNumber = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_students_education_EducationId",
                        column: x => x.EducationId,
                        principalTable: "education",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "teacher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    TeachesId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_teacher_education_TeachesId",
                        column: x => x.TeachesId,
                        principalTable: "education",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_students_EducationId",
                table: "students",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_TeachesId",
                table: "teacher",
                column: "TeachesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "course");

            migrationBuilder.DropTable(
                name: "EducationCourse");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "teacher");

            migrationBuilder.DropTable(
                name: "education");
        }
    }
}

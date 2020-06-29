using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tamrin.Data.Migrations
{
    public partial class migInitialDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    LastUpdateDateTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ParentId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    IsHide = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    LastUpdateDateTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RoleId = table.Column<long>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 30, nullable: true),
                    LastName = table.Column<string>(maxLength: 40, nullable: true),
                    GenderType = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Email = table.Column<string>(unicode: false, maxLength: 120, nullable: false),
                    PasswordHash = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    AvatarName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    ActiveCode = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    LastUpdateDateTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TeacherId = table.Column<long>(nullable: false),
                    Title = table.Column<string>(maxLength: 75, nullable: false),
                    CoursePriceType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    Price = table.Column<int>(nullable: false),
                    ImageName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    CourseLevel = table.Column<int>(nullable: false),
                    CourseStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseComments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    LastUpdateDateTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    CourseId = table.Column<long>(nullable: false),
                    ParentId = table.Column<long>(nullable: true),
                    Text = table.Column<string>(maxLength: 400, nullable: false),
                    IsConfirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseComments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseComments_CourseComments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "CourseComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseScores",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    LastUpdateDateTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    CourseId = table.Column<long>(nullable: false),
                    ImageAndSoundQuality = table.Column<int>(nullable: false),
                    MasterIsMasteryOfTheSubject = table.Column<int>(nullable: false),
                    CourseQuality = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseScores_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseScores_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseSelectedCategories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    LastUpdateDateTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CourseId = table.Column<long>(nullable: false),
                    CategoryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSelectedCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseSelectedCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseSelectedCategories_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseVisits",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<long>(nullable: false),
                    Ip = table.Column<string>(unicode: false, maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseVisits_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    LastUpdateDateTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CourseId = table.Column<long>(nullable: false),
                    Title = table.Column<string>(maxLength: 45, nullable: false),
                    Description = table.Column<string>(maxLength: 150, nullable: true),
                    FileName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    EpisodePriceType = table.Column<int>(nullable: false),
                    Time = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episodes_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseComments_CourseId",
                table: "CourseComments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseComments_ParentId",
                table: "CourseComments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseComments_UserId",
                table: "CourseComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseScores_CourseId",
                table: "CourseScores",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseScores_UserId",
                table: "CourseScores",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSelectedCategories_CategoryId",
                table: "CourseSelectedCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSelectedCategories_CourseId",
                table: "CourseSelectedCategories",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseVisits_CourseId",
                table: "CourseVisits",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_CourseId",
                table: "Episodes",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseComments");

            migrationBuilder.DropTable(
                name: "CourseScores");

            migrationBuilder.DropTable(
                name: "CourseSelectedCategories");

            migrationBuilder.DropTable(
                name: "CourseVisits");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

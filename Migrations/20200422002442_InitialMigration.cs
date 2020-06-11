using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organization.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChildEntities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(nullable: false),
                    ChildId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Weekend = table.Column<string>(nullable: true),
                    OkrDisplay = table.Column<string>(nullable: true),
                    OkrStatus = table.Column<string>(nullable: true),
                    WallDisplayed = table.Column<string>(nullable: true),
                    EntityDisplayed = table.Column<string>(nullable: true),
                    Avatar = table.Column<byte>(nullable: false),
                    Parent_id = table.Column<int>(nullable: false),
                    TimeZone = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationPermission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    EntityId = table.Column<int>(nullable: false),
                    GetNotification = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationPermission_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OKRPermission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    EntityId = table.Column<int>(nullable: false),
                    Add = table.Column<bool>(nullable: false),
                    Update = table.Column<bool>(nullable: false),
                    Delete = table.Column<bool>(nullable: false),
                    Observe = table.Column<bool>(nullable: false),
                    AddEmployees = table.Column<bool>(nullable: false),
                    DeleteEmployees = table.Column<bool>(nullable: false),
                    AddKResult = table.Column<bool>(nullable: false),
                    DeleteKResult = table.Column<bool>(nullable: false),
                    updateKResult = table.Column<bool>(nullable: false),
                    ObserveKResult = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OKRPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OKRPermission_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationPermission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    EntityId = table.Column<int>(nullable: false),
                    ChangeSettings = table.Column<bool>(nullable: false),
                    ChangeOrganizationProfile = table.Column<bool>(nullable: false),
                    AddEntities = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationPermission_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Entity",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    EntityId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Entity", x => new { x.UserId, x.EntityId });
                    table.ForeignKey(
                        name: "FK_User_Entity_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Entity_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WallPermission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    EntityId = table.Column<int>(nullable: false),
                    Add = table.Column<bool>(nullable: false),
                    Update = table.Column<bool>(nullable: false),
                    Delete = table.Column<bool>(nullable: false),
                    Observe = table.Column<bool>(nullable: false),
                    AddEmployees = table.Column<bool>(nullable: false),
                    DeleteEmployees = table.Column<bool>(nullable: false),
                    DisplayFivePointQuestionUser = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WallPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WallPermission_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationPermission_UserId",
                table: "NotificationPermission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OKRPermission_userId",
                table: "OKRPermission",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationPermission_UserId",
                table: "OrganizationPermission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Entity_EntityId",
                table: "User_Entity",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WallPermission_userId",
                table: "WallPermission",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChildEntities");

            migrationBuilder.DropTable(
                name: "NotificationPermission");

            migrationBuilder.DropTable(
                name: "OKRPermission");

            migrationBuilder.DropTable(
                name: "OrganizationPermission");

            migrationBuilder.DropTable(
                name: "User_Entity");

            migrationBuilder.DropTable(
                name: "WallPermission");

            migrationBuilder.DropTable(
                name: "Entities");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

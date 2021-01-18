using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Praise.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    StartHour = table.Column<DateTime>(nullable: true),
                    EndHour = table.Column<DateTime>(nullable: true),
                    Local = table.Column<string>(type: "varchar(100)", maxLength: 50, nullable: true),
                    Note = table.Column<string>(type: "varchar(1000)", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "musics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true),
                    Reminder = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true),
                    Singer = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true),
                    Lirycs = table.Column<string>(type: "text", nullable: true),
                    Notation = table.Column<string>(type: "text", nullable: true),
                    Video = table.Column<string>(type: "varchar(2048)", maxLength: 60, nullable: true),
                    Play = table.Column<ulong>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_musics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Username = table.Column<string>(type: "varchar(20)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "char(32)", fixedLength: true, nullable: true),
                    Phone = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: true),
                    Photo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true),
                    Disabled = table.Column<ulong>(type: "bit", nullable: false),
                    LastLogon = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventsMusics",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false),
                    MusicId = table.Column<int>(nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Play = table.Column<ulong>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsMusics", x => new { x.EventId, x.MusicId });
                    table.ForeignKey(
                        name: "FK_EventsMusics_events_EventId",
                        column: x => x.EventId,
                        principalTable: "events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventsMusics_musics_MusicId",
                        column: x => x.MusicId,
                        principalTable: "musics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventsUsers",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsUsers", x => new { x.EventId, x.UserId });
                    table.ForeignKey(
                        name: "FK_EventsUsers_events_EventId",
                        column: x => x.EventId,
                        principalTable: "events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventsUsers_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventsMusics_MusicId",
                table: "EventsMusics",
                column: "MusicId");

            migrationBuilder.CreateIndex(
                name: "IX_EventsUsers_UserId",
                table: "EventsUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_users_Username",
                table: "users",
                column: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventsMusics");

            migrationBuilder.DropTable(
                name: "EventsUsers");

            migrationBuilder.DropTable(
                name: "musics");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}

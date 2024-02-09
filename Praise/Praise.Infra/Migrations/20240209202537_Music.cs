using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Praise.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Music : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Musics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    Reminder = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true),
                    Singer = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true),
                    Lirycs = table.Column<string>(type: "text", nullable: true),
                    Video = table.Column<string>(type: "varchar(2048)", maxLength: 60, nullable: true),
                    Play = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musics", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Musics");
        }
    }
}

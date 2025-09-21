using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiSerasa.Migrations
{
    /// <inheritdoc />
    public partial class InitPetTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PetId",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Vaccines = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Age = table.Column<byte>(type: "INTEGER", nullable: false),
                    AnimalSize = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Locale = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropColumn(
                name: "PetId",
                table: "Users");
        }
    }
}

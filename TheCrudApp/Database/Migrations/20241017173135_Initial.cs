using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheCrudApp.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    model = table.Column<string>(type: "text", nullable: true),
                    year = table.Column<int>(type: "integer", nullable: true),
                    price = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cars", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cars");
        }
    }
}

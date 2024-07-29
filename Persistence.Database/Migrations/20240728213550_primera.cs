using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Database.Migrations
{
    public partial class primera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberDocument = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeDocument = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberDocumentTypeDocument = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TypeDocuments",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeDocuments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "TypeDocuments",
                columns: new[] { "ID", "Abbreviation", "State", "Type" },
                values: new object[] { new Guid("3af4f6b4-8e93-4f1d-9b7d-13d3d577d7df"), "PA", 1, "Pasaporte" });

            migrationBuilder.InsertData(
                table: "TypeDocuments",
                columns: new[] { "ID", "Abbreviation", "State", "Type" },
                values: new object[] { new Guid("649bbf51-d1d1-4525-b4c3-3a836a7a430b"), "CC", 1, "Cédula Ciudadanía" });

            migrationBuilder.InsertData(
                table: "TypeDocuments",
                columns: new[] { "ID", "Abbreviation", "State", "Type" },
                values: new object[] { new Guid("9a88b25a-8334-45ca-9e1a-33538af3fa0d"), "CE", 1, "Cédula Extranjería" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "TypeDocuments");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

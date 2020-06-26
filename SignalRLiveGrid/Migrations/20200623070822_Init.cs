using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SignalRLiveGrid.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Salary = table.Column<decimal>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "PersonId", "IsLock", "Name", "Salary" },
                values: new object[] { 1, false, "John", 10m });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "PersonId", "IsLock", "Name", "Salary" },
                values: new object[] { 2, false, "Logan", 20m });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "PersonId", "IsLock", "Name", "Salary" },
                values: new object[] { 3, false, "James", 30m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");
        }
    }
}

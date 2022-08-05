using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class addlanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
              name: "Languages",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                  ShortName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                  Status = table.Column<int>(type: "int", nullable: false),
                  CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                  UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                  CreatedAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  UpdatedAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Languages", x => x.Id);
                  
              });
            migrationBuilder.AddForeignKey(
                name: "FK_Languages_Accounts_CreatedAccountId",
                table: "Languages",
                column: "CreatedAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_Accounts_UpdatedAccountId",
                table: "Languages",
                column: "UpdatedAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_CreatedAccountId",
                table: "Languages",
                column: "CreatedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_UpdatedAccountId",
                table: "Languages",
                column: "UpdatedAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Accounts_CreatedAccountId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Accounts_UpdatedAccountId",
                table: "Languages");

            migrationBuilder.DropIndex(
               name: "IX_Languages_CreatedAccountId",
               table: "Languages");

            migrationBuilder.DropIndex(
               name: "IX_Languages_UpdatedAccountId",
               table: "Languages");
        }
    }
}

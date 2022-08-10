using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class updateEntityGender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
               name: "FK_Gendermanagemet_Accounts_CreatedAccountId",
               table: "Gendermanagemet",
               column: "CreatedAccountId",
               principalTable: "Accounts",
               principalColumn: "Id",
               onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Gendermanagemet_Accounts_UpdatedAccountId",
                table: "Gendermanagemet",
                column: "UpdatedAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gendermanagemet_Accounts_CreatedAccountId",
                table: "Gendermanagemet");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Accounts_UpdatedAccountId",
                table: "Gendermanagemet");
        }
    }
}

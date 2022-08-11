using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class updateEntityGender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
               name: "CreatedAccountId",
               table: "Gendermanagemet",
               type: "uniqueidentifier",
               nullable: false,
               defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Gendermanagemet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedAccountId",
                table: "Gendermanagemet",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Gendermanagemet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

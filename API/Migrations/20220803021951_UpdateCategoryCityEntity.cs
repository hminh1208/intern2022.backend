using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class UpdateCategoryCityEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedAccountId",
                table: "CategoryCities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CategoryCities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedAccountId",
                table: "CategoryCities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "CategoryCities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_CategoryCities_CreatedAccountId",
                table: "CategoryCities",
                column: "CreatedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryCities_UpdatedAccountId",
                table: "CategoryCities",
                column: "UpdatedAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryCities_Accounts_CreatedAccountId",
                table: "CategoryCities",
                column: "CreatedAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryCities_Accounts_UpdatedAccountId",
                table: "CategoryCities",
                column: "UpdatedAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryCities_Accounts_CreatedAccountId",
                table: "CategoryCities");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryCities_Accounts_UpdatedAccountId",
                table: "CategoryCities");

            migrationBuilder.DropIndex(
                name: "IX_CategoryCities_CreatedAccountId",
                table: "CategoryCities");

            migrationBuilder.DropIndex(
                name: "IX_CategoryCities_UpdatedAccountId",
                table: "CategoryCities");

            migrationBuilder.DropColumn(
                name: "CreatedAccountId",
                table: "CategoryCities");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CategoryCities");

            migrationBuilder.DropColumn(
                name: "UpdatedAccountId",
                table: "CategoryCities");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "CategoryCities");
        }
    }
}

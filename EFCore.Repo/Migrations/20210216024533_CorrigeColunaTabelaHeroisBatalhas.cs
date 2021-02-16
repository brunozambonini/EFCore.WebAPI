using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore.Repo.Migrations
{
    public partial class CorrigeColunaTabelaHeroisBatalhas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeroisBatalhas_Herois_HeroiId",
                table: "HeroisBatalhas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HeroisBatalhas",
                table: "HeroisBatalhas");

            migrationBuilder.DropColumn(
                name: "HeroidId",
                table: "HeroisBatalhas");

            migrationBuilder.AlterColumn<int>(
                name: "HeroiId",
                table: "HeroisBatalhas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HeroisBatalhas",
                table: "HeroisBatalhas",
                columns: new[] { "BatalhaId", "HeroiId" });

            migrationBuilder.AddForeignKey(
                name: "FK_HeroisBatalhas_Herois_HeroiId",
                table: "HeroisBatalhas",
                column: "HeroiId",
                principalTable: "Herois",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeroisBatalhas_Herois_HeroiId",
                table: "HeroisBatalhas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HeroisBatalhas",
                table: "HeroisBatalhas");

            migrationBuilder.AlterColumn<int>(
                name: "HeroiId",
                table: "HeroisBatalhas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "HeroidId",
                table: "HeroisBatalhas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HeroisBatalhas",
                table: "HeroisBatalhas",
                columns: new[] { "BatalhaId", "HeroidId" });

            migrationBuilder.AddForeignKey(
                name: "FK_HeroisBatalhas_Herois_HeroiId",
                table: "HeroisBatalhas",
                column: "HeroiId",
                principalTable: "Herois",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

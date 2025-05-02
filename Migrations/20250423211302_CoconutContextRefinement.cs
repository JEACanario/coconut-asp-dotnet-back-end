using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace coconut_asp_dotnet_back_end.Migrations
{
    /// <inheritdoc />
    public partial class CoconutContextRefinement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coconuts_Users_UserId",
                table: "Coconuts");

            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Coconuts_CoconutId",
                table: "Entries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entries",
                table: "Entries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coconuts",
                table: "Coconuts");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Entries",
                newName: "Entry");

            migrationBuilder.RenameTable(
                name: "Coconuts",
                newName: "Coconut");

            migrationBuilder.RenameIndex(
                name: "IX_Entries_CoconutId",
                table: "Entry",
                newName: "IX_Entry_CoconutId");

            migrationBuilder.RenameIndex(
                name: "IX_Coconuts_UserId",
                table: "Coconut",
                newName: "IX_Coconut_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entry",
                table: "Entry",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coconut",
                table: "Coconut",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Coconut_User_UserId",
                table: "Coconut",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entry_Coconut_CoconutId",
                table: "Entry",
                column: "CoconutId",
                principalTable: "Coconut",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coconut_User_UserId",
                table: "Coconut");

            migrationBuilder.DropForeignKey(
                name: "FK_Entry_Coconut_CoconutId",
                table: "Entry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entry",
                table: "Entry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coconut",
                table: "Coconut");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Entry",
                newName: "Entries");

            migrationBuilder.RenameTable(
                name: "Coconut",
                newName: "Coconuts");

            migrationBuilder.RenameIndex(
                name: "IX_Entry_CoconutId",
                table: "Entries",
                newName: "IX_Entries_CoconutId");

            migrationBuilder.RenameIndex(
                name: "IX_Coconut_UserId",
                table: "Coconuts",
                newName: "IX_Coconuts_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entries",
                table: "Entries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coconuts",
                table: "Coconuts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Coconuts_Users_UserId",
                table: "Coconuts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Coconuts_CoconutId",
                table: "Entries",
                column: "CoconutId",
                principalTable: "Coconuts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

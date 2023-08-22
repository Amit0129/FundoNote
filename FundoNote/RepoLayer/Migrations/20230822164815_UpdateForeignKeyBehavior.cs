using Microsoft.EntityFrameworkCore.Migrations;

namespace RepoLayer.Migrations
{
    public partial class UpdateForeignKeyBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
            name: "FK_Collaborators_Notes_NoteId",
            table: "Collaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_Users_UserId",
                table: "Collaborators");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Notes_NoteId",
                table: "Collaborators",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Users_UserId",
                table: "Collaborators",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //collab
            migrationBuilder.DropForeignKey(
           name: "FK_Collaborators_Notes_NoteId",
           table: "Collaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_Users_UserId",
                table: "Collaborators");


            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Notes_NoteId",
                table: "Collaborators",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Restrict); // Change back to ReferentialAction.Cascade

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Users_UserId",
                table: "Collaborators",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict); // Change back to ReferentialAction.Cascade
        }
    }
}

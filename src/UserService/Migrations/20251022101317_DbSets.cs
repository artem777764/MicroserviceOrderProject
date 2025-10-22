using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class DbSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDataEntity_UserEntity_Id",
                table: "UserDataEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleEntity_RoleEntity_RoleId",
                table: "UserRoleEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleEntity_UserEntity_UserId",
                table: "UserRoleEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoleEntity",
                table: "UserRoleEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEntity",
                table: "UserEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDataEntity",
                table: "UserDataEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleEntity",
                table: "RoleEntity");

            migrationBuilder.RenameTable(
                name: "UserRoleEntity",
                newName: "UserRoles");

            migrationBuilder.RenameTable(
                name: "UserEntity",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "UserDataEntity",
                newName: "UsersData");

            migrationBuilder.RenameTable(
                name: "RoleEntity",
                newName: "Roles");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoleEntity_RoleId",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersData",
                table: "UsersData",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersData_Users_Id",
                table: "UsersData",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersData_Users_Id",
                table: "UsersData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersData",
                table: "UsersData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "UsersData",
                newName: "UserDataEntity");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserEntity");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "UserRoleEntity");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "RoleEntity");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoleEntity",
                newName: "IX_UserRoleEntity_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDataEntity",
                table: "UserDataEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEntity",
                table: "UserEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoleEntity",
                table: "UserRoleEntity",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleEntity",
                table: "RoleEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDataEntity_UserEntity_Id",
                table: "UserDataEntity",
                column: "Id",
                principalTable: "UserEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleEntity_RoleEntity_RoleId",
                table: "UserRoleEntity",
                column: "RoleId",
                principalTable: "RoleEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleEntity_UserEntity_UserId",
                table: "UserRoleEntity",
                column: "UserId",
                principalTable: "UserEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

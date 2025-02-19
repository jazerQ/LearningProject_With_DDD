using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PRINIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionsEntity_Roles_RoleEntityId",
                table: "PermissionsEntity");

            migrationBuilder.DropIndex(
                name: "IX_PermissionsEntity_RoleEntityId",
                table: "PermissionsEntity");

            migrationBuilder.DropColumn(
                name: "RoleEntityId",
                table: "PermissionsEntity");

            migrationBuilder.InsertData(
                table: "PermissionsEntity",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Create" },
                    { 2, "Read" },
                    { 3, "Update" },
                    { 4, "Delete" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRoleEntity_RoleId",
                table: "PermissionRoleEntity",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRoleEntity_PermissionsEntity_PermissionId",
                table: "PermissionRoleEntity",
                column: "PermissionId",
                principalTable: "PermissionsEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRoleEntity_Roles_RoleId",
                table: "PermissionRoleEntity",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionRoleEntity_PermissionsEntity_PermissionId",
                table: "PermissionRoleEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionRoleEntity_Roles_RoleId",
                table: "PermissionRoleEntity");

            migrationBuilder.DropIndex(
                name: "IX_PermissionRoleEntity_RoleId",
                table: "PermissionRoleEntity");

            migrationBuilder.DeleteData(
                table: "PermissionsEntity",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PermissionsEntity",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PermissionsEntity",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PermissionsEntity",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "RoleEntityId",
                table: "PermissionsEntity",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionsEntity_RoleEntityId",
                table: "PermissionsEntity",
                column: "RoleEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionsEntity_Roles_RoleEntityId",
                table: "PermissionsEntity",
                column: "RoleEntityId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}

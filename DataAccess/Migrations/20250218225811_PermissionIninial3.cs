using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PermissionIninial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PermissionRoleEntity",
                table: "PermissionRoleEntity");

            migrationBuilder.DropIndex(
                name: "IX_PermissionRoleEntity_RoleId",
                table: "PermissionRoleEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PermissionRoleEntity",
                table: "PermissionRoleEntity",
                columns: new[] { "RoleId", "PermissionId" });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRoleEntity_PermissionId",
                table: "PermissionRoleEntity",
                column: "PermissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PermissionRoleEntity",
                table: "PermissionRoleEntity");

            migrationBuilder.DropIndex(
                name: "IX_PermissionRoleEntity_PermissionId",
                table: "PermissionRoleEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PermissionRoleEntity",
                table: "PermissionRoleEntity",
                columns: new[] { "PermissionId", "RoleId" });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRoleEntity_RoleId",
                table: "PermissionRoleEntity",
                column: "RoleId");
        }
    }
}

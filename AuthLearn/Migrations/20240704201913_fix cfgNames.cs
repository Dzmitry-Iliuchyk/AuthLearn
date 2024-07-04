using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthLearn.Migrations
{
    /// <inheritdoc />
    public partial class fixcfgNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5dfa87f8-731e-4770-95de-a7cf7c21268c"));

            migrationBuilder.InsertData(
                table: "GroupPermission",
                columns: new[] { "GroupId", "PermissionId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 5 },
                    { 2, 4 },
                    { 2, 6 },
                    { 3, 1 },
                    { 3, 2 },
                    { 3, 3 },
                    { 3, 4 },
                    { 3, 6 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash" },
                values: new object[] { new Guid("64e779ff-3e8f-4b52-97ee-ac40f1e67a91"), "admin@admin.com", "admin", "AQAAAAIAAYagAAAAEGjAu93UxqZgmS5I4vIyvBU98HMszCST/Y7MgA3L+cXanktcyqI9mwFj8Ezwtr9l/w==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GroupPermission",
                keyColumns: new[] { "GroupId", "PermissionId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "GroupPermission",
                keyColumns: new[] { "GroupId", "PermissionId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "GroupPermission",
                keyColumns: new[] { "GroupId", "PermissionId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "GroupPermission",
                keyColumns: new[] { "GroupId", "PermissionId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "GroupPermission",
                keyColumns: new[] { "GroupId", "PermissionId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "GroupPermission",
                keyColumns: new[] { "GroupId", "PermissionId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "GroupPermission",
                keyColumns: new[] { "GroupId", "PermissionId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "GroupPermission",
                keyColumns: new[] { "GroupId", "PermissionId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "GroupPermission",
                keyColumns: new[] { "GroupId", "PermissionId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "GroupPermission",
                keyColumns: new[] { "GroupId", "PermissionId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "GroupPermission",
                keyColumns: new[] { "GroupId", "PermissionId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("64e779ff-3e8f-4b52-97ee-ac40f1e67a91"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash" },
                values: new object[] { new Guid("5dfa87f8-731e-4770-95de-a7cf7c21268c"), "admin@admin.com", "admin", "AQAAAAIAAYagAAAAEFiSSEKxjsiaiR895Sq++DFCULYDyuTNeLcp8qg4AK5aO3nQBEK7ZnvGRkA9V/yBVQ==" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eComSolution.Data.Migrations
{
    public partial class UpdateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "ResetPasses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16);

            migrationBuilder.UpdateData(
                table: "Histories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 10, 21, 11, 9, 2, 742, DateTimeKind.Local).AddTicks(8105));

            migrationBuilder.UpdateData(
                table: "Histories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2021, 10, 21, 11, 9, 2, 742, DateTimeKind.Local).AddTicks(8562));

            migrationBuilder.UpdateData(
                table: "Histories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2021, 10, 21, 11, 9, 2, 742, DateTimeKind.Local).AddTicks(8566));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2021, 10, 21, 11, 9, 2, 743, DateTimeKind.Local).AddTicks(512));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 10, 21, 11, 9, 2, 741, DateTimeKind.Local).AddTicks(6741));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 10, 21, 11, 9, 2, 742, DateTimeKind.Local).AddTicks(5501));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2021, 10, 21, 11, 9, 2, 742, DateTimeKind.Local).AddTicks(5518));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2021, 10, 21, 11, 9, 2, 742, DateTimeKind.Local).AddTicks(5521));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 110, 11, 157, 197, 139, 42, 85, 120, 221, 76, 70, 99, 141, 167, 111, 130, 207, 114, 150, 25, 33, 170, 247, 41, 19, 240, 211, 174, 200, 39, 54, 73, 109, 108, 75, 207, 219, 173, 166, 92, 235, 218, 134, 55, 148, 175, 237, 60, 10, 95, 153, 117, 125, 76, 211, 87, 44, 68, 88, 25, 202, 196, 174, 229 }, new byte[] { 133, 227, 203, 184, 116, 248, 210, 82, 173, 86, 4, 107, 132, 88, 159, 239, 164, 22, 143, 101, 221, 105, 167, 72, 44, 122, 109, 148, 201, 137, 223, 54, 1, 157, 42, 220, 151, 176, 113, 24, 34, 129, 162, 30, 218, 213, 115, 200, 228, 213, 99, 130, 41, 124, 236, 180, 50, 241, 90, 7, 229, 108, 63, 91, 149, 192, 145, 181, 157, 28, 163, 165, 8, 220, 150, 246, 15, 224, 113, 56, 27, 26, 21, 30, 84, 97, 107, 175, 217, 129, 50, 236, 110, 6, 84, 213, 78, 42, 67, 77, 173, 22, 32, 146, 109, 147, 163, 139, 182, 39, 15, 137, 69, 4, 248, 133, 212, 124, 67, 102, 85, 78, 224, 116, 225, 52, 156, 107 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 214, 145, 105, 139, 127, 136, 120, 37, 6, 120, 254, 53, 57, 86, 32, 66, 124, 232, 242, 68, 37, 63, 104, 209, 141, 150, 116, 15, 52, 197, 36, 48, 172, 152, 131, 233, 160, 252, 102, 49, 89, 123, 212, 199, 127, 67, 137, 146, 116, 209, 169, 91, 216, 128, 114, 33, 54, 145, 220, 150, 47, 22, 243, 232 }, new byte[] { 133, 227, 203, 184, 116, 248, 210, 82, 173, 86, 4, 107, 132, 88, 159, 239, 164, 22, 143, 101, 221, 105, 167, 72, 44, 122, 109, 148, 201, 137, 223, 54, 1, 157, 42, 220, 151, 176, 113, 24, 34, 129, 162, 30, 218, 213, 115, 200, 228, 213, 99, 130, 41, 124, 236, 180, 50, 241, 90, 7, 229, 108, 63, 91, 149, 192, 145, 181, 157, 28, 163, 165, 8, 220, 150, 246, 15, 224, 113, 56, 27, 26, 21, 30, 84, 97, 107, 175, 217, 129, 50, 236, 110, 6, 84, 213, 78, 42, 67, 77, 173, 22, 32, 146, 109, 147, 163, 139, 182, 39, 15, 137, 69, 4, 248, 133, 212, 124, 67, 102, 85, 78, 224, 116, 225, 52, 156, 107 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 28, 181, 254, 193, 81, 186, 242, 155, 193, 28, 102, 94, 180, 166, 27, 93, 82, 126, 50, 245, 130, 121, 162, 23, 119, 198, 162, 5, 102, 62, 78, 35, 130, 222, 166, 125, 134, 45, 211, 213, 100, 131, 62, 102, 122, 16, 53, 255, 139, 99, 39, 188, 109, 74, 56, 171, 9, 89, 41, 50, 38, 72, 79, 82 }, new byte[] { 133, 227, 203, 184, 116, 248, 210, 82, 173, 86, 4, 107, 132, 88, 159, 239, 164, 22, 143, 101, 221, 105, 167, 72, 44, 122, 109, 148, 201, 137, 223, 54, 1, 157, 42, 220, 151, 176, 113, 24, 34, 129, 162, 30, 218, 213, 115, 200, 228, 213, 99, 130, 41, 124, 236, 180, 50, 241, 90, 7, 229, 108, 63, 91, 149, 192, 145, 181, 157, 28, 163, 165, 8, 220, 150, 246, 15, 224, 113, 56, 27, 26, 21, 30, 84, 97, 107, 175, 217, 129, 50, 236, 110, 6, 84, 213, 78, 42, 67, 77, 173, 22, 32, 146, 109, 147, 163, 139, 182, 39, 15, 137, 69, 4, 248, 133, 212, 124, 67, 102, 85, 78, 224, 116, 225, 52, 156, 107 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 41, 177, 172, 178, 201, 195, 108, 196, 185, 201, 183, 5, 103, 163, 41, 138, 3, 13, 101, 254, 171, 139, 18, 254, 191, 47, 223, 63, 185, 113, 243, 10, 30, 59, 126, 52, 74, 150, 223, 67, 86, 228, 84, 182, 222, 240, 238, 191, 233, 28, 99, 121, 57, 90, 176, 49, 34, 166, 40, 196, 78, 102, 40, 10 }, new byte[] { 133, 227, 203, 184, 116, 248, 210, 82, 173, 86, 4, 107, 132, 88, 159, 239, 164, 22, 143, 101, 221, 105, 167, 72, 44, 122, 109, 148, 201, 137, 223, 54, 1, 157, 42, 220, 151, 176, 113, 24, 34, 129, 162, 30, 218, 213, 115, 200, 228, 213, 99, 130, 41, 124, 236, 180, 50, 241, 90, 7, 229, 108, 63, 91, 149, 192, 145, 181, 157, 28, 163, 165, 8, 220, 150, 246, 15, 224, 113, 56, 27, 26, 21, 30, 84, 97, 107, 175, 217, 129, 50, 236, 110, 6, 84, 213, 78, 42, 67, 77, 173, 22, 32, 146, 109, 147, 163, 139, 182, 39, 15, 137, 69, 4, 248, 133, 212, 124, 67, 102, 85, 78, 224, 116, 225, 52, 156, 107 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "ResetPasses",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Histories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 10, 21, 10, 49, 54, 94, DateTimeKind.Local).AddTicks(4355));

            migrationBuilder.UpdateData(
                table: "Histories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2021, 10, 21, 10, 49, 54, 94, DateTimeKind.Local).AddTicks(4796));

            migrationBuilder.UpdateData(
                table: "Histories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2021, 10, 21, 10, 49, 54, 94, DateTimeKind.Local).AddTicks(4800));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2021, 10, 21, 10, 49, 54, 94, DateTimeKind.Local).AddTicks(6684));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 10, 21, 10, 49, 54, 93, DateTimeKind.Local).AddTicks(3072));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 10, 21, 10, 49, 54, 94, DateTimeKind.Local).AddTicks(1702));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2021, 10, 21, 10, 49, 54, 94, DateTimeKind.Local).AddTicks(1717));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2021, 10, 21, 10, 49, 54, 94, DateTimeKind.Local).AddTicks(1719));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 221, 195, 133, 103, 243, 27, 249, 121, 146, 30, 175, 104, 57, 9, 113, 212, 48, 170, 172, 179, 128, 22, 177, 215, 187, 188, 245, 98, 112, 236, 52, 196, 42, 26, 106, 7, 94, 208, 233, 138, 232, 64, 208, 37, 49, 167, 16, 43, 7, 53, 98, 107, 121, 52, 165, 70, 233, 16, 156, 41, 221, 209, 44, 102 }, new byte[] { 227, 79, 249, 106, 253, 140, 231, 131, 9, 16, 135, 157, 254, 205, 72, 28, 129, 220, 2, 224, 207, 211, 214, 138, 224, 222, 93, 253, 35, 220, 80, 181, 255, 46, 51, 26, 175, 222, 195, 56, 140, 79, 57, 154, 79, 213, 53, 6, 204, 135, 2, 162, 196, 5, 131, 53, 203, 33, 192, 51, 253, 126, 186, 154, 122, 2, 203, 156, 47, 149, 66, 214, 19, 211, 93, 145, 120, 78, 6, 18, 141, 117, 158, 94, 23, 252, 103, 101, 32, 138, 107, 185, 41, 128, 110, 56, 159, 63, 1, 33, 210, 117, 27, 97, 111, 174, 228, 191, 150, 194, 208, 234, 229, 247, 121, 163, 59, 183, 41, 166, 66, 81, 231, 111, 99, 92, 14, 149 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 237, 21, 46, 16, 11, 159, 179, 165, 100, 64, 48, 53, 194, 111, 199, 7, 182, 26, 55, 214, 245, 105, 211, 248, 181, 162, 169, 82, 186, 75, 228, 31, 145, 131, 248, 63, 114, 107, 193, 48, 220, 107, 187, 34, 115, 144, 210, 68, 0, 168, 120, 30, 156, 62, 97, 14, 190, 41, 58, 212, 35, 59, 47, 146 }, new byte[] { 227, 79, 249, 106, 253, 140, 231, 131, 9, 16, 135, 157, 254, 205, 72, 28, 129, 220, 2, 224, 207, 211, 214, 138, 224, 222, 93, 253, 35, 220, 80, 181, 255, 46, 51, 26, 175, 222, 195, 56, 140, 79, 57, 154, 79, 213, 53, 6, 204, 135, 2, 162, 196, 5, 131, 53, 203, 33, 192, 51, 253, 126, 186, 154, 122, 2, 203, 156, 47, 149, 66, 214, 19, 211, 93, 145, 120, 78, 6, 18, 141, 117, 158, 94, 23, 252, 103, 101, 32, 138, 107, 185, 41, 128, 110, 56, 159, 63, 1, 33, 210, 117, 27, 97, 111, 174, 228, 191, 150, 194, 208, 234, 229, 247, 121, 163, 59, 183, 41, 166, 66, 81, 231, 111, 99, 92, 14, 149 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 45, 126, 216, 132, 128, 189, 64, 196, 191, 183, 138, 35, 214, 197, 240, 176, 135, 212, 149, 113, 158, 237, 140, 213, 235, 52, 179, 39, 252, 131, 93, 103, 82, 180, 238, 87, 88, 75, 90, 62, 89, 138, 52, 40, 99, 19, 253, 112, 120, 16, 200, 156, 115, 107, 126, 57, 217, 129, 179, 57, 125, 218, 221, 176 }, new byte[] { 227, 79, 249, 106, 253, 140, 231, 131, 9, 16, 135, 157, 254, 205, 72, 28, 129, 220, 2, 224, 207, 211, 214, 138, 224, 222, 93, 253, 35, 220, 80, 181, 255, 46, 51, 26, 175, 222, 195, 56, 140, 79, 57, 154, 79, 213, 53, 6, 204, 135, 2, 162, 196, 5, 131, 53, 203, 33, 192, 51, 253, 126, 186, 154, 122, 2, 203, 156, 47, 149, 66, 214, 19, 211, 93, 145, 120, 78, 6, 18, 141, 117, 158, 94, 23, 252, 103, 101, 32, 138, 107, 185, 41, 128, 110, 56, 159, 63, 1, 33, 210, 117, 27, 97, 111, 174, 228, 191, 150, 194, 208, 234, 229, 247, 121, 163, 59, 183, 41, 166, 66, 81, 231, 111, 99, 92, 14, 149 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 177, 247, 191, 30, 12, 33, 116, 81, 141, 218, 137, 139, 95, 50, 211, 57, 0, 32, 202, 165, 228, 245, 195, 104, 172, 179, 32, 201, 252, 55, 211, 157, 170, 131, 232, 122, 124, 232, 106, 144, 140, 62, 144, 248, 90, 200, 241, 138, 39, 79, 88, 32, 111, 244, 71, 59, 252, 224, 105, 217, 55, 8, 130, 64 }, new byte[] { 227, 79, 249, 106, 253, 140, 231, 131, 9, 16, 135, 157, 254, 205, 72, 28, 129, 220, 2, 224, 207, 211, 214, 138, 224, 222, 93, 253, 35, 220, 80, 181, 255, 46, 51, 26, 175, 222, 195, 56, 140, 79, 57, 154, 79, 213, 53, 6, 204, 135, 2, 162, 196, 5, 131, 53, 203, 33, 192, 51, 253, 126, 186, 154, 122, 2, 203, 156, 47, 149, 66, 214, 19, 211, 93, 145, 120, 78, 6, 18, 141, 117, 158, 94, 23, 252, 103, 101, 32, 138, 107, 185, 41, 128, 110, 56, 159, 63, 1, 33, 210, 117, 27, 97, 111, 174, 228, 191, 150, 194, 208, 234, 229, 247, 121, 163, 59, 183, 41, 166, 66, 81, 231, 111, 99, 92, 14, 149 } });
        }
    }
}

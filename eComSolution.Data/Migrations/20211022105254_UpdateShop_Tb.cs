using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eComSolution.Data.Migrations
{
    public partial class UpdateShop_Tb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Shops",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Histories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 10, 22, 17, 52, 54, 169, DateTimeKind.Local).AddTicks(3801));

            migrationBuilder.UpdateData(
                table: "Histories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2021, 10, 22, 17, 52, 54, 169, DateTimeKind.Local).AddTicks(4288));

            migrationBuilder.UpdateData(
                table: "Histories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2021, 10, 22, 17, 52, 54, 169, DateTimeKind.Local).AddTicks(4292));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderDate", "State" },
                values: new object[] { new DateTime(2021, 10, 22, 17, 52, 54, 169, DateTimeKind.Local).AddTicks(6225), "Chờ xử lý" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 10, 22, 17, 52, 54, 167, DateTimeKind.Local).AddTicks(8769));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 10, 22, 17, 52, 54, 169, DateTimeKind.Local).AddTicks(538));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2021, 10, 22, 17, 52, 54, 169, DateTimeKind.Local).AddTicks(569));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2021, 10, 22, 17, 52, 54, 169, DateTimeKind.Local).AddTicks(571));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 81, 248, 223, 7, 66, 155, 73, 65, 244, 193, 137, 142, 121, 34, 52, 21, 191, 73, 31, 206, 76, 21, 177, 220, 217, 116, 192, 151, 51, 203, 249, 23, 1, 33, 24, 134, 42, 196, 90, 240, 185, 118, 104, 169, 134, 242, 73, 218, 254, 255, 138, 17, 177, 197, 170, 4, 241, 186, 120, 143, 57, 236, 72, 118 }, new byte[] { 145, 134, 143, 179, 85, 185, 211, 228, 60, 253, 233, 59, 248, 202, 177, 212, 255, 98, 155, 1, 16, 219, 151, 211, 231, 187, 250, 105, 144, 48, 20, 64, 124, 122, 13, 141, 236, 152, 146, 95, 134, 173, 41, 250, 244, 232, 29, 1, 117, 168, 35, 3, 2, 63, 91, 151, 199, 83, 248, 145, 25, 114, 209, 71, 146, 182, 5, 103, 227, 0, 70, 248, 29, 249, 183, 215, 158, 102, 153, 101, 35, 11, 144, 86, 54, 170, 226, 181, 69, 204, 118, 192, 236, 82, 229, 45, 102, 204, 203, 144, 164, 113, 3, 43, 47, 225, 41, 126, 191, 169, 137, 196, 232, 78, 103, 182, 131, 78, 44, 91, 132, 26, 87, 135, 26, 134, 44, 220 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 24, 243, 49, 91, 174, 130, 237, 99, 60, 166, 28, 182, 23, 112, 41, 216, 123, 117, 31, 108, 191, 210, 41, 54, 160, 159, 230, 185, 64, 146, 80, 178, 72, 24, 36, 188, 206, 55, 128, 232, 93, 209, 25, 1, 236, 255, 186, 232, 48, 9, 188, 137, 237, 90, 73, 107, 151, 58, 15, 156, 242, 113, 121, 241 }, new byte[] { 145, 134, 143, 179, 85, 185, 211, 228, 60, 253, 233, 59, 248, 202, 177, 212, 255, 98, 155, 1, 16, 219, 151, 211, 231, 187, 250, 105, 144, 48, 20, 64, 124, 122, 13, 141, 236, 152, 146, 95, 134, 173, 41, 250, 244, 232, 29, 1, 117, 168, 35, 3, 2, 63, 91, 151, 199, 83, 248, 145, 25, 114, 209, 71, 146, 182, 5, 103, 227, 0, 70, 248, 29, 249, 183, 215, 158, 102, 153, 101, 35, 11, 144, 86, 54, 170, 226, 181, 69, 204, 118, 192, 236, 82, 229, 45, 102, 204, 203, 144, 164, 113, 3, 43, 47, 225, 41, 126, 191, 169, 137, 196, 232, 78, 103, 182, 131, 78, 44, 91, 132, 26, 87, 135, 26, 134, 44, 220 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 136, 217, 3, 94, 53, 185, 187, 178, 3, 253, 166, 22, 28, 132, 244, 238, 77, 1, 73, 110, 241, 68, 10, 150, 70, 88, 124, 19, 148, 35, 11, 135, 201, 215, 21, 29, 237, 125, 222, 141, 87, 79, 143, 189, 226, 122, 139, 181, 36, 122, 0, 137, 65, 238, 246, 87, 30, 19, 195, 33, 227, 160, 87, 156 }, new byte[] { 145, 134, 143, 179, 85, 185, 211, 228, 60, 253, 233, 59, 248, 202, 177, 212, 255, 98, 155, 1, 16, 219, 151, 211, 231, 187, 250, 105, 144, 48, 20, 64, 124, 122, 13, 141, 236, 152, 146, 95, 134, 173, 41, 250, 244, 232, 29, 1, 117, 168, 35, 3, 2, 63, 91, 151, 199, 83, 248, 145, 25, 114, 209, 71, 146, 182, 5, 103, 227, 0, 70, 248, 29, 249, 183, 215, 158, 102, 153, 101, 35, 11, 144, 86, 54, 170, 226, 181, 69, 204, 118, 192, 236, 82, 229, 45, 102, 204, 203, 144, 164, 113, 3, 43, 47, 225, 41, 126, 191, 169, 137, 196, 232, 78, 103, 182, 131, 78, 44, 91, 132, 26, 87, 135, 26, 134, 44, 220 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 196, 218, 2, 147, 228, 22, 147, 53, 172, 254, 255, 86, 17, 22, 226, 200, 90, 97, 150, 158, 169, 249, 251, 233, 106, 134, 219, 12, 30, 161, 17, 232, 178, 203, 66, 140, 69, 228, 147, 4, 75, 176, 150, 80, 81, 32, 50, 108, 118, 80, 195, 34, 70, 249, 180, 173, 10, 25, 29, 254, 152, 169, 233, 231 }, new byte[] { 145, 134, 143, 179, 85, 185, 211, 228, 60, 253, 233, 59, 248, 202, 177, 212, 255, 98, 155, 1, 16, 219, 151, 211, 231, 187, 250, 105, 144, 48, 20, 64, 124, 122, 13, 141, 236, 152, 146, 95, 134, 173, 41, 250, 244, 232, 29, 1, 117, 168, 35, 3, 2, 63, 91, 151, 199, 83, 248, 145, 25, 114, 209, 71, 146, 182, 5, 103, 227, 0, 70, 248, 29, 249, 183, 215, 158, 102, 153, 101, 35, 11, 144, 86, 54, 170, 226, 181, 69, 204, 118, 192, 236, 82, 229, 45, 102, 204, 203, 144, 164, 113, 3, 43, 47, 225, 41, 126, 191, 169, 137, 196, 232, 78, 103, 182, 131, 78, 44, 91, 132, 26, 87, 135, 26, 134, 44, 220 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Shops");

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
                columns: new[] { "OrderDate", "State" },
                values: new object[] { new DateTime(2021, 10, 21, 11, 9, 2, 743, DateTimeKind.Local).AddTicks(512), "Đang xử lý" });

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
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eComSolution.Data.Migrations
{
    public partial class Update_Product_Tb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Histories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 10, 26, 7, 22, 27, 654, DateTimeKind.Local).AddTicks(999));

            migrationBuilder.UpdateData(
                table: "Histories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2021, 10, 26, 7, 22, 27, 654, DateTimeKind.Local).AddTicks(1698));

            migrationBuilder.UpdateData(
                table: "Histories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2021, 10, 26, 7, 22, 27, 654, DateTimeKind.Local).AddTicks(1704));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2021, 10, 26, 7, 22, 27, 654, DateTimeKind.Local).AddTicks(4426));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "Gender" },
                values: new object[] { new DateTime(2021, 10, 26, 7, 22, 27, 653, DateTimeKind.Local).AddTicks(5589), 1 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "Gender" },
                values: new object[] { new DateTime(2021, 10, 26, 7, 22, 27, 653, DateTimeKind.Local).AddTicks(8073), 1 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "Gender" },
                values: new object[] { new DateTime(2021, 10, 26, 7, 22, 27, 653, DateTimeKind.Local).AddTicks(8082), 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "Gender" },
                values: new object[] { new DateTime(2021, 10, 26, 7, 22, 27, 653, DateTimeKind.Local).AddTicks(8085), 1 });

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateModified",
                value: new DateTime(2021, 10, 26, 7, 22, 27, 644, DateTimeKind.Local).AddTicks(8840));

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateModified",
                value: new DateTime(2021, 10, 26, 7, 22, 27, 645, DateTimeKind.Local).AddTicks(9532));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 138, 130, 217, 125, 56, 214, 193, 158, 243, 238, 229, 115, 41, 252, 231, 68, 1, 210, 174, 2, 143, 250, 1, 76, 139, 175, 108, 169, 223, 40, 186, 115, 51, 46, 239, 135, 65, 136, 133, 147, 39, 135, 20, 254, 159, 127, 218, 4, 175, 236, 218, 85, 7, 27, 43, 190, 164, 140, 118, 97, 121, 222, 108, 203 }, new byte[] { 10, 136, 56, 185, 137, 156, 104, 139, 204, 145, 191, 35, 250, 63, 52, 54, 34, 139, 131, 36, 25, 47, 125, 96, 10, 27, 120, 251, 60, 172, 56, 238, 134, 179, 72, 109, 58, 223, 252, 36, 133, 5, 148, 129, 120, 82, 167, 242, 43, 0, 68, 153, 161, 77, 16, 144, 47, 77, 50, 118, 132, 146, 42, 77, 0, 191, 4, 101, 174, 128, 34, 127, 62, 208, 116, 75, 200, 164, 133, 17, 208, 115, 83, 184, 214, 129, 149, 161, 28, 185, 38, 185, 46, 23, 204, 59, 227, 94, 240, 64, 199, 123, 30, 233, 33, 108, 250, 145, 140, 92, 83, 164, 38, 162, 154, 140, 86, 138, 163, 145, 209, 110, 194, 121, 89, 236, 166, 74 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 142, 17, 184, 165, 114, 48, 123, 160, 144, 50, 65, 65, 188, 214, 103, 89, 38, 88, 117, 12, 59, 247, 115, 202, 83, 73, 84, 126, 171, 7, 204, 34, 210, 140, 94, 13, 123, 87, 187, 18, 120, 183, 37, 198, 52, 91, 98, 28, 16, 105, 228, 150, 126, 204, 135, 250, 231, 170, 143, 126, 176, 95, 86, 69 }, new byte[] { 10, 136, 56, 185, 137, 156, 104, 139, 204, 145, 191, 35, 250, 63, 52, 54, 34, 139, 131, 36, 25, 47, 125, 96, 10, 27, 120, 251, 60, 172, 56, 238, 134, 179, 72, 109, 58, 223, 252, 36, 133, 5, 148, 129, 120, 82, 167, 242, 43, 0, 68, 153, 161, 77, 16, 144, 47, 77, 50, 118, 132, 146, 42, 77, 0, 191, 4, 101, 174, 128, 34, 127, 62, 208, 116, 75, 200, 164, 133, 17, 208, 115, 83, 184, 214, 129, 149, 161, 28, 185, 38, 185, 46, 23, 204, 59, 227, 94, 240, 64, 199, 123, 30, 233, 33, 108, 250, 145, 140, 92, 83, 164, 38, 162, 154, 140, 86, 138, 163, 145, 209, 110, 194, 121, 89, 236, 166, 74 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 163, 53, 138, 231, 47, 1, 66, 163, 32, 239, 148, 252, 98, 173, 216, 85, 67, 116, 43, 240, 30, 174, 253, 43, 128, 95, 118, 195, 119, 71, 212, 174, 83, 130, 203, 174, 249, 55, 19, 225, 255, 20, 156, 248, 55, 182, 11, 38, 180, 74, 239, 118, 233, 88, 242, 156, 185, 229, 193, 50, 232, 4, 219, 230 }, new byte[] { 10, 136, 56, 185, 137, 156, 104, 139, 204, 145, 191, 35, 250, 63, 52, 54, 34, 139, 131, 36, 25, 47, 125, 96, 10, 27, 120, 251, 60, 172, 56, 238, 134, 179, 72, 109, 58, 223, 252, 36, 133, 5, 148, 129, 120, 82, 167, 242, 43, 0, 68, 153, 161, 77, 16, 144, 47, 77, 50, 118, 132, 146, 42, 77, 0, 191, 4, 101, 174, 128, 34, 127, 62, 208, 116, 75, 200, 164, 133, 17, 208, 115, 83, 184, 214, 129, 149, 161, 28, 185, 38, 185, 46, 23, 204, 59, 227, 94, 240, 64, 199, 123, 30, 233, 33, 108, 250, 145, 140, 92, 83, 164, 38, 162, 154, 140, 86, 138, 163, 145, 209, 110, 194, 121, 89, 236, 166, 74 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 39, 65, 193, 208, 105, 99, 41, 154, 249, 194, 238, 211, 119, 182, 65, 150, 44, 44, 70, 133, 227, 244, 207, 139, 217, 13, 158, 209, 11, 228, 0, 3, 6, 178, 163, 131, 106, 73, 35, 201, 92, 251, 7, 225, 81, 72, 113, 58, 140, 76, 128, 95, 42, 81, 95, 28, 165, 68, 176, 53, 78, 191, 211, 151 }, new byte[] { 10, 136, 56, 185, 137, 156, 104, 139, 204, 145, 191, 35, 250, 63, 52, 54, 34, 139, 131, 36, 25, 47, 125, 96, 10, 27, 120, 251, 60, 172, 56, 238, 134, 179, 72, 109, 58, 223, 252, 36, 133, 5, 148, 129, 120, 82, 167, 242, 43, 0, 68, 153, 161, 77, 16, 144, 47, 77, 50, 118, 132, 146, 42, 77, 0, 191, 4, 101, 174, 128, 34, 127, 62, 208, 116, 75, 200, 164, 133, 17, 208, 115, 83, 184, 214, 129, 149, 161, 28, 185, 38, 185, 46, 23, 204, 59, 227, 94, 240, 64, 199, 123, 30, 233, 33, 108, 250, 145, 140, 92, 83, 164, 38, 162, 154, 140, 86, 138, 163, 145, 209, 110, 194, 121, 89, 236, 166, 74 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Products");

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
                column: "OrderDate",
                value: new DateTime(2021, 10, 22, 17, 52, 54, 169, DateTimeKind.Local).AddTicks(6225));

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
                table: "Shops",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateModified",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateModified",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
    }
}

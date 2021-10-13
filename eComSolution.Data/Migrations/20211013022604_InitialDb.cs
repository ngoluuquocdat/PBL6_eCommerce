using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eComSolution.Data.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Functions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Functions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    FunctionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => new { x.FunctionId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_Permissions_Functions_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "Functions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permissions_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    OriginalPrice = table.Column<int>(type: "int", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(62)", maxLength: 62, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: true),
                    Disable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    ColorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSizeDetail = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupUsers",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUsers", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_GroupUsers_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Histories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Histories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShipAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductDetail_Id = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_ProductDetails_ProductDetail_Id",
                        column: x => x.ProductDetail_Id,
                        principalTable: "ProductDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductDetail_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_ProductDetails_ProductDetail_Id",
                        column: x => x.ProductDetail_Id,
                        principalTable: "ProductDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "SortOrder" },
                values: new object[,]
                {
                    { 1, "Áo Thun", 1 },
                    { 2, "Áo Sơ mi", 2 },
                    { 3, "Áo Hoodie", 3 },
                    { 4, "Quần Jean", 4 },
                    { 5, "Đồ lót", 5 }
                });

            migrationBuilder.InsertData(
                table: "Functions",
                columns: new[] { "Id", "ActionName" },
                values: new object[,]
                {
                    { 1, "Register" },
                    { 2, "Login" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Mod" },
                    { 3, "Member" }
                });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "Address", "Avatar", "Description", "Disable", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "123 DT605, Hòa Tiến, Hòa Vang, Đà Nẵng", "", "Cửa hàng áo quần chất lượng cao, giá cả phải chăng", false, "Tuấn's Fashion", "0921231220" },
                    { 2, "160 Trần Nhật Duật, Cẩm Châu, Hội An, Quảng Nam", "", "Cửa hàng áo quần nam", false, "Đạt's Clothes", "0905553859" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Disable", "Email", "Fullname", "PasswordHash", "PasswordSalt", "PhoneNumber", "ShopId", "Username" },
                values: new object[,]
                {
                    { 1, false, "tuandang29042000@gmail.com", "admin", new byte[] { 229, 210, 0, 238, 153, 149, 95, 2, 34, 63, 5, 157, 132, 221, 245, 194, 171, 87, 59, 175, 96, 9, 3, 197, 50, 137, 196, 86, 136, 221, 135, 203, 80, 237, 198, 32, 59, 109, 94, 70, 218, 27, 89, 71, 163, 182, 5, 135, 227, 77, 5, 188, 11, 77, 86, 235, 246, 198, 19, 39, 142, 81, 63, 98 }, new byte[] { 27, 6, 185, 68, 206, 41, 252, 220, 96, 129, 168, 245, 69, 11, 214, 191, 199, 82, 204, 89, 198, 119, 138, 248, 201, 59, 112, 71, 243, 116, 116, 15, 80, 190, 79, 90, 16, 217, 210, 115, 197, 72, 33, 53, 37, 7, 211, 172, 119, 161, 124, 99, 236, 210, 201, 191, 114, 20, 30, 82, 147, 141, 235, 202, 42, 15, 126, 17, 86, 151, 63, 187, 220, 36, 119, 132, 75, 115, 181, 190, 91, 18, 114, 197, 253, 212, 59, 166, 220, 230, 153, 231, 253, 234, 124, 80, 22, 184, 54, 95, 190, 255, 46, 102, 21, 231, 152, 61, 6, 134, 109, 29, 5, 121, 220, 252, 29, 1, 242, 115, 76, 173, 172, 190, 87, 134, 136, 148 }, "0921231220", null, "admin" },
                    { 4, false, "badding@gmail.com", "Đinh Công Tài", new byte[] { 121, 120, 15, 223, 181, 101, 2, 237, 178, 43, 183, 231, 236, 18, 23, 22, 243, 109, 77, 59, 211, 10, 221, 169, 76, 5, 65, 143, 229, 124, 110, 189, 71, 232, 136, 75, 80, 51, 109, 150, 192, 135, 60, 91, 99, 133, 123, 34, 166, 121, 70, 212, 177, 135, 78, 3, 89, 106, 33, 76, 14, 255, 95, 250 }, new byte[] { 27, 6, 185, 68, 206, 41, 252, 220, 96, 129, 168, 245, 69, 11, 214, 191, 199, 82, 204, 89, 198, 119, 138, 248, 201, 59, 112, 71, 243, 116, 116, 15, 80, 190, 79, 90, 16, 217, 210, 115, 197, 72, 33, 53, 37, 7, 211, 172, 119, 161, 124, 99, 236, 210, 201, 191, 114, 20, 30, 82, 147, 141, 235, 202, 42, 15, 126, 17, 86, 151, 63, 187, 220, 36, 119, 132, 75, 115, 181, 190, 91, 18, 114, 197, 253, 212, 59, 166, 220, 230, 153, 231, 253, 234, 124, 80, 22, 184, 54, 95, 190, 255, 46, 102, 21, 231, 152, 61, 6, 134, 109, 29, 5, 121, 220, 252, 29, 1, 242, 115, 76, 173, 172, 190, 87, 134, 136, 148 }, "090553859", null, "congtai" }
                });

            migrationBuilder.InsertData(
                table: "GroupUsers",
                columns: new[] { "GroupId", "UserId" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 2, 1 },
                    { 1, 1 },
                    { 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "OrderDate", "ShipAddress", "ShipName", "ShipPhone", "ShopId", "State", "UserId" },
                values: new object[] { 1, new DateTime(2021, 10, 13, 9, 26, 3, 946, DateTimeKind.Local).AddTicks(9960), "123 Lê Duẩn, Thanh Khê, Đà Nẵng", "Đinh Công Tài", "09053438847", 2, "Đang xử lý", 4 });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "FunctionId", "GroupId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 2, 2 },
                    { 1, 2 },
                    { 2, 1 },
                    { 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "DateCreated", "Description", "Name", "OriginalPrice", "Price", "ShopId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 10, 13, 9, 26, 3, 945, DateTimeKind.Local).AddTicks(1465), "Mô tả sản phẩm:\n Chất liệu: 100% cotton\n Màu sắc : trắng & đen", "Áo thun SadBoiz", 100000, 200000, 1 },
                    { 2, 5, new DateTime(2021, 10, 13, 9, 26, 3, 946, DateTimeKind.Local).AddTicks(3022), "Mô tả sản phẩm:\n Chất liệu: 100% cotton\n Màu sắc : đỏ & đen", "Quần lót Calvin Klein", 100000, 150000, 1 },
                    { 3, 1, new DateTime(2021, 10, 13, 9, 26, 3, 946, DateTimeKind.Local).AddTicks(3045), "Mô tả sản phẩm:\n Brand: XFire\n Chất liệu : cotton co dãn", "Áo thun trơn XFire", 150000, 250000, 2 },
                    { 4, 4, new DateTime(2021, 10, 13, 9, 26, 3, 946, DateTimeKind.Local).AddTicks(3048), "Mô tả sản phẩm:\n Form: Fit\n Chất liệu : jean", "Quần Jean ôm nam", 150000, 250000, 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Disable", "Email", "Fullname", "PasswordHash", "PasswordSalt", "PhoneNumber", "ShopId", "Username" },
                values: new object[,]
                {
                    { 3, false, "ngoluuquocdat@gmail.com", "Ngô Lưu Quốc Đạt", new byte[] { 113, 161, 34, 206, 30, 200, 99, 114, 167, 53, 10, 133, 139, 82, 160, 73, 162, 82, 44, 131, 52, 182, 7, 91, 84, 141, 92, 238, 75, 20, 125, 124, 83, 216, 183, 65, 89, 165, 73, 222, 227, 231, 136, 22, 192, 170, 52, 234, 154, 252, 119, 30, 232, 166, 196, 177, 228, 227, 74, 237, 230, 69, 237, 176 }, new byte[] { 27, 6, 185, 68, 206, 41, 252, 220, 96, 129, 168, 245, 69, 11, 214, 191, 199, 82, 204, 89, 198, 119, 138, 248, 201, 59, 112, 71, 243, 116, 116, 15, 80, 190, 79, 90, 16, 217, 210, 115, 197, 72, 33, 53, 37, 7, 211, 172, 119, 161, 124, 99, 236, 210, 201, 191, 114, 20, 30, 82, 147, 141, 235, 202, 42, 15, 126, 17, 86, 151, 63, 187, 220, 36, 119, 132, 75, 115, 181, 190, 91, 18, 114, 197, 253, 212, 59, 166, 220, 230, 153, 231, 253, 234, 124, 80, 22, 184, 54, 95, 190, 255, 46, 102, 21, 231, 152, 61, 6, 134, 109, 29, 5, 121, 220, 252, 29, 1, 242, 115, 76, 173, 172, 190, 87, 134, 136, 148 }, "090553859", 2, "quocdat" },
                    { 2, false, "tuandang29042000@gmail.com", "Đặng Quốc Tuấn", new byte[] { 232, 156, 61, 211, 31, 150, 43, 143, 179, 246, 163, 23, 251, 171, 12, 140, 44, 56, 102, 100, 103, 35, 35, 250, 101, 28, 23, 95, 193, 0, 2, 162, 44, 116, 44, 115, 72, 229, 4, 180, 67, 46, 138, 102, 12, 93, 56, 74, 138, 36, 235, 151, 2, 46, 226, 35, 39, 167, 210, 136, 37, 151, 80, 104 }, new byte[] { 27, 6, 185, 68, 206, 41, 252, 220, 96, 129, 168, 245, 69, 11, 214, 191, 199, 82, 204, 89, 198, 119, 138, 248, 201, 59, 112, 71, 243, 116, 116, 15, 80, 190, 79, 90, 16, 217, 210, 115, 197, 72, 33, 53, 37, 7, 211, 172, 119, 161, 124, 99, 236, 210, 201, 191, 114, 20, 30, 82, 147, 141, 235, 202, 42, 15, 126, 17, 86, 151, 63, 187, 220, 36, 119, 132, 75, 115, 181, 190, 91, 18, 114, 197, 253, 212, 59, 166, 220, 230, 153, 231, 253, 234, 124, 80, 22, 184, 54, 95, 190, 255, 46, 102, 21, 231, 152, 61, 6, 134, 109, 29, 5, 121, 220, 252, 29, 1, 242, 115, 76, 173, 172, 190, 87, 134, 136, 148 }, "0921231220", 1, "quoctuan" }
                });

            migrationBuilder.InsertData(
                table: "GroupUsers",
                columns: new[] { "GroupId", "UserId" },
                values: new object[,]
                {
                    { 3, 2 },
                    { 2, 3 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Histories",
                columns: new[] { "Id", "Count", "Date", "ProductId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 10, 13, 9, 26, 3, 946, DateTimeKind.Local).AddTicks(6568), 2, 4 },
                    { 2, 1, new DateTime(2021, 10, 13, 9, 26, 3, 946, DateTimeKind.Local).AddTicks(7155), 4, 4 },
                    { 3, 1, new DateTime(2021, 10, 13, 9, 26, 3, 946, DateTimeKind.Local).AddTicks(7161), 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "ProductDetails",
                columns: new[] { "Id", "Color", "ProductId", "Size", "Stock" },
                values: new object[,]
                {
                    { 14, "Đen", 4, "L", 10 },
                    { 13, "Đen", 4, "XL", 10 },
                    { 12, "Xám", 3, "L", 10 },
                    { 11, "Xám", 3, "XL", 10 },
                    { 10, "Đỏ", 3, "L", 10 },
                    { 9, "Đỏ", 3, "XL", 10 },
                    { 2, "Đen", 1, "L", 10 },
                    { 3, "Trắng", 1, "XL", 10 },
                    { 8, "Đỏ", 2, "M", 10 },
                    { 7, "Đỏ", 2, "L", 10 },
                    { 6, "Đen", 2, "M", 10 },
                    { 5, "Đen", 2, "L", 10 },
                    { 4, "Trắng", 1, "L", 10 },
                    { 1, "Đen", 1, "XL", 10 }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "Price", "ProductDetail_Id", "Quantity", "UserId" },
                values: new object[] { 1, 150000, 8, 10, 4 });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "Id", "OrderId", "Price", "ProductDetail_Id", "Quantity" },
                values: new object[] { 1, 1, 250000, 11, 1 });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "Id", "OrderId", "Price", "ProductDetail_Id", "Quantity" },
                values: new object[] { 2, 1, 250000, 13, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductDetail_Id",
                table: "Carts",
                column: "ProductDetail_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_GroupId",
                table: "GroupUsers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_ProductId",
                table: "Histories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_UserId",
                table: "Histories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductDetail_Id",
                table: "OrderDetails",
                column: "ProductDetail_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShopId",
                table: "Orders",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_GroupId",
                table: "Permissions",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ProductId",
                table: "ProductDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShopId",
                table: "Products",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ShopId",
                table: "Users",
                column: "ShopId",
                unique: true,
                filter: "[ShopId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "GroupUsers");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductDetails");

            migrationBuilder.DropTable(
                name: "Functions");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Shops");
        }
    }
}

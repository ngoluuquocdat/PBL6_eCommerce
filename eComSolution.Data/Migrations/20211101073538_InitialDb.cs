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
                name: "ResetPasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Numcheck = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResetPasses", x => x.Id);
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
                    Disable = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Gender = table.Column<int>(type: "int", nullable: false),
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
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    { 5, "Đồ lót", 5 },
                    { 6, "Váy", 6 },
                    { 7, "Đầm", 7 }
                });

            migrationBuilder.InsertData(
                table: "Functions",
                columns: new[] { "Id", "ActionName" },
                values: new object[,]
                {
                    { 1, "Accounts.Register" },
                    { 2, "Accounts.Login" }
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
                columns: new[] { "Id", "Address", "Avatar", "DateCreated", "DateModified", "Description", "Disable", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "123 DT605, Hòa Tiến, Hòa Vang, Đà Nẵng", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 1, 14, 35, 37, 921, DateTimeKind.Local).AddTicks(9620), "Cửa hàng áo quần chất lượng cao, giá cả phải chăng", false, "Tuấn's Fashion", "0921231220" },
                    { 2, "160 Trần Nhật Duật, Cẩm Châu, Hội An, Quảng Nam", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 1, 14, 35, 37, 923, DateTimeKind.Local).AddTicks(636), "Cửa hàng áo quần nam", false, "Đạt's Clothes", "0905553859" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Disable", "Email", "Fullname", "PasswordHash", "PasswordSalt", "PhoneNumber", "ShopId", "Username" },
                values: new object[,]
                {
                    { 1, null, false, "tuandang29042000@gmail.com", "admin", new byte[] { 77, 41, 26, 185, 14, 218, 207, 161, 19, 170, 38, 117, 132, 104, 216, 244, 23, 197, 184, 248, 207, 164, 89, 2, 71, 122, 176, 97, 154, 113, 164, 10, 248, 94, 164, 104, 29, 165, 3, 89, 228, 64, 191, 213, 71, 28, 238, 199, 78, 197, 212, 122, 240, 237, 26, 40, 253, 198, 25, 50, 2, 104, 58, 33 }, new byte[] { 123, 210, 24, 24, 206, 157, 17, 180, 45, 128, 204, 102, 151, 180, 184, 90, 37, 89, 115, 143, 166, 12, 223, 72, 172, 230, 204, 2, 23, 148, 46, 41, 7, 32, 178, 26, 78, 118, 121, 204, 121, 86, 241, 160, 64, 175, 227, 162, 249, 18, 134, 100, 46, 212, 180, 133, 197, 191, 171, 3, 166, 127, 72, 8, 112, 143, 122, 135, 177, 203, 253, 127, 59, 209, 98, 180, 30, 134, 148, 231, 102, 70, 81, 194, 252, 203, 171, 105, 133, 202, 202, 8, 71, 57, 75, 153, 178, 59, 63, 230, 108, 232, 55, 241, 121, 205, 29, 17, 136, 160, 249, 168, 192, 41, 30, 213, 117, 47, 109, 51, 213, 176, 182, 135, 29, 77, 125, 251 }, "0921231220", null, "admin" },
                    { 4, null, false, "badding@gmail.com", "Đinh Công Tài", new byte[] { 82, 141, 165, 240, 242, 215, 238, 64, 142, 249, 192, 155, 247, 199, 98, 187, 2, 238, 137, 44, 47, 216, 177, 128, 110, 66, 197, 134, 187, 162, 8, 157, 106, 170, 11, 201, 43, 71, 238, 147, 158, 249, 216, 194, 199, 245, 193, 12, 195, 88, 15, 176, 146, 251, 202, 15, 166, 85, 175, 107, 222, 203, 78, 84 }, new byte[] { 123, 210, 24, 24, 206, 157, 17, 180, 45, 128, 204, 102, 151, 180, 184, 90, 37, 89, 115, 143, 166, 12, 223, 72, 172, 230, 204, 2, 23, 148, 46, 41, 7, 32, 178, 26, 78, 118, 121, 204, 121, 86, 241, 160, 64, 175, 227, 162, 249, 18, 134, 100, 46, 212, 180, 133, 197, 191, 171, 3, 166, 127, 72, 8, 112, 143, 122, 135, 177, 203, 253, 127, 59, 209, 98, 180, 30, 134, 148, 231, 102, 70, 81, 194, 252, 203, 171, 105, 133, 202, 202, 8, 71, 57, 75, 153, 178, 59, 63, 230, 108, 232, 55, 241, 121, 205, 29, 17, 136, 160, 249, 168, 192, 41, 30, 213, 117, 47, 109, 51, 213, 176, 182, 135, 29, 77, 125, 251 }, "090553859", null, "congtai" }
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
                values: new object[] { 1, new DateTime(2021, 11, 1, 14, 35, 37, 929, DateTimeKind.Local).AddTicks(8788), "123 Lê Duẩn, Thanh Khê, Đà Nẵng", "Đinh Công Tài", "09053438847", 2, "Chờ xử lý", 4 });

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
                columns: new[] { "Id", "CategoryId", "DateCreated", "Description", "Gender", "Name", "OriginalPrice", "Price", "ShopId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 11, 1, 14, 35, 37, 929, DateTimeKind.Local).AddTicks(628), "Mô tả sản phẩm:\n Chất liệu: 100% cotton\n Màu sắc : trắng & đen", 1, "Áo thun SadBoiz", 100000, 200000, 1 },
                    { 2, 5, new DateTime(2021, 11, 1, 14, 35, 37, 929, DateTimeKind.Local).AddTicks(2517), "Mô tả sản phẩm:\n Chất liệu: 100% cotton\n Màu sắc : đỏ & đen", 1, "Quần lót Calvin Klein", 100000, 150000, 1 },
                    { 3, 1, new DateTime(2021, 11, 1, 14, 35, 37, 929, DateTimeKind.Local).AddTicks(2524), "Mô tả sản phẩm:\n Brand: XFire\n Chất liệu : cotton co dãn", 3, "Áo thun trơn XFire", 150000, 250000, 2 },
                    { 4, 4, new DateTime(2021, 11, 1, 14, 35, 37, 929, DateTimeKind.Local).AddTicks(2526), "Mô tả sản phẩm:\n Form: Fit\n Chất liệu : jean", 1, "Quần Jean ôm nam", 150000, 250000, 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Disable", "Email", "Fullname", "PasswordHash", "PasswordSalt", "PhoneNumber", "ShopId", "Username" },
                values: new object[,]
                {
                    { 3, null, false, "ngoluuquocdat@gmail.com", "Ngô Lưu Quốc Đạt", new byte[] { 210, 254, 246, 151, 167, 216, 223, 176, 20, 196, 142, 0, 64, 70, 43, 111, 175, 71, 33, 80, 149, 121, 143, 99, 62, 213, 32, 255, 229, 169, 238, 235, 173, 163, 87, 40, 64, 202, 220, 14, 141, 211, 222, 240, 182, 108, 14, 150, 196, 6, 153, 237, 113, 183, 192, 108, 217, 22, 29, 255, 216, 78, 156, 44 }, new byte[] { 123, 210, 24, 24, 206, 157, 17, 180, 45, 128, 204, 102, 151, 180, 184, 90, 37, 89, 115, 143, 166, 12, 223, 72, 172, 230, 204, 2, 23, 148, 46, 41, 7, 32, 178, 26, 78, 118, 121, 204, 121, 86, 241, 160, 64, 175, 227, 162, 249, 18, 134, 100, 46, 212, 180, 133, 197, 191, 171, 3, 166, 127, 72, 8, 112, 143, 122, 135, 177, 203, 253, 127, 59, 209, 98, 180, 30, 134, 148, 231, 102, 70, 81, 194, 252, 203, 171, 105, 133, 202, 202, 8, 71, 57, 75, 153, 178, 59, 63, 230, 108, 232, 55, 241, 121, 205, 29, 17, 136, 160, 249, 168, 192, 41, 30, 213, 117, 47, 109, 51, 213, 176, 182, 135, 29, 77, 125, 251 }, "090553859", 2, "quocdat" },
                    { 2, null, false, "tuandang29042000@gmail.com", "Đặng Quốc Tuấn", new byte[] { 142, 136, 55, 69, 214, 50, 91, 156, 16, 71, 31, 252, 27, 37, 102, 6, 205, 193, 114, 233, 115, 92, 35, 186, 63, 214, 191, 157, 196, 173, 150, 22, 64, 56, 204, 162, 148, 127, 67, 122, 243, 165, 71, 68, 37, 20, 85, 72, 107, 116, 103, 215, 252, 73, 45, 128, 19, 16, 171, 202, 45, 37, 252, 83 }, new byte[] { 123, 210, 24, 24, 206, 157, 17, 180, 45, 128, 204, 102, 151, 180, 184, 90, 37, 89, 115, 143, 166, 12, 223, 72, 172, 230, 204, 2, 23, 148, 46, 41, 7, 32, 178, 26, 78, 118, 121, 204, 121, 86, 241, 160, 64, 175, 227, 162, 249, 18, 134, 100, 46, 212, 180, 133, 197, 191, 171, 3, 166, 127, 72, 8, 112, 143, 122, 135, 177, 203, 253, 127, 59, 209, 98, 180, 30, 134, 148, 231, 102, 70, 81, 194, 252, 203, 171, 105, 133, 202, 202, 8, 71, 57, 75, 153, 178, 59, 63, 230, 108, 232, 55, 241, 121, 205, 29, 17, 136, 160, 249, 168, 192, 41, 30, 213, 117, 47, 109, 51, 213, 176, 182, 135, 29, 77, 125, 251 }, "0921231220", 1, "quoctuan" }
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
                    { 1, 1, new DateTime(2021, 11, 1, 14, 35, 37, 929, DateTimeKind.Local).AddTicks(5642), 2, 4 },
                    { 2, 1, new DateTime(2021, 11, 1, 14, 35, 37, 929, DateTimeKind.Local).AddTicks(6230), 4, 4 },
                    { 3, 1, new DateTime(2021, 11, 1, 14, 35, 37, 929, DateTimeKind.Local).AddTicks(6234), 3, 4 }
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
                name: "ResetPasses");

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

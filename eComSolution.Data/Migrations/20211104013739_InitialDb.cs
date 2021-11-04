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
                    { 1, "123 DT605, Hòa Tiến, Hòa Vang, Đà Nẵng", "", new DateTime(2021, 11, 4, 8, 37, 39, 49, DateTimeKind.Local).AddTicks(620), new DateTime(2021, 11, 4, 8, 37, 39, 50, DateTimeKind.Local).AddTicks(2113), "Cửa hàng áo quần chất lượng cao, giá cả phải chăng", false, "Tuấn's Fashion", "0921231220" },
                    { 2, "160 Trần Nhật Duật, Cẩm Châu, Hội An, Quảng Nam", "", new DateTime(2021, 11, 4, 8, 37, 39, 50, DateTimeKind.Local).AddTicks(3213), new DateTime(2021, 11, 4, 8, 37, 39, 50, DateTimeKind.Local).AddTicks(3220), "Cửa hàng áo quần nam", false, "Đạt's Clothes", "0905553859" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Disable", "Email", "Fullname", "PasswordHash", "PasswordSalt", "PhoneNumber", "ShopId", "Username" },
                values: new object[,]
                {
                    { 1, null, false, "tuandang29042000@gmail.com", "admin", new byte[] { 22, 168, 128, 70, 137, 56, 251, 54, 75, 237, 251, 152, 193, 30, 14, 23, 96, 238, 63, 141, 103, 146, 171, 87, 5, 27, 250, 49, 164, 61, 63, 137, 42, 43, 86, 230, 116, 40, 50, 110, 222, 24, 156, 227, 94, 124, 169, 143, 31, 206, 247, 211, 226, 18, 208, 31, 249, 107, 196, 110, 178, 157, 204, 200 }, new byte[] { 217, 99, 111, 254, 97, 160, 15, 152, 104, 227, 62, 226, 191, 250, 205, 210, 118, 8, 139, 134, 75, 71, 140, 23, 195, 107, 8, 11, 159, 187, 13, 80, 74, 80, 102, 249, 186, 15, 241, 21, 155, 65, 134, 212, 250, 97, 161, 149, 52, 125, 153, 245, 116, 87, 182, 150, 82, 86, 101, 135, 158, 7, 68, 159, 39, 49, 240, 202, 181, 228, 71, 121, 63, 16, 246, 84, 75, 132, 249, 0, 174, 246, 199, 238, 89, 50, 173, 103, 213, 72, 31, 177, 199, 255, 248, 41, 172, 222, 62, 83, 231, 125, 198, 64, 137, 26, 66, 233, 193, 61, 14, 180, 163, 233, 62, 177, 170, 210, 119, 189, 238, 196, 214, 201, 126, 221, 85, 81 }, "0921231220", null, "admin" },
                    { 4, "123 Lê Duẩn, Thanh Khê, Đà Nẵng", false, "badding@gmail.com", "Đinh Công Tài", new byte[] { 30, 145, 14, 49, 35, 217, 55, 99, 231, 242, 85, 110, 134, 6, 250, 68, 225, 15, 30, 251, 176, 235, 55, 171, 30, 5, 180, 57, 69, 255, 52, 168, 110, 198, 14, 252, 137, 52, 130, 74, 107, 27, 149, 51, 126, 89, 240, 189, 68, 251, 55, 102, 179, 161, 148, 97, 183, 57, 141, 209, 45, 170, 160, 46 }, new byte[] { 217, 99, 111, 254, 97, 160, 15, 152, 104, 227, 62, 226, 191, 250, 205, 210, 118, 8, 139, 134, 75, 71, 140, 23, 195, 107, 8, 11, 159, 187, 13, 80, 74, 80, 102, 249, 186, 15, 241, 21, 155, 65, 134, 212, 250, 97, 161, 149, 52, 125, 153, 245, 116, 87, 182, 150, 82, 86, 101, 135, 158, 7, 68, 159, 39, 49, 240, 202, 181, 228, 71, 121, 63, 16, 246, 84, 75, 132, 249, 0, 174, 246, 199, 238, 89, 50, 173, 103, 213, 72, 31, 177, 199, 255, 248, 41, 172, 222, 62, 83, 231, 125, 198, 64, 137, 26, 66, 233, 193, 61, 14, 180, 163, 233, 62, 177, 170, 210, 119, 189, 238, 196, 214, 201, 126, 221, 85, 81 }, "090553859", null, "congtai" }
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
                values: new object[] { 1, new DateTime(2021, 11, 4, 8, 37, 39, 57, DateTimeKind.Local).AddTicks(2316), "123 Lê Duẩn, Thanh Khê, Đà Nẵng", "Đinh Công Tài", "09053438847", 2, "Chờ xử lý", 4 });

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
                    { 1, 1, new DateTime(2021, 11, 4, 8, 37, 39, 56, DateTimeKind.Local).AddTicks(3276), "Mô tả sản phẩm:\n Chất liệu: 100% cotton\n Màu sắc : trắng & đen", 1, "Áo thun SadBoiz", 100000, 200000, 1 },
                    { 2, 5, new DateTime(2021, 11, 4, 8, 37, 39, 56, DateTimeKind.Local).AddTicks(5227), "Mô tả sản phẩm:\n Chất liệu: 100% cotton\n Màu sắc : đỏ & đen", 1, "Quần lót Calvin Klein", 100000, 150000, 1 },
                    { 3, 1, new DateTime(2021, 11, 4, 8, 37, 39, 56, DateTimeKind.Local).AddTicks(5234), "Mô tả sản phẩm:\n Brand: XFire\n Chất liệu : cotton co dãn", 3, "Áo thun trơn XFire", 150000, 250000, 2 },
                    { 4, 4, new DateTime(2021, 11, 4, 8, 37, 39, 56, DateTimeKind.Local).AddTicks(5237), "Mô tả sản phẩm:\n Form: Fit\n Chất liệu : jean", 1, "Quần Jean ôm nam", 150000, 250000, 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Disable", "Email", "Fullname", "PasswordHash", "PasswordSalt", "PhoneNumber", "ShopId", "Username" },
                values: new object[,]
                {
                    { 3, "160 Trần Nhật Duật, Hội An, Quảng Nam", false, "ngoluuquocdat@gmail.com", "Ngô Lưu Quốc Đạt", new byte[] { 192, 28, 123, 191, 88, 105, 139, 30, 168, 18, 244, 218, 123, 74, 98, 54, 195, 192, 22, 121, 28, 55, 195, 35, 167, 214, 98, 35, 216, 208, 101, 196, 159, 188, 224, 177, 235, 43, 118, 140, 166, 64, 181, 211, 240, 111, 141, 215, 222, 46, 240, 187, 184, 194, 21, 88, 21, 177, 159, 217, 43, 28, 182, 91 }, new byte[] { 217, 99, 111, 254, 97, 160, 15, 152, 104, 227, 62, 226, 191, 250, 205, 210, 118, 8, 139, 134, 75, 71, 140, 23, 195, 107, 8, 11, 159, 187, 13, 80, 74, 80, 102, 249, 186, 15, 241, 21, 155, 65, 134, 212, 250, 97, 161, 149, 52, 125, 153, 245, 116, 87, 182, 150, 82, 86, 101, 135, 158, 7, 68, 159, 39, 49, 240, 202, 181, 228, 71, 121, 63, 16, 246, 84, 75, 132, 249, 0, 174, 246, 199, 238, 89, 50, 173, 103, 213, 72, 31, 177, 199, 255, 248, 41, 172, 222, 62, 83, 231, 125, 198, 64, 137, 26, 66, 233, 193, 61, 14, 180, 163, 233, 62, 177, 170, 210, 119, 189, 238, 196, 214, 201, 126, 221, 85, 81 }, "090553859", 2, "quocdat" },
                    { 2, "123 DT605, Hòa Tiến, Hòa Vang, Đà Nẵng", false, "tuandang29042000@gmail.com", "Đặng Quốc Tuấn", new byte[] { 39, 18, 226, 144, 140, 16, 198, 248, 189, 10, 100, 28, 208, 227, 89, 87, 134, 155, 127, 3, 34, 15, 145, 199, 53, 171, 123, 155, 154, 49, 26, 46, 148, 57, 182, 32, 21, 74, 117, 253, 213, 236, 145, 62, 137, 179, 169, 129, 240, 208, 35, 52, 105, 215, 47, 114, 193, 106, 170, 32, 43, 119, 129, 179 }, new byte[] { 217, 99, 111, 254, 97, 160, 15, 152, 104, 227, 62, 226, 191, 250, 205, 210, 118, 8, 139, 134, 75, 71, 140, 23, 195, 107, 8, 11, 159, 187, 13, 80, 74, 80, 102, 249, 186, 15, 241, 21, 155, 65, 134, 212, 250, 97, 161, 149, 52, 125, 153, 245, 116, 87, 182, 150, 82, 86, 101, 135, 158, 7, 68, 159, 39, 49, 240, 202, 181, 228, 71, 121, 63, 16, 246, 84, 75, 132, 249, 0, 174, 246, 199, 238, 89, 50, 173, 103, 213, 72, 31, 177, 199, 255, 248, 41, 172, 222, 62, 83, 231, 125, 198, 64, 137, 26, 66, 233, 193, 61, 14, 180, 163, 233, 62, 177, 170, 210, 119, 189, 238, 196, 214, 201, 126, 221, 85, 81 }, "0921231220", 1, "quoctuan" }
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
                    { 1, 1, new DateTime(2021, 11, 4, 8, 37, 39, 56, DateTimeKind.Local).AddTicks(8639), 2, 4 },
                    { 2, 1, new DateTime(2021, 11, 4, 8, 37, 39, 56, DateTimeKind.Local).AddTicks(9222), 4, 4 },
                    { 3, 1, new DateTime(2021, 11, 4, 8, 37, 39, 56, DateTimeKind.Local).AddTicks(9227), 3, 4 }
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

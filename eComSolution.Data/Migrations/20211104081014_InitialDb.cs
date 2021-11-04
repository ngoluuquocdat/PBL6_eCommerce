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
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CancelReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    { 1, "123 DT605, Hòa Tiến, Hòa Vang, Đà Nẵng", "", new DateTime(2021, 11, 4, 15, 10, 13, 704, DateTimeKind.Local).AddTicks(5936), new DateTime(2021, 11, 4, 15, 10, 13, 705, DateTimeKind.Local).AddTicks(5635), "Cửa hàng áo quần chất lượng cao, giá cả phải chăng", false, "Tuấn's Fashion", "0921231220" },
                    { 2, "160 Trần Nhật Duật, Cẩm Châu, Hội An, Quảng Nam", "", new DateTime(2021, 11, 4, 15, 10, 13, 705, DateTimeKind.Local).AddTicks(6396), new DateTime(2021, 11, 4, 15, 10, 13, 705, DateTimeKind.Local).AddTicks(6402), "Cửa hàng áo quần nam", false, "Đạt's Clothes", "0905553859" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Disable", "Email", "Fullname", "PasswordHash", "PasswordSalt", "PhoneNumber", "ShopId", "Username" },
                values: new object[,]
                {
                    { 1, null, false, "tuandang29042000@gmail.com", "admin", new byte[] { 126, 208, 99, 57, 66, 169, 170, 241, 7, 93, 187, 165, 142, 88, 104, 27, 229, 39, 13, 165, 135, 29, 70, 90, 1, 60, 19, 146, 167, 249, 198, 66, 92, 172, 209, 200, 33, 212, 146, 38, 53, 196, 150, 96, 89, 67, 213, 102, 190, 217, 42, 190, 118, 14, 221, 208, 2, 110, 135, 157, 231, 163, 248, 42 }, new byte[] { 249, 45, 232, 251, 29, 246, 235, 13, 23, 88, 254, 193, 249, 127, 88, 249, 36, 255, 234, 35, 78, 124, 210, 73, 15, 89, 34, 101, 249, 52, 50, 12, 131, 252, 78, 53, 152, 225, 124, 24, 91, 127, 78, 194, 71, 100, 43, 92, 181, 89, 93, 18, 129, 200, 50, 246, 254, 106, 134, 117, 155, 94, 144, 169, 35, 245, 213, 100, 55, 65, 42, 126, 109, 90, 122, 7, 255, 63, 53, 134, 77, 106, 80, 147, 203, 11, 13, 131, 232, 172, 203, 137, 18, 56, 188, 58, 92, 248, 124, 71, 182, 181, 17, 36, 11, 177, 189, 108, 61, 209, 212, 38, 223, 244, 48, 166, 90, 244, 98, 30, 186, 180, 85, 37, 175, 200, 100, 75 }, "0921231220", null, "admin" },
                    { 4, "123 Lê Duẩn, Thanh Khê, Đà Nẵng", false, "badding@gmail.com", "Đinh Công Tài", new byte[] { 121, 134, 2, 14, 214, 150, 158, 119, 1, 205, 184, 43, 33, 189, 138, 85, 13, 103, 99, 15, 62, 234, 119, 81, 66, 109, 109, 130, 74, 215, 197, 240, 51, 168, 210, 208, 17, 25, 125, 128, 25, 255, 215, 18, 229, 48, 232, 110, 26, 163, 50, 205, 60, 11, 178, 237, 108, 161, 148, 33, 8, 63, 68, 208 }, new byte[] { 249, 45, 232, 251, 29, 246, 235, 13, 23, 88, 254, 193, 249, 127, 88, 249, 36, 255, 234, 35, 78, 124, 210, 73, 15, 89, 34, 101, 249, 52, 50, 12, 131, 252, 78, 53, 152, 225, 124, 24, 91, 127, 78, 194, 71, 100, 43, 92, 181, 89, 93, 18, 129, 200, 50, 246, 254, 106, 134, 117, 155, 94, 144, 169, 35, 245, 213, 100, 55, 65, 42, 126, 109, 90, 122, 7, 255, 63, 53, 134, 77, 106, 80, 147, 203, 11, 13, 131, 232, 172, 203, 137, 18, 56, 188, 58, 92, 248, 124, 71, 182, 181, 17, 36, 11, 177, 189, 108, 61, 209, 212, 38, 223, 244, 48, 166, 90, 244, 98, 30, 186, 180, 85, 37, 175, 200, 100, 75 }, "090553859", null, "congtai" }
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
                columns: new[] { "Id", "CancelReason", "DateModified", "OrderDate", "ShipAddress", "ShipName", "ShipPhone", "ShopId", "State", "UserId" },
                values: new object[] { 1, null, new DateTime(2021, 11, 4, 15, 10, 13, 711, DateTimeKind.Local).AddTicks(1884), new DateTime(2021, 11, 4, 15, 10, 13, 711, DateTimeKind.Local).AddTicks(1500), "123 Lê Duẩn, Thanh Khê, Đà Nẵng", "Đinh Công Tài", "09053438847", 2, "Chờ xử lý", 4 });

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
                    { 1, 1, new DateTime(2021, 11, 4, 15, 10, 13, 710, DateTimeKind.Local).AddTicks(3429), "Mô tả sản phẩm:\n Chất liệu: 100% cotton\n Màu sắc : trắng & đen", 1, "Áo thun SadBoiz", 100000, 200000, 1 },
                    { 2, 5, new DateTime(2021, 11, 4, 15, 10, 13, 710, DateTimeKind.Local).AddTicks(5312), "Mô tả sản phẩm:\n Chất liệu: 100% cotton\n Màu sắc : đỏ & đen", 1, "Quần lót Calvin Klein", 100000, 150000, 1 },
                    { 3, 1, new DateTime(2021, 11, 4, 15, 10, 13, 710, DateTimeKind.Local).AddTicks(5320), "Mô tả sản phẩm:\n Brand: XFire\n Chất liệu : cotton co dãn", 3, "Áo thun trơn XFire", 150000, 250000, 2 },
                    { 4, 4, new DateTime(2021, 11, 4, 15, 10, 13, 710, DateTimeKind.Local).AddTicks(5322), "Mô tả sản phẩm:\n Form: Fit\n Chất liệu : jean", 1, "Quần Jean ôm nam", 150000, 250000, 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Disable", "Email", "Fullname", "PasswordHash", "PasswordSalt", "PhoneNumber", "ShopId", "Username" },
                values: new object[,]
                {
                    { 3, "160 Trần Nhật Duật, Hội An, Quảng Nam", false, "ngoluuquocdat@gmail.com", "Ngô Lưu Quốc Đạt", new byte[] { 88, 153, 80, 29, 204, 114, 112, 42, 206, 222, 90, 108, 170, 155, 105, 187, 214, 157, 81, 117, 44, 202, 173, 132, 105, 249, 62, 1, 186, 219, 155, 210, 82, 177, 223, 21, 122, 143, 47, 244, 224, 41, 244, 114, 60, 166, 196, 189, 143, 217, 81, 42, 31, 217, 230, 26, 121, 221, 149, 134, 249, 124, 244, 43 }, new byte[] { 249, 45, 232, 251, 29, 246, 235, 13, 23, 88, 254, 193, 249, 127, 88, 249, 36, 255, 234, 35, 78, 124, 210, 73, 15, 89, 34, 101, 249, 52, 50, 12, 131, 252, 78, 53, 152, 225, 124, 24, 91, 127, 78, 194, 71, 100, 43, 92, 181, 89, 93, 18, 129, 200, 50, 246, 254, 106, 134, 117, 155, 94, 144, 169, 35, 245, 213, 100, 55, 65, 42, 126, 109, 90, 122, 7, 255, 63, 53, 134, 77, 106, 80, 147, 203, 11, 13, 131, 232, 172, 203, 137, 18, 56, 188, 58, 92, 248, 124, 71, 182, 181, 17, 36, 11, 177, 189, 108, 61, 209, 212, 38, 223, 244, 48, 166, 90, 244, 98, 30, 186, 180, 85, 37, 175, 200, 100, 75 }, "090553859", 2, "quocdat" },
                    { 2, "123 DT605, Hòa Tiến, Hòa Vang, Đà Nẵng", false, "tuandang29042000@gmail.com", "Đặng Quốc Tuấn", new byte[] { 158, 231, 143, 223, 29, 20, 180, 98, 78, 129, 63, 67, 17, 204, 90, 62, 85, 236, 110, 161, 148, 151, 121, 108, 201, 196, 234, 116, 78, 229, 204, 104, 115, 33, 113, 153, 119, 34, 244, 68, 140, 206, 241, 58, 69, 44, 164, 41, 127, 26, 165, 161, 117, 175, 147, 199, 110, 36, 106, 237, 217, 13, 105, 184 }, new byte[] { 249, 45, 232, 251, 29, 246, 235, 13, 23, 88, 254, 193, 249, 127, 88, 249, 36, 255, 234, 35, 78, 124, 210, 73, 15, 89, 34, 101, 249, 52, 50, 12, 131, 252, 78, 53, 152, 225, 124, 24, 91, 127, 78, 194, 71, 100, 43, 92, 181, 89, 93, 18, 129, 200, 50, 246, 254, 106, 134, 117, 155, 94, 144, 169, 35, 245, 213, 100, 55, 65, 42, 126, 109, 90, 122, 7, 255, 63, 53, 134, 77, 106, 80, 147, 203, 11, 13, 131, 232, 172, 203, 137, 18, 56, 188, 58, 92, 248, 124, 71, 182, 181, 17, 36, 11, 177, 189, 108, 61, 209, 212, 38, 223, 244, 48, 166, 90, 244, 98, 30, 186, 180, 85, 37, 175, 200, 100, 75 }, "0921231220", 1, "quoctuan" }
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
                    { 1, 1, new DateTime(2021, 11, 4, 15, 10, 13, 710, DateTimeKind.Local).AddTicks(8423), 2, 4 },
                    { 2, 1, new DateTime(2021, 11, 4, 15, 10, 13, 710, DateTimeKind.Local).AddTicks(9006), 4, 4 },
                    { 3, 1, new DateTime(2021, 11, 4, 15, 10, 13, 710, DateTimeKind.Local).AddTicks(9011), 3, 4 }
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

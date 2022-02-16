using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eComSolution.Data.Migrations
{
    public partial class InitDb : Migration
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
                    DisableReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Quantity = table.Column<int>(type: "int", nullable: false)
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
                columns: new[] { "Id", "Address", "Avatar", "DateCreated", "DateModified", "Description", "Disable", "DisableReason", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "123 DT605, Hòa Tiến, Hòa Vang, Đà Nẵng", "", new DateTime(2021, 11, 10, 10, 2, 51, 812, DateTimeKind.Local).AddTicks(8087), new DateTime(2021, 11, 10, 10, 2, 51, 813, DateTimeKind.Local).AddTicks(5408), "Cửa hàng áo quần chất lượng cao, giá cả phải chăng", false, null, "Tuấn's Fashion", "0921231220" },
                    { 2, "160 Trần Nhật Duật, Cẩm Châu, Hội An, Quảng Nam", "", new DateTime(2021, 11, 10, 10, 2, 51, 813, DateTimeKind.Local).AddTicks(6118), new DateTime(2021, 11, 10, 10, 2, 51, 813, DateTimeKind.Local).AddTicks(6122), "Cửa hàng áo quần nam", false, null, "Đạt's Clothes", "0905553859" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Disable", "Email", "Fullname", "PasswordHash", "PasswordSalt", "PhoneNumber", "ShopId", "Username" },
                values: new object[,]
                {
                    { 1, null, false, "tuandang29042000@gmail.com", "admin", new byte[] { 121, 186, 32, 3, 50, 116, 233, 254, 3, 61, 56, 22, 58, 73, 141, 127, 60, 70, 128, 88, 143, 16, 20, 6, 201, 103, 73, 209, 200, 208, 110, 210, 229, 99, 39, 110, 186, 149, 245, 236, 152, 198, 159, 11, 73, 35, 115, 151, 101, 250, 45, 190, 232, 249, 72, 12, 115, 204, 181, 138, 138, 56, 216, 145 }, new byte[] { 180, 66, 173, 108, 108, 108, 103, 196, 143, 173, 157, 3, 230, 163, 86, 85, 150, 89, 115, 199, 84, 56, 182, 56, 59, 248, 226, 208, 145, 84, 207, 178, 230, 104, 3, 39, 241, 230, 227, 82, 80, 236, 132, 157, 198, 146, 246, 239, 185, 117, 87, 228, 43, 228, 223, 181, 140, 111, 110, 217, 169, 231, 127, 178, 50, 255, 86, 189, 120, 41, 64, 173, 48, 60, 182, 245, 115, 143, 39, 101, 199, 5, 54, 230, 243, 97, 142, 252, 47, 30, 20, 82, 27, 93, 193, 16, 65, 45, 183, 75, 215, 97, 100, 111, 119, 125, 233, 123, 132, 51, 191, 112, 8, 207, 208, 30, 32, 85, 133, 210, 115, 141, 169, 97, 143, 87, 213, 8 }, "0921231220", null, "admin" },
                    { 4, "123 Lê Duẩn, Thanh Khê, Đà Nẵng", false, "badding@gmail.com", "Đinh Công Tài", new byte[] { 97, 75, 167, 213, 241, 173, 89, 32, 168, 199, 146, 143, 110, 85, 145, 182, 220, 160, 16, 151, 23, 248, 206, 236, 152, 58, 223, 231, 20, 167, 22, 136, 214, 64, 228, 212, 72, 81, 156, 162, 125, 184, 93, 85, 7, 102, 19, 233, 154, 220, 245, 14, 122, 85, 46, 148, 182, 235, 209, 96, 189, 235, 56, 44 }, new byte[] { 180, 66, 173, 108, 108, 108, 103, 196, 143, 173, 157, 3, 230, 163, 86, 85, 150, 89, 115, 199, 84, 56, 182, 56, 59, 248, 226, 208, 145, 84, 207, 178, 230, 104, 3, 39, 241, 230, 227, 82, 80, 236, 132, 157, 198, 146, 246, 239, 185, 117, 87, 228, 43, 228, 223, 181, 140, 111, 110, 217, 169, 231, 127, 178, 50, 255, 86, 189, 120, 41, 64, 173, 48, 60, 182, 245, 115, 143, 39, 101, 199, 5, 54, 230, 243, 97, 142, 252, 47, 30, 20, 82, 27, 93, 193, 16, 65, 45, 183, 75, 215, 97, 100, 111, 119, 125, 233, 123, 132, 51, 191, 112, 8, 207, 208, 30, 32, 85, 133, 210, 115, 141, 169, 97, 143, 87, 213, 8 }, "090553859", null, "congtai" }
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
                values: new object[] { 1, null, new DateTime(2021, 11, 10, 10, 2, 51, 817, DateTimeKind.Local).AddTicks(9896), new DateTime(2021, 11, 10, 10, 2, 51, 817, DateTimeKind.Local).AddTicks(9642), "123 Lê Duẩn, Thanh Khê, Đà Nẵng", "Đinh Công Tài", "09053438847", 2, "Chờ xử lý", 4 });

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
                    { 1, 1, new DateTime(2021, 11, 10, 10, 2, 51, 817, DateTimeKind.Local).AddTicks(3112), "Mô tả sản phẩm:\n Chất liệu: 100% cotton\n Màu sắc : trắng & đen", 1, "Áo thun SadBoiz", 100000, 200000, 1 },
                    { 2, 5, new DateTime(2021, 11, 10, 10, 2, 51, 817, DateTimeKind.Local).AddTicks(4602), "Mô tả sản phẩm:\n Chất liệu: 100% cotton\n Màu sắc : đỏ & đen", 1, "Quần lót Calvin Klein", 100000, 150000, 1 },
                    { 3, 1, new DateTime(2021, 11, 10, 10, 2, 51, 817, DateTimeKind.Local).AddTicks(4607), "Mô tả sản phẩm:\n Brand: XFire\n Chất liệu : cotton co dãn", 3, "Áo thun trơn XFire", 150000, 250000, 2 },
                    { 4, 4, new DateTime(2021, 11, 10, 10, 2, 51, 817, DateTimeKind.Local).AddTicks(4609), "Mô tả sản phẩm:\n Form: Fit\n Chất liệu : jean", 1, "Quần Jean ôm nam", 150000, 250000, 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Disable", "Email", "Fullname", "PasswordHash", "PasswordSalt", "PhoneNumber", "ShopId", "Username" },
                values: new object[,]
                {
                    { 3, "160 Trần Nhật Duật, Hội An, Quảng Nam", false, "ngoluuquocdat@gmail.com", "Ngô Lưu Quốc Đạt", new byte[] { 135, 116, 199, 203, 97, 178, 52, 219, 250, 139, 22, 56, 196, 191, 110, 29, 90, 168, 159, 185, 71, 64, 137, 139, 199, 72, 132, 230, 101, 1, 195, 235, 156, 111, 16, 60, 13, 1, 110, 86, 49, 235, 78, 124, 239, 74, 139, 18, 222, 11, 209, 15, 92, 97, 2, 134, 20, 34, 222, 237, 234, 183, 134, 223 }, new byte[] { 180, 66, 173, 108, 108, 108, 103, 196, 143, 173, 157, 3, 230, 163, 86, 85, 150, 89, 115, 199, 84, 56, 182, 56, 59, 248, 226, 208, 145, 84, 207, 178, 230, 104, 3, 39, 241, 230, 227, 82, 80, 236, 132, 157, 198, 146, 246, 239, 185, 117, 87, 228, 43, 228, 223, 181, 140, 111, 110, 217, 169, 231, 127, 178, 50, 255, 86, 189, 120, 41, 64, 173, 48, 60, 182, 245, 115, 143, 39, 101, 199, 5, 54, 230, 243, 97, 142, 252, 47, 30, 20, 82, 27, 93, 193, 16, 65, 45, 183, 75, 215, 97, 100, 111, 119, 125, 233, 123, 132, 51, 191, 112, 8, 207, 208, 30, 32, 85, 133, 210, 115, 141, 169, 97, 143, 87, 213, 8 }, "090553859", 2, "quocdat" },
                    { 2, "123 DT605, Hòa Tiến, Hòa Vang, Đà Nẵng", false, "tuandang29042000@gmail.com", "Đặng Quốc Tuấn", new byte[] { 4, 150, 156, 46, 123, 214, 191, 29, 222, 48, 140, 244, 52, 206, 105, 89, 46, 36, 113, 239, 252, 104, 39, 148, 3, 90, 120, 162, 30, 201, 109, 234, 244, 140, 40, 47, 232, 36, 43, 83, 104, 181, 125, 198, 247, 22, 251, 74, 139, 91, 157, 249, 165, 100, 56, 139, 97, 81, 173, 110, 250, 196, 95, 185 }, new byte[] { 180, 66, 173, 108, 108, 108, 103, 196, 143, 173, 157, 3, 230, 163, 86, 85, 150, 89, 115, 199, 84, 56, 182, 56, 59, 248, 226, 208, 145, 84, 207, 178, 230, 104, 3, 39, 241, 230, 227, 82, 80, 236, 132, 157, 198, 146, 246, 239, 185, 117, 87, 228, 43, 228, 223, 181, 140, 111, 110, 217, 169, 231, 127, 178, 50, 255, 86, 189, 120, 41, 64, 173, 48, 60, 182, 245, 115, 143, 39, 101, 199, 5, 54, 230, 243, 97, 142, 252, 47, 30, 20, 82, 27, 93, 193, 16, 65, 45, 183, 75, 215, 97, 100, 111, 119, 125, 233, 123, 132, 51, 191, 112, 8, 207, 208, 30, 32, 85, 133, 210, 115, 141, 169, 97, 143, 87, 213, 8 }, "0921231220", 1, "quoctuan" }
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
                    { 1, 1, new DateTime(2021, 11, 10, 10, 2, 51, 817, DateTimeKind.Local).AddTicks(7101), 2, 4 },
                    { 2, 1, new DateTime(2021, 11, 10, 10, 2, 51, 817, DateTimeKind.Local).AddTicks(7558), 4, 4 },
                    { 3, 1, new DateTime(2021, 11, 10, 10, 2, 51, 817, DateTimeKind.Local).AddTicks(7562), 3, 4 }
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
                columns: new[] { "Id", "ProductDetail_Id", "Quantity", "UserId" },
                values: new object[] { 1, 8, 10, 4 });

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

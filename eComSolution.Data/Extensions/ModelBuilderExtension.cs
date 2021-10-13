using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using eComSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace eComSolution.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {      
            // data seeding cho Shops
            modelBuilder.Entity<Shop>().HasData(
                new Shop
                {
                    Id = 1,
                    Name = "Tuấn's Fashion",
                    Avatar = "",
                    PhoneNumber = "0921231220",
                    Address = "123 DT605, Hòa Tiến, Hòa Vang, Đà Nẵng",
                    Description = "Cửa hàng áo quần chất lượng cao, giá cả phải chăng",
                    Disable = false
                },
                new Shop
                {
                    Id = 2,
                    Name = "Đạt's Clothes",
                    Avatar = "",
                    PhoneNumber = "0905553859",
                    Address = "160 Trần Nhật Duật, Cẩm Châu, Hội An, Quảng Nam",
                    Description = "Cửa hàng áo quần nam",
                    Disable = false
                }
            );  

            // data seeding cho Users
            using var hmac = new HMACSHA512();  // thuật toán mã hóa
            
            modelBuilder.Entity<User>().HasData(
                 new User
                {
                    Id = 1,
                    Fullname = "admin",
                    Email = "tuandang29042000@gmail.com",
                    PhoneNumber = "0921231220",
                    Username = "admin",
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Admin123!")),
                    PasswordSalt = hmac.Key,
                    ShopId = null,
                    Disable = false
                },
                new User
                {
                    Id = 2,
                    Fullname = "Đặng Quốc Tuấn",
                    Email = "tuandang29042000@gmail.com",
                    PhoneNumber = "0921231220",
                    Username = "quoctuan",
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Quoctuan123!")),
                    PasswordSalt = hmac.Key,
                    ShopId = 1,
                    Disable = false
                },
                new User
                {
                    Id = 3,
                    Fullname = "Ngô Lưu Quốc Đạt",
                    Email = "ngoluuquocdat@gmail.com",
                    PhoneNumber = "090553859",
                    Username = "quocdat",
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Quocdat123!")),
                    PasswordSalt = hmac.Key,
                    ShopId = 2,
                    Disable = false
                },
                new User
                {
                    Id = 4,
                    Fullname = "Đinh Công Tài",
                    Email = "badding@gmail.com",
                    PhoneNumber = "090553859",
                    Username = "congtai",
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Congtai123!")),
                    PasswordSalt = hmac.Key,
                    ShopId = null,
                    Disable = false
                }
            );

            // data seeding cho Groups
            modelBuilder.Entity<Group>().HasData(
                new Group
                {
                    Id = 1,
                    Name = "Admin"
                },
                new Group
                {
                    Id = 2,
                    Name = "Mod"
                },
                new Group
                {
                    Id = 3,
                    Name = "Member"
                }
            );

            // data seeding cho GroupUsers
            modelBuilder.Entity<GroupUser>().HasData(
                new GroupUser{ UserId = 1, GroupId = 1},
                new GroupUser{ UserId = 1, GroupId = 2},
                new GroupUser{ UserId = 1, GroupId = 3},
                new GroupUser{ UserId = 2, GroupId = 2},
                new GroupUser{ UserId = 2, GroupId = 3},
                new GroupUser{ UserId = 3, GroupId = 2},
                new GroupUser{ UserId = 3, GroupId = 3},
                new GroupUser{ UserId = 4, GroupId = 3}
            );

            // data seeding cho Functions
            modelBuilder.Entity<Function>().HasData(
                new Function{ Id = 1, ActionName = "Register"},
                new Function{ Id = 2, ActionName = "Login"}
            );

            // data seeding cho Permissions
            modelBuilder.Entity<Permission>().HasData(
                new Permission{ GroupId = 1, FunctionId = 1},
                new Permission{ GroupId = 1, FunctionId = 2},
                new Permission{ GroupId = 2, FunctionId = 1},
                new Permission{ GroupId = 2, FunctionId = 2},
                new Permission{ GroupId = 3, FunctionId = 1},
                new Permission{ GroupId = 3, FunctionId = 2}
            );

            // data seeding cho Categories
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Áo Thun",
                    SortOrder = 1   
                },
                new Category
                {
                    Id = 2,
                    Name = "Áo Sơ mi",
                    SortOrder = 2                  
                },
                new Category
                {
                    Id = 3,
                    Name = "Áo Hoodie",
                    SortOrder = 3                  
                },
                new Category
                {
                    Id = 4,
                    Name = "Quần Jean",
                    SortOrder = 4                  
                },
                new Category
                {
                    Id = 5,
                    Name = "Đồ lót",
                    SortOrder = 5                  
                }
            );

            // data seeding cho Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Áo thun SadBoiz",
                    Description="Mô tả sản phẩm:"+'\n'+" Chất liệu: 100% cotton"+'\n'+" Màu sắc : trắng & đen",
                    DateCreated = DateTime.Now,
                    OriginalPrice = 100000,
                    Price = 200000,
                    ViewCount = 0,
                    CategoryId = 1,
                    ShopId = 1,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 2,
                    Name = "Quần lót Calvin Klein",
                    Description="Mô tả sản phẩm:"+'\n'+" Chất liệu: 100% cotton"+'\n'+" Màu sắc : đỏ & đen",
                    DateCreated = DateTime.Now,
                    OriginalPrice = 100000,
                    Price = 150000,
                    ViewCount = 0,
                    CategoryId = 5,
                    ShopId = 1,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 3,
                    Name = "Áo thun trơn XFire",
                    Description="Mô tả sản phẩm:"+'\n'+" Brand: XFire"+'\n'+" Chất liệu : cotton co dãn",
                    DateCreated = DateTime.Now,
                    OriginalPrice = 150000,
                    Price = 250000,
                    ViewCount = 0,
                    CategoryId = 1,
                    ShopId = 2,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 4,
                    Name = "Quần Jean ôm nam",
                    Description="Mô tả sản phẩm:"+'\n'+" Form: Fit"+'\n'+" Chất liệu : jean",
                    DateCreated = DateTime.Now,
                    OriginalPrice = 150000,
                    Price = 250000,
                    ViewCount = 0,
                    CategoryId = 4,
                    ShopId = 2,
                    IsDeleted = false
                }
            );

            // data seeding cho product details
            modelBuilder.Entity<ProductDetail>().HasData(
                new ProductDetail{Id = 1, ProductId = 1, Color = "Đen", Size = "XL", Stock = 10},
                new ProductDetail{Id = 2, ProductId = 1, Color = "Đen", Size = "L", Stock = 10},
                new ProductDetail{Id = 3, ProductId = 1, Color = "Trắng", Size = "XL", Stock = 10},
                new ProductDetail{Id = 4, ProductId = 1, Color = "Trắng", Size = "L", Stock = 10},
                new ProductDetail{Id = 5, ProductId = 2, Color = "Đen", Size = "L", Stock = 10},
                new ProductDetail{Id = 6, ProductId = 2, Color = "Đen", Size = "M", Stock = 10},
                new ProductDetail{Id = 7, ProductId = 2, Color = "Đỏ", Size = "L", Stock = 10},
                new ProductDetail{Id = 8, ProductId = 2, Color = "Đỏ", Size = "M", Stock = 10},
                new ProductDetail{Id = 9, ProductId = 3, Color = "Đỏ", Size = "XL", Stock = 10},
                new ProductDetail{Id = 10, ProductId = 3, Color = "Đỏ", Size = "L", Stock = 10},
                new ProductDetail{Id = 11, ProductId = 3, Color = "Xám", Size = "XL", Stock = 10},
                new ProductDetail{Id = 12, ProductId = 3, Color = "Xám", Size = "L", Stock = 10},
                new ProductDetail{Id = 13, ProductId = 4, Color = "Đen", Size = "XL", Stock = 10},
                new ProductDetail{Id = 14, ProductId = 4, Color = "Đen", Size = "L", Stock = 10}                
            );

            // data seeding cho histories
            modelBuilder.Entity<History>().HasData(
                new History{Id = 1, UserId = 4, ProductId = 2, Date=DateTime.Now, Count=1},
                new History{Id = 2, UserId = 4, ProductId = 4, Date=DateTime.Now, Count=1},
                new History{Id = 3, UserId = 4, ProductId = 3, Date=DateTime.Now, Count=1}
            );

            // data seeding cho carts
            modelBuilder.Entity<Cart>().HasData(
                new Cart{Id = 1, UserId = 4, ProductDetail_Id = 8, Quantity=10, Price=150000}
            );
            
            // data seeding cho Order
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    OrderDate = DateTime.Now,
                    UserId = 4,
                    ShopId = 2,
                    State = "Đang xử lý",
                    ShipName = "Đinh Công Tài",
                    ShipAddress = "123 Lê Duẩn, Thanh Khê, Đà Nẵng",
                    ShipPhone = "09053438847",                   
                }
            );

            // data seeding cho Order Details
            modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail{Id = 1, OrderId = 1, Price=250000, Quantity=1, ProductDetail_Id = 11},
                new OrderDetail{Id = 2, OrderId = 1, Price=250000, Quantity=1, ProductDetail_Id = 13}
            );
        }         
    }
}
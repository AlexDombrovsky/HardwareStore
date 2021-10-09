using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HardwareStore.Data;
using HardwareStore.Data.Entities.Products;
using HardwareStore.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HardwareStore.Services
{
    public class DbInitializerService : IDbInitializerService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializerService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        public async Task SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    if (!context.Roles.Any())
                    {
                        var roles = new List<IdentityRole>
                        {
                            new IdentityRole {Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString()}
                        };
                        foreach (var role in roles)
                            context.Roles.Add(role);
                    }

                    var user = new IdentityUser
                    {
                        Email = "admin@gmail.com",
                        NormalizedEmail = "admin@gmail.com".ToUpper(),
                        UserName = "admin@gmail.com",
                        NormalizedUserName = "admin@gmail.com".ToUpper(),
                        SecurityStamp = Guid.NewGuid().ToString("D")
                    };

                    if (!context.Users.Any(u => u.UserName == user.UserName))
                    {
                        var password = new PasswordHasher<IdentityUser>();
                        var hashed = password.HashPassword(user, "123456");
                        user.PasswordHash = hashed;
                        var userStore = new UserStore<IdentityUser>(context);
                        await userStore.CreateAsync(user);
                        await userStore.AddToRoleAsync(user, "ADMIN");
                    }

                    if (!context.Products.Any())
                    {
                        var products = new List<Product>
                        {
                            new Product
                            {
                                Name = "iPhone 12 Pro 256GB",
                                Created = DateTime.UtcNow,
                                Price = 1399,
                                Description = "\r\nДиагональ дисплея:\t6.1''\r\nОсновная камера:\t12 МП\r\nФронтальная камера:\t12 MP\r\nЁмкость аккумулятора:\t2815 mAh\r\nОперационная система:\tiOS 14\r\nCPU:\tApple A14 Bionic\r\n",
                                Photos =
                                {
                                    new Photo {Path = "\\img\\iPhone12Pro1.jpg", Created = DateTime.UtcNow},
                                    new Photo {Path = "\\img\\iPhone12Pro2.jpg", Created = DateTime.UtcNow}
                                }
                            },
                            new Product
                            {
                                Name = "iPhone 12 Pro Max 512GB",
                                Created = DateTime.UtcNow,
                                Price = 1599,
                                Description = "Диагональ дисплея:\t6.7''\r\nОсновная камера:\t12 МП\r\nФронтальная камера:\t12 MP\r\nЁмкость аккумулятора:\t3687 mAh\r\nОперационная система:\tiOS 14\r\nCPU:\tApple A14 Bionic\r\n",
                                Photos =
                                {
                                    new Photo {Path = "\\img\\iPhone12ProMax1.jpg", Created = DateTime.UtcNow},
                                    new Photo {Path = "\\img\\iPhone12ProMax2.jpg", Created = DateTime.UtcNow}
                                }
                            },
                            new Product
                            {
                                Name = "iPhone SE (2020) 128GB",
                                Created = DateTime.UtcNow,
                                Price = 589,
                                Description = "Диагональ дисплея:\t4.7\"\r\nОсновная камера:\t12 Мп\r\nФронтальная камера:\t7 Мп\r\nЁмкость аккумулятора:\t1821 mAh\r\nОперационная система:\tiOS\r\nCPU:\tApple A13 Bionic\r\n",
                                Photos =
                                {
                                    new Photo {Path = "\\img\\iPhoneSE(2020)1.jpg", Created = DateTime.UtcNow},
                                    new Photo {Path = "\\img\\iPhoneSE(2020)2.jpg", Created = DateTime.UtcNow}
                                }
                            },
                            new Product
                            {
                                Name = "iPhone 12 64GB",
                                Created = DateTime.UtcNow,
                                Price = 1099,
                                Description = "\r\nДиагональ дисплея:\t6.1''\r\nОсновная камера:\t12 МП\r\nФронтальная камера:\t12 MP\r\nЁмкость аккумулятора:\t2815 mAh\r\nОперационная система:\tiOS 14\r\nCPU:\tApple A14 Bionic\r\n",
                                Photos =
                                {
                                    new Photo {Path = "\\img\\iPhone1264GB1.jpg", Created = DateTime.UtcNow},
                                    new Photo {Path = "\\img\\iPhone1264GB2.jpg", Created = DateTime.UtcNow}
                                }
                            },
                            new Product
                            {
                                Name = "iPhone 12 mini 64 GB",
                                Created = DateTime.UtcNow,
                                Price = 799,
                                Description = "\r\nДиагональ дисплея:\t5.4''\r\nОсновная камера:\t12 МП\r\nФронтальная камера:\t12 MP\r\nЁмкость аккумулятора:\t2227 mAh\r\nОперационная система:\tiOS 14\r\nCPU:\tApple A14 Bionic\r\n",
                                Photos =
                                {
                                    new Photo {Path = "\\img\\iPhone12mini64GB1.jpg", Created = DateTime.UtcNow},
                                    new Photo {Path = "\\img\\iPhone12mini64GB2.jpg", Created = DateTime.UtcNow}
                                }
                            },
                            new Product
                            {
                                Name = "Xiaomi Mi 10T Lite 6/128GB",
                                Created = DateTime.UtcNow,
                                Price = 349,
                                Description = "\r\nДиагональ дисплея:\t6.67''\r\nОсновная камера:\t64 МП\r\nФронтальная камера:\t16 МП\r\nЁмкость аккумулятора:\t4820 mAh\r\nОперационная система:\tAndroid 10\r\nCPU:\tQualcomm SM7225 Snapdragon 750 5G",
                                Photos =
                                {
                                    new Photo {Path = "\\img\\XiaomiMi10TLite6128GB1.jpg", Created = DateTime.UtcNow},
                                    new Photo {Path = "\\img\\XiaomiMi10TLite6128GB2.jpg", Created = DateTime.UtcNow}
                                }
                            },
                            new Product
                            {
                                Name = "Xiaomi Mi 10T Pro 6/128GB",
                                Created = DateTime.UtcNow,
                                Price = 749,
                                Description = "Диагональ дисплея:\t6.67''\r\nОсновная камера:\t64 МП\r\nФронтальная камера:\t16 МП\r\nЁмкость аккумулятора:\t4820 mAh\r\nОперационная система:\tAndroid 10\r\nCPU:\tQualcomm SM7225 Snapdragon 750 5G",
                                Photos =
                                {
                                    new Photo {Path = "\\img\\XiaomiMi10TPro8256GB1.jpg", Created = DateTime.UtcNow},
                                    new Photo {Path = "\\img\\XiaomiMi10TPro8256GB2.jpg", Created = DateTime.UtcNow}
                                }
                            },
                            new Product
                            {
                                Name = "Xiaomi Mi 11 8/128GB",
                                Created = DateTime.UtcNow,
                                Price = 649,
                                Description = "Диагональ дисплея:\t6.81''\r\nОсновная камера:\t108 МП\r\nФронтальная камера:\t20 МП\r\nЁмкость аккумулятора:\t4600 mAh\r\nОперационная система:\tAndroid 10\r\nCPU:\tQualcomm SM8350 Snapdragon 888 (5 nm)",
                                Photos =
                                {
                                    new Photo {Path = "\\img\\XiaomiMi118128GB1.jpg", Created = DateTime.UtcNow},
                                    new Photo {Path = "\\img\\XiaomiMi118128GB2.jpg", Created = DateTime.UtcNow}
                                }
                            },
                            new Product
                            {
                                Name = "Samsung Galaxy A72 128GB",
                                Created = DateTime.UtcNow,
                                Price = 449,
                                Description = "Диагональ дисплея:\t6.7\"\r\nОсновная камера:\t64 MP\r\nФронтальная камера:\t32 MP\r\nЁмкость аккумулятора:\t5000 mAh\r\nОперационная система:\tAndroid\r\nCPU:\tQualcomm SM7125 Snapdragon 720G",
                                Photos =
                                {
                                    new Photo {Path = "\\img\\SamsungGalaxyA72128GB1.jpg", Created = DateTime.UtcNow},
                                    new Photo {Path = "\\img\\SamsungGalaxyA72128GB2.jpg", Created = DateTime.UtcNow}
                                }
                            },
                            new Product
                            {
                                Name = "Samsung Galaxy S21+ 128GB (G996)",
                                Created = DateTime.UtcNow,
                                Price = 1199,
                                Description = "Диагональ дисплея:\t6.7''\r\nОсновная камера:\t12 МП\r\nФронтальная камера:\t10 MP\r\nЁмкость аккумулятора:\t4800 mAh\r\nОперационная система:\tAndroid 11\r\nCPU:\tExynos 2100\r\n",
                                Photos =
                                {
                                    new Photo {Path = "\\img\\SamsungGalaxyS21128GB(G996)1.jpg", Created = DateTime.UtcNow},
                                    new Photo {Path = "\\img\\SamsungGalaxyS21128GB(G996)2.jpg", Created = DateTime.UtcNow}
                                }
                            },
                            new Product
                            {
                                Name = "Lenovo Tab M10 HD",
                                Created = DateTime.UtcNow,
                                Price = 299,
                                Description = "Диагональ дисплея:\t10.1''\r\nРазрешение дисплея:\t1280 x 800\r\nВстроенная память:\t64 ГБ\r\nCPU:\tMediatek Helio P22T\r\nОперационная система:\tAndroid 10",
                                Photos =
                                {
                                    new Photo {Path = "\\img\\LenovoTabM10HD1.jpg", Created = DateTime.UtcNow},
                                    new Photo {Path = "\\img\\LenovoTabM10HD2.jpg", Created = DateTime.UtcNow}
                                }
                            },
                            new Product
                            {
                                Name = "Samsung Galaxy Tab A7 2020",
                                Created = DateTime.UtcNow,
                                Price = 399,
                                Description = "\r\nДиагональ дисплея:\t10.4''\r\nРазрешение дисплея:\t2000 x 1200\r\nВстроенная память:\t32 Гб\r\nCPU:\tQualcomm SM6115 Snapdragon 662\r\nОперационная система:\tAndroid 10",
                                Photos =
                                {
                                    new Photo {Path = "\\img\\SamsungGalaxyTabA720201.jpg", Created = DateTime.UtcNow},
                                    new Photo {Path = "\\img\\SamsungGalaxyTabA720202.jpg", Created = DateTime.UtcNow}
                                }
                            },
                            new Product
                            {
                                Name = "Apple 11 iPad Pro 128GB 2020",
                                Created = DateTime.UtcNow,
                                Price = 1299,
                                Description = "Разрешение сенсора : Dual: 12 + 10 Мп\r\nФронтальная камера : 7 Мп\r\nТип батареи : Li-Pol\r\nКоличество ядер : 8\r\nРазрешение (пкс) : 1668 x 2388 пкс\r\nВстроенная память : 128 ГБ\r\nОперативная память : 6 ГБ",
                                Photos =
                                {
                                    new Photo {Path = "\\img\\Apple11iPadPro128GB20201.jpg", Created = DateTime.UtcNow}
                                }
                            }
                        };
                        foreach (var product in products)
                            context.Products.Add(product);
                    }

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
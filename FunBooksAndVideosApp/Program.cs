using Common.Helpers;
using DataAccess.Implementations;
using DataAccess.Interfaces;
using FunBooksAndVideosApp.Models.Orders.Implementations;
using MembershipService.Controllers;
using MembershipService.Managers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Models;
using Models.Enums;
using Models.Implementations.Books;
using Models.Interfaces.Api;
using Models.Interfaces.Services;
using PurchaseService.Controllers;
using Refit;
using ShippingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunBooksAndVideosApp
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var pur = new Purchase()
            {
                Customer = new Customer()
                {
                    Address = "new address",
                    FirstName = "first",
                    LastName = "last",
                    Id = 1,

                },
                Orders = new List<OrderPosition>() {
                    new OrderPosition()
                    {
                        OrderItem = new BookOrderItem()
               {
                   Id= 1,
                   NameOfItem = "book1",
                   OrderItemType = OrderItemType.Book,
                   Price = 10,
                   Author = "Artur",
                   Title = "Interesting book1"
               },
                        Quantity = 1,
                    },
                     new OrderPosition()
                    {
                        OrderItem = new BookOrderItem()
                {
                    Id = 2,
                    NameOfItem = "book2",
                    OrderItemType = OrderItemType.Book,
                    Price = 10,
                    Author = "Artur",
                    Title = "Interesting book2"
                },
                        Quantity = 1,
                    },
                      new OrderPosition()
                    {
                        OrderItem = new CDVideoOrderItem()
                 {
                     Id = 3,
                     NameOfItem = "video1",
                     OrderItemType = OrderItemType.Video,
                     Price = 10,
                     Length = new System.TimeSpan(1,20,12),
                 },
                        Quantity = 1,
                    },
                       new OrderPosition()
                    {
                        OrderItem =  new CDVideoOrderItem()
                 {
                     Id = 4,
                     NameOfItem = "video2",
                     OrderItemType = OrderItemType.Book,
                     Price = 10,
                     Author = "Artur",
                 },
                        Quantity = 1,
                    },
                        new OrderPosition()
                    {
                        OrderItem = new BookClubMembership()
                 {
                     Id = 5,
                     NameOfItem = "membership1",
                     OrderItemType = OrderItemType.BookClubMembeship,
                     Price = 10
                 },
                        Quantity = 1,
                    },
                         new OrderPosition()
                    {
                        OrderItem = new VideoClubMembership()
                 {
                     Id = 6,
                     NameOfItem = "membership2",
                     OrderItemType = OrderItemType.VideoClubMembership,
                     Price = 10
                 },
                        Quantity = 1,
                    },
                          new OrderPosition()
                    {
                        OrderItem =  new PremiumMembership()
                 {
                     Id = 7,
                     NameOfItem = "membership3",
                     OrderItemType = OrderItemType.Premium,
                     Price = 10
                 },
                        Quantity = 1,
                    },
                },




            };

            var t = JsonHelper.ToJson(pur);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .ConfigureServices((context, services) =>
                {
                    var connectionString = context.Configuration.GetConnectionString("mainConnectionStr");
                    services.AddLogging(loggingBuilder =>
                    {
                        loggingBuilder.AddConfiguration(context.Configuration.GetSection("Logging"));
                        loggingBuilder.AddConsole();
                    });

                    services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(connectionString), ServiceLifetime.Singleton);
                    services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
                    services.AddScoped<IRepository<Purchase>, PurchaseRepository>();
                    services.AddScoped<IRepository<MembershipStatus>, MembershipRepository>();
                    services.AddScoped<IRepository<Customer>, CustomerRepository>();
                    services.AddScoped<IRepository<OrderItem>, OrderItemsRepository>();


                    services.AddScoped<IMembershipActivationService, MembershipActivationService>();
                    services.AddScoped<IShippingService, ShippingService.ShippingService>();


                    services.AddControllers();



                    services.AddControllersWithViews().AddApplicationPart(typeof(PurchaseController).Assembly);
                    services.AddScoped<IMembershipApi>((ser) => RestService.For<IMembershipApi>("http://localhost:5000"));
                    services.AddScoped<ICustomerApi>((ser) => RestService.For<ICustomerApi>("http://localhost:5000"));
                    services.AddControllersWithViews().AddApplicationPart(typeof(MembershipController).Assembly);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

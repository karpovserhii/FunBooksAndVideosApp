using DataAccess.Implementations;
using DataAccess.Interfaces;
using MembershipService.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Models;
using Models.Interfaces;
using Models.Interfaces.Api;
using PurchaseService.Controllers;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseService
{
	public class Program
	{
		public static void Main(string[] args)
		{
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

                    services.AddDbContext<AppDbContext>(ServiceLifetime.Singleton );
                    services.AddSingleton(typeof(IRepository<>), typeof(BaseRepository<>));
                    services.AddSingleton<IRepository<Purchase>, PurchaseRepository>();
                    services.AddSingleton<IRepository<MembershipStatus>, MembershipRepository>();
					

                    services.AddControllers();

					

					services.AddSingleton<PurchaseController>();
					services.AddSingleton<IMembershipApi>(RestService.For<IMembershipApi>("http://localhost:5000"));
					services.AddSingleton<MembershipController>();
				})
		
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}

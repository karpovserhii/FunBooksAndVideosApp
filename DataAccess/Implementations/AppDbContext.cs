using FunBooksAndVideosApp.Models.Orders.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Models;
using Models.Enums;
using Models.Implementations.Books;
using Models.Interfaces;

namespace DataAccess.Implementations
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        DbContextOptions<AppDbContext> options;
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            this.options = options;
            Initialize();
        }

        public Microsoft.EntityFrameworkCore.DbSet<Customer> Customers { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<MembershipStatus> MembershipStatuses { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<OrderItem> OrderItems { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<OrderPosition> OrderPosition { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Purchase> Purchases { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(c => c.LastName).IsRequired().HasMaxLength(50);
                entity.Property(c => c.Address).HasMaxLength(200);
            });

            modelBuilder.Entity<Customer>().HasData(
                new Customer()
                {
                    Id = -1,
                    Address = "TestAddress1",
                    FirstName = "First1",
                    LastName = "Last1"
                },
                 new Customer()
                 {
                     Id = -2,
                     Address = "TestAddress2",
                     FirstName = "First2",
                     LastName = "Last2"
                 },
                  new Customer()
                  {
                      Id = -3,
                      Address = "TestAddress3",
                      FirstName = "First3",
                      LastName = "Last3"
                  }
            );


            //modelBuilder.Entity<OrderItem>()
            //.HasKey(t => t.Id);
            //modelBuilder.Entity<OrderItem>().Property(t => t.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<OrderItem>().HasData(
               new OrderItem()
               {
                   Id = -1,
                   NameOfItem = "book1",
                   OrderItemType = Models.Enums.OrderItemType.Book,
                   Price = 10,
                   Author = "Artur",
                   Title = "Interesting book1"
               },
                new OrderItem()
                {
                    Id = -2,
                    NameOfItem = "book2",
                    OrderItemType = Models.Enums.OrderItemType.Book,
                    Price = 10,
                    Author = "Artur",
                    Title = "Interesting book2"
                },
                 new OrderItem()
                 {
                     Id = -3,
                     NameOfItem = "video1",
                     OrderItemType = Models.Enums.OrderItemType.Video,
                     Price = 10,
                     Length = new System.TimeSpan(1, 20, 12),
                 },
                 new OrderItem()
                 {
                     Id = -4,
                     NameOfItem = "video2",
                     OrderItemType = Models.Enums.OrderItemType.Book,
                     Price = 10,
                     Author = "Artur",
                 },
                 new OrderItem()
                 {
                     Id = -5,
                     NameOfItem = "membership1",
                     OrderItemType = Models.Enums.OrderItemType.BookClubMembeship,
                     Price = 10
                 },
                 new OrderItem()
                 {
                     Id = -6,
                     NameOfItem = "membership2",
                     OrderItemType = Models.Enums.OrderItemType.VideoClubMembership,
                     Price = 10
                 },
                 new OrderItem()
                 {
                     Id = -7,
                     NameOfItem = "membership3",
                     OrderItemType = Models.Enums.OrderItemType.Premium,
                     Price = 10
                 }

           );

        }

        public void Initialize()
        {
            Database.EnsureCreated();
        }
    }
}

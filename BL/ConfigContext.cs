using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains;

namespace BL
{
    public partial class ConfigContext : IdentityDbContext<ApplicationUser>
    {
       
        public ConfigContext()
        {
        }

        public ConfigContext(DbContextOptions<ConfigContext> options) : base(options)
        {
        }

        public DbSet<Restaurant> TbRestaurants { get; set; }
        public DbSet<MenuItem> TbMenuItems { get; set; }
        public DbSet<Order> TbOrders { get; set; }
        public DbSet<OrderItem> TbOrderItems { get; set; }
        public DbSet<Payment> TbPayments { get; set; }
        


        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveMaxLength(400);
            configurationBuilder.Properties<decimal>().HaveColumnType("decimal(8,2)");
            configurationBuilder.Properties<DateTime>().HaveColumnType("DateTime");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ العلاقة بين المستخدم والمطاعم (صاحب المطعم)
            modelBuilder.Entity<Restaurant>()
                .HasOne(r => r.Owner)
                .WithMany(u => u.Restaurants)
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // ✅ العلاقة بين المطعم وقائمة الطعام
            modelBuilder.Entity<MenuItem>()
                .HasOne(m => m.Restaurant)
                .WithMany(r => r.MenuItems)
                .HasForeignKey(m => m.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ العلاقة بين الطلب والعميل
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // ✅ العلاقة بين الطلب والمندوب
            modelBuilder.Entity<Order>()
                .HasOne(o => o.DeliveryPerson)
                .WithMany(u => u.Deliveries)
                .HasForeignKey(o => o.DeliveryPersonId)
                .OnDelete(DeleteBehavior.Restrict);

            // ✅ العلاقة بين الطلب وتفاصيل الطلب
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ العلاقة بين تفاصيل الطلب وقائمة الطعام
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.MenuItem)
                .WithOne(l => l.OrderItem)
                .HasForeignKey<OrderItem>(oi => oi.MenuItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>()
             .HasOne(p => p.Order)
             .WithOne(o => o.Payment)
             .HasForeignKey<Payment>(p => p.OrderId) // لازم نحدد الـ Model اللي فيه المفتاح الأجنبي
             .OnDelete(DeleteBehavior.Cascade);



        }
    }
    

}


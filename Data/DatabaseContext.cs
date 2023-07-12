using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using lab3.Models;

namespace lab3.Data
{
    public class DatabaseContext : DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAddressEntity>()
            .HasKey(ua => new { ua.userId, ua.addressId });

            modelBuilder.Entity<UserAddressEntity>()
                .HasOne(ua => ua.user)
                .WithMany(u => u.userAddressEntity)
                .HasForeignKey(ua => ua.userId);

            modelBuilder.Entity<UserAddressEntity>()
                .HasOne(ua => ua.address)
                .WithMany(a => a.userAddressEntity)
                .HasForeignKey(ua => ua.addressId);
        }

        public DatabaseContext(DbContextOptions options) : base(options) { }
        public DbSet<UserEntity> UserEntityMapping { get; set; }
        public DbSet<AddressEntity> AddressMapping { get; set; }
        public DbSet<UserAddressEntity> UserAddressMapping { get; set; }
    }
}
using System;
using ECommerce.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    //1. db nerede? Bu soruyu, AppDbContext nesnesini oluşturan Program.cs, DbContextOptions<AppDbContext> opsiyonu ile  yanıtlasın.


    //2. tabloların kuralları ve ilişkileri....

    public DbSet<Product> Products => Set<Product>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().Property(p=>p.Sku)
                                      .IsRequired()
                                      .HasMaxLength(50);

        modelBuilder.Entity<Product>().Property(p=>p.Price)
                                    .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Order>().HasMany(o=>o.Items)
                                    .WithOne()
                                    .HasForeignKey(oi=>oi.Id)
                                    .OnDelete(DeleteBehavior.Cascade);
                                    

    }

  
}

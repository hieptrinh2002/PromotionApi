using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PromotionApi.Models;

public class PromotionDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public PromotionDbContext(DbContextOptions<PromotionDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Promotion> Promotions { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySQL(_configuration.GetConnectionString("DefaultConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Promotion>()
            .HasIndex(p => p.Code)
            .IsUnique();
        base.OnModelCreating(modelBuilder);

        // Seed data for Promotion table
        modelBuilder.Entity<Promotion>().HasData(
            new Promotion
            {
                Code = "SUMMER2023",
                DateStart = new DateTime(2024, 6, 1),
                DateExpire = new DateTime(2024, 7, 31),
                Condition = 850000,
                Discount = 15f,
                QuantityAvailable = 40,
                MerchantId = "2236b29d-c850-4c6e-bb29-629be5eace69"
            },
            new Promotion
            {
                Code = "FALLSALE2023",
                DateStart = new DateTime(2024, 6, 1),
                DateExpire = new DateTime(2024, 8, 30),
                Condition = 750000,
                Discount = 20f,
                QuantityAvailable = 400,
                MerchantId = "2236b29d-c850-4c6e-bb29-629be5eace69"
            },
            new Promotion
            {
                Code = "HOLIDAY2023",
                DateStart = new DateTime(2024, 6, 1),
                DateExpire = new DateTime(2024, 7, 31),
                Condition = 950000,
                Discount = 25f,
                QuantityAvailable = 200,
                MerchantId = "2236b29d-c850-4c6e-bb29-629be5eace69"
            }
        ); ;
    }

}
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
        base.OnModelCreating(modelBuilder);

        // Seed data for Promotion table
        modelBuilder.Entity<Promotion>().HasData(
            new Promotion
            {
                Code = "SUMMER2023",
                DateStart = new DateTime(2023, 6, 1),
                DateExpire = new DateTime(2023, 8, 31),
                Condition = 50.0,
                Discount = 0.15f,
                IdMer = "39ae71a3-1f29-4c5e-a0e8-12e06b70f7b5"
            },
            new Promotion
            {
                Code = "FALLSALE2023",
                DateStart = new DateTime(2023, 9, 1),
                DateExpire = new DateTime(2023, 11, 30),
                Condition = 75.0,
                Discount = 0.20f,
                IdMer = "39ae71a3-1f29-4c5e-a0e8-12e06b70f7b5"
            },
            new Promotion
            {
                Code = "HOLIDAY2023",
                DateStart = new DateTime(2023, 12, 1),
                DateExpire = new DateTime(2023, 12, 31),
                Condition = 100.0,
                Discount = 0.25f,
                IdMer = "39ae71a3-1f29-4c5e-a0e8-12e06b70f7b5"
            }
        ); ;
    }

}
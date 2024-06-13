﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PromotionApi.Models;

#nullable disable

namespace PromotionApi.Migrations
{
    [DbContext(typeof(PromotionDbContext))]
    [Migration("20240613082351_initdb")]
    partial class initdb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PromotionApi.Models.Promotion", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<double>("Condition")
                        .HasColumnType("double");

                    b.Property<DateTime>("DateExpire")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime(6)");

                    b.Property<float>("Discount")
                        .HasColumnType("float");

                    b.Property<string>("IdMer")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Promotions");

                    b.HasData(
                        new
                        {
                            Id = "fdc011be-2fd5-4b96-8c96-943caa9bb663",
                            Code = "SUMMER2023",
                            Condition = 50.0,
                            DateExpire = new DateTime(2023, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateStart = new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Discount = 0.15f,
                            IdMer = "39ae71a3-1f29-4c5e-a0e8-12e06b70f7b5"
                        },
                        new
                        {
                            Id = "7130a7c3-3896-495d-b58a-cadfbce4ef78",
                            Code = "FALLSALE2023",
                            Condition = 75.0,
                            DateExpire = new DateTime(2023, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateStart = new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Discount = 0.2f,
                            IdMer = "39ae71a3-1f29-4c5e-a0e8-12e06b70f7b5"
                        },
                        new
                        {
                            Id = "05b81e12-76fd-4f2f-93a3-31771ac40dd2",
                            Code = "HOLIDAY2023",
                            Condition = 100.0,
                            DateExpire = new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateStart = new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Discount = 0.25f,
                            IdMer = "39ae71a3-1f29-4c5e-a0e8-12e06b70f7b5"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
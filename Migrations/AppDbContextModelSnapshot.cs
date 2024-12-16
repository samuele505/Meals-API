﻿// <auto-generated />
using System;
using Meals_API;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Meals_API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("Meals_API.Models.MealSearch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("SearchQuery")
                        .HasColumnType("TEXT");

                    b.Property<bool>("SearchSuccess")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Searches", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
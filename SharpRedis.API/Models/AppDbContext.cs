﻿using System;
using Microsoft.EntityFrameworkCore;

namespace SharpRedis.API.Models
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			//Seed
			modelBuilder.Entity<Product>().HasData(
				new Product() { Id = 1, Name = "Pen 1", Price = 100 },
				new Product() { Id = 2, Name = "Pen 2", Price = 200 },
				new Product() { Id = 3, Name = "Pen 3", Price = 300 }
				);

            base.OnModelCreating(modelBuilder);
        }
    }
}


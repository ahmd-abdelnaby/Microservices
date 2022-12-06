﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OrderApplication.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MassTransit.Logging.OperationName;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OrderApplication.Context
{
  
        public class OrderDBContext : DbContext
        {
        public OrderDBContext(DbContextOptions<OrderDBContext> options) : base(options)
        {
        }

        public virtual DbSet<Order>  Orders { get; set; }
        public virtual DbSet<OrderDetails>  OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
            .HasMany(o => o.Details)
            .WithOne(d => d.Order).HasForeignKey(d => d.OderId);
        }
    }

    public class BloggingContextFactory : IDesignTimeDbContextFactory<OrderDBContext>
    {
        public OrderDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OrderDBContext>();
            optionsBuilder.UseSqlServer("Data Source =.; Initial Catalog = OrdersDB; Integrated Security = True;Trust Server Certificate=true");

            return new OrderDBContext(optionsBuilder.Options);
        }
    }


}
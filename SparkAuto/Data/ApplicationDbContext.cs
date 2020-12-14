﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparkAuto.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ServiceType> ServiceTypes { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Car> Cars  { get; set; }

        public DbSet<ServiceShoppingCart> serviceShoppingCarts { get; set; }

        public DbSet<ServiceHeader> ServiceHeaders { get; set; }

        public DbSet<ServiseDetails> ServiseDetails { get; set; }
    }
}

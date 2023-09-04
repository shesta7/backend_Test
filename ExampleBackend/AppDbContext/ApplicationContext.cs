using System;
using ExampleBackend.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ExampleBackend.Controllers.Entities;
using System.Reflection.Emit;

namespace ExampleBackend.AppDbContext
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<MarkEntity> Marks => Set<MarkEntity>();
        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
       : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().Property(e => e.UserId).ValueGeneratedOnAdd();
            modelBuilder.Entity<MarkEntity>().Property(e => e.MarkId).ValueGeneratedOnAdd();
        }


    }
}


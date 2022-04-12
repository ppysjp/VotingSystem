using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using VotingSystem.Models;
using Xunit;

namespace VotingSystem.Database.Tests
{
    public class DatabaseTests
    {
        [Fact]
        public void SavesCounterToDatabase() 
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(nameof(SavesCounterToDatabase))
                .Options;

            var counter = new Counter { Name = "New Counter"};

            using (var ctx = new AppDbContext(options)) 
            {
                ctx.Counters.Add(counter);
                ctx.SaveChanges();
            }

            using (var ctx = new AppDbContext(options)) 
            {
                var savedCounter = ctx.Counters.Single();
                Assert.Equal(counter.Name, savedCounter.Name);
            }
        }

        [Fact]
        public void SavesCounterToDatabase2() 
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(nameof(SavesCounterToDatabase))
                .Options;

            var counter = new Counter { Name = "New Counter"};

            using (var ctx = new AppDbContext(options)) 
            {
                ctx.Counters.Add(counter);
                ctx.Counters.Add(counter);
                ctx.SaveChanges();
            }

            using (var ctx = new AppDbContext(options)) 
            {
                var savedCounter = ctx.Counters.ToList();
            }
        }


    }

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Counter> Counters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Counter>().Property<int>("Id"); 
        }
    }
}

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
            var counter = new Counter { Name = "New Counter"};
            using (var ctx = new AppDbContext()) 
            {
                ctx.Counters.Add(counter);
                ctx.SaveChanges();
            }

            using (var ctx = new AppDbContext()) 
            {
                var counter = ctx.Counters.Single();

            }
        }
    }

    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public DbSet<Counter> Counters { get; set; }
    }
}

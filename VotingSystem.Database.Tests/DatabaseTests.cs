using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace VotingSystem.Database.Tests
{
    public class DatabaseTests
    {
        [Fact]
        public void SavesCounterToDatabase() 
        {
            using (var ctx = new AppDbContext()) 
            { 
                ctx.Counters
            }

            using (var ctx = new AppDbContext()) 
            { 

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

﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using VotingSystem.Models;
using Xunit;

namespace VotingSystem.Database.Tests
{
    public class AppDbContextTests
    {

        public AppDbContext CreateDbContext(string databasename) 
        { 
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databasename)
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public void SavesCounterToDatabase() 
        {
            var counter = new Counter { Name = "New Counter"};

            using (var ctx = CreateDbContext(nameof(SavesCounterToDatabase)))
            {
                ctx.Counters.Add(counter);
                ctx.SaveChanges();
            }

            using (var ctx = CreateDbContext(nameof(SavesCounterToDatabase)))
            {
                var savedCounter = ctx.Counters.Single();
                Assert.Equal(counter.Name, savedCounter.Name);
            }
        }

        [Fact]
        public void SavesVotingPollToDatabase() 
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(nameof(SavesVotingPollToDatabase))
                .Options;

            var poll = new VotingPoll { Title = "New VotingPoll"};

            using (var ctx = CreateDbContext(nameof(SavesVotingPollToDatabase)))
            {
                ctx.VotingPolls.Add(poll);
                ctx.SaveChanges();
            }

            using (var ctx = CreateDbContext(nameof(SavesVotingPollToDatabase)))
            {
                var savedPoll = ctx.VotingPolls.Single();
                Assert.Equal(poll.Title, savedPoll.Title); 
            }
        }



    }

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Counter> Counters { get; set; }
        public  DbSet<VotingPoll> VotingPolls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Counter>().Property<int>("Id"); 
            modelBuilder.Entity<VotingPoll>().Property<int>("Id"); 
        }
    }
}
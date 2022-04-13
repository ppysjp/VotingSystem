using Microsoft.EntityFrameworkCore;

namespace VotingSystem.Database.Tests.Infrastructure
{
    public class DbContextFactory
    {
        public static AppDbContext Create(string databasename) 
        { 
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databasename)
                .Options;
            return new AppDbContext(options);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Application;
using VotingSystem.Database.Tests.Infrastructure;
using VotingSystem.Models;
using Xunit;

namespace VotingSystem.Database.Tests
{
    public class VotingSystemPersistanceTests
    {
       [Fact] 
       public void SavesVotingPollToDatabase() 
        {
            var poll = new VotingPoll
            {
                Title = "title",
                Description = "desc",
                Counters = new List<Counter>
                {
                    new Counter {Name = "One" },
                    new Counter {Name = "Two" },
                }
            };

            using (var ctx = DbContextFactory.Create(nameof(SavesVotingPollToDatabase)))
            {
                IVotingSystemPersistance persistance = new VotingSystemPersistance(ctx);
                persistance.SaveVotingPoll(poll);
            }


        }
    }

    public class VotingSystemPersistance : IVotingSystemPersistance
    {
        private AppDbContext _ctx;

        public VotingSystemPersistance(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public void SaveVotingPoll(VotingPoll votingPoll)
        {
            throw new NotImplementedException();
        }
    }
}

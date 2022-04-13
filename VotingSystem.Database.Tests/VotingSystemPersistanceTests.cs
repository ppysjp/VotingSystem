using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Application;
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

            
            IVotingSystemPersistance persistance = new VotingSystemPersistance();
        }
    }

    public class VotingSystemPersistance : IVotingSystemPersistance
    {
        public void SaveVotingPoll(VotingPoll votingPoll)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Models;
using Xunit;

namespace VotingSystem.Application.Tests
{
    public class StatisticsInteractor
    {
       [Fact] 
       public void DisplayPollStatics() 
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

       }

    }
}

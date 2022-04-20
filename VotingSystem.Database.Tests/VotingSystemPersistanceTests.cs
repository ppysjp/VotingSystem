﻿using Microsoft.EntityFrameworkCore;
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
       public void PersistsVotingPoll() 
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

            using (var ctx = DbContextFactory.Create(nameof(PersistsVotingPoll)))
            {
                IVotingSystemPersistance persistance = new VotingSystemPersistance(ctx);
                persistance.SaveVotingPoll(poll);
            }

            using (var ctx = DbContextFactory.Create(nameof(PersistsVotingPoll)))
            {
                var savedPoll = ctx.VotingPolls
                    .Include(x => x.Counters)
                    .Single();

                Assert.Equal(poll.Title, savedPoll.Title); 
                Assert.Equal(poll.Description, savedPoll.Description);
                Assert.Equal(poll.Counters.Count(), savedPoll.Counters.Count());

                foreach (var name in poll.Counters.Select(x => x.Name))
                {
                    Assert.Contains(name, savedPoll.Counters.Select(x => x.Name));
                }
            }
        }

       [Fact] 
       public void PersistsVote() 
        {

            var vote = new Vote { UserId = "user", CounterId = 1 };

            using (var ctx = DbContextFactory.Create(nameof(PersistsVote)))
            {
                var persistance = new VotingSystemPersistance(ctx);
                persistance.SaveVote(vote);
            }

            using (var ctx = DbContextFactory.Create(nameof(PersistsVote)))
            {
                var savedVote = ctx.Votes.Single();
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

        public void SaveVote(Vote vote)
        {
            throw new NotImplementedException();
        }

        public void SaveVotingPoll(VotingPoll votingPoll)
        {
            _ctx.VotingPolls.Add(votingPoll);
            _ctx.SaveChanges();
        }

        public bool VoteExists(Vote vote)
        {
            throw new NotImplementedException();
        }
    }
}

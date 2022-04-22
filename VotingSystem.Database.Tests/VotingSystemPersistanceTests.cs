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
using static Xunit.Assert;


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

                Equal(poll.Title, savedPoll.Title);
                Equal(poll.Description, savedPoll.Description);
                Equal(poll.Counters.Count(), savedPoll.Counters.Count());

                foreach (var name in poll.Counters.Select(x => x.Name))
                {
                    Contains(name, savedPoll.Counters.Select(x => x.Name));
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
                Equal(vote.UserId, savedVote.UserId);
                Equal(vote.CounterId, savedVote.CounterId);
            }
       }

       [Fact] 
       public void VoteExists_ReturnsFalseWhenNoVote() 
       {

            var vote = new Vote { UserId = "user", CounterId = 1 };

            using (var ctx = DbContextFactory.Create(nameof(VoteExists_ReturnsFalseWhenNoVote)))
            {
                var persistance = new VotingSystemPersistance(ctx);
                False(persistance.VoteExists(vote));
            }
       }

       [Fact] 
       public void VoteExists_ReturnsTrueWhenVotePersisted() 
       {

            var vote = new Vote { UserId = "user", CounterId = 1 };

            using (var ctx = DbContextFactory.Create(nameof(VoteExists_ReturnsTrueWhenVotePersisted)))
            {
                ctx.Votes.Add(vote);
                ctx.SaveChanges();
            }

            using (var ctx = DbContextFactory.Create(nameof(VoteExists_ReturnsTrueWhenVotePersisted)))
            {
                var persistance = new VotingSystemPersistance(ctx);
                True(persistance.VoteExists(vote));
            }
       }

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

       [Fact] 
       public void GetPoll_RetrievesAPollFWithCountersFromDatabase() 
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
                IVotingSystemPersistance persistance = new VotingSystemPersistance(ctx);
                var savedPoll = persistance.GetPoll(1);
            }
       }




    }
}

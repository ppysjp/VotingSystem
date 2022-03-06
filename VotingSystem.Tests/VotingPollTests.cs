﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  Xunit;
using static Xunit.Assert;

namespace VotingSystem.Tests
{
    public class VotingPollTests
    {
        [Fact]
        public void ZeroCountersWhenCreated() 
        {
            var poll = new VotingPoll();
            Empty(poll.Counters);
        }
    }
    public class VotingPollFactoryTests
    {

        private VotingPollFactory _factory = new VotingPollFactory();

        private VotingPollFactory.Request _request = new VotingPollFactory.Request 
        { 
            Title = "title",
            Description = "description",
            Names = new[] { "names1", "name2" }
        };

        [Fact]
        public void Create_AddsCounterToThePollForEachName() 
        {
            var poll = _factory.Create(_request);

            foreach (var name in _request.Names)
            {
                Contains(name, poll.Counters.Select(x => x.Name));
            }
        }

        [Fact]
        public void Create_AddsTitleToThePoll() 
        {
            var poll = _factory.Create(_request);

            Equal(_request.Title, poll.Title);
        }
        
        [Fact]
        public void Create_AddsDescriptionToThePoll() 
        {
            var poll = _factory.Create(_request);

            Equal(_request.Description, poll.Description);
        }
 
    }

    public interface IVotingPollFactory 
    {
        public VotingPoll Create(VotingPollFactory.Request request);

    }

    public class VotingPollFactory
    {
        public class Request 
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string[] Names { get; set; }
        }
        
        public VotingPoll Create(Request request)
        {

            if (string.IsNullOrEmpty(request.Title)) throw new ArgumentException("Request Title must not be empty string.");
            if (string.IsNullOrEmpty(request.Description)) throw new ArgumentException("Request Description must not be empty string.");
            if (request.Names.Length < 2) throw new ArgumentException("Request Names must contain at least 2 counter names.");

            return new VotingPoll
            {
                Title = request.Title,
                Description = request.Description,
                Counters = request.Names.Select(name => new Counter { Name = name })
            };
        }
    }

    public class VotingPoll
    {
        public VotingPoll()
        {
            Counters = Enumerable.Empty<Counter>();
        }

        public string Title { get; set; }
        public string Description { get; internal set; }
        public IEnumerable<Counter> Counters { get; set; }
    }
}


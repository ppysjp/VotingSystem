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
        [Fact]
        public void Create_ThrowIfLessThanTwoCounterNames() 
        {
            var names = new[] { "name" };
            var factory = new VotingPollFactory();

            Throws<ArgumentException>(() => factory.Create(new[] { "name" }));
            Throws<ArgumentException>(() => factory.Create(new string[] { }));
        }

        [Fact]
        public void Create_CreatesCounterForEachProvidedNameAndAddsToPoll() 
        {
            var names = new[] { "Yes", "No"};
            var factory = new VotingPollFactory();

            var poll = factory.Create(names);

            foreach (var name in names)
            {
                Contains(name, poll.Counters.Select(x => x.Name));
            }
        }
 
    }

    public class VotingPollFactory
    {
        public VotingPollFactory()
        {
        }

        public VotingPoll Create(string[] names)
        {
            if (names.Length < 2) throw new ArgumentException();

            return new VotingPoll
            {
                Counters = names.Select(name => new Counter { Name = name })
            };
        }
    }

    public class VotingPoll
    {
        public VotingPoll()
        {
            Counters = Enumerable.Empty<Counter>();
        }

        public IEnumerable<Counter> Counters { get; set; }
    }
}


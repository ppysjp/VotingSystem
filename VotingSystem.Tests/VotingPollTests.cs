using System;
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
            var names = new[] { "name"};
            var factory = new VotingPollFactory();

            Throws<ArgumentException>(() => factory.Create(names));
        }
    }

    public class VotingPollFactory
    {
        public VotingPollFactory()
        {
        }

        internal void Create(string[] names)
        {
            throw new NotImplementedException();
        }
    }

    public class VotingPoll
    {
        public VotingPoll()
        {
            Counters = Enumerable.Empty<Counter>();
        }

        public IEnumerable<Counter> Counters { get; }
    }
}


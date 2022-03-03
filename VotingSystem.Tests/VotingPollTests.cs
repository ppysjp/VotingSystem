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


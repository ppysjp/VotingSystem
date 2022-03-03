using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace VotingSystem.Tests
{
    public class VotingPollTests
    {
        [Fact]
        public void ZeroCountersWhenCreated() 
        {
            var poll = new VotingPoll();
            Assert.Empty(poll.Counters);
        }
    }

    public class VotingPoll
    {
        public VotingPoll()
        {
        }

        public IEnumerable Counters { get; internal set; }
    }
}

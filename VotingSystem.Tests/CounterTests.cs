using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static Xunit.Assert;

namespace VotingSystem.Tests
{
    public class CounterTests
    {
        [Fact]
        public void HasName() 
        {
            var name = "Counter Name"; 
            var counter = new Counter();
            counter.Name = name;

            Assert.Equal(name, counter.Name);
        }

        [Fact]
        public void GetCounterStatistics_IncludesCounterName() 
        { 
            var name = "Counter Name"; 
            var counter = new Counter();
            counter.Name = name;

            var statistics = counter.GetCounterStatistics();

        }
    }

    public class Counter
    {
        public string Name { get; set; }

        public Counter()
        {
        }

        internal object GetCounterStatistics()
        {
            throw new NotImplementedException();
        }
    }
}

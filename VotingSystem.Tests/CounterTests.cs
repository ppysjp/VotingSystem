using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static Xunit.Assert;

namespace VotingSystem.Tests
{
    public class CounterTests
    {
        public const string CounterName = "Counter Name";
        public Counter _counter = new Counter() { Name = CounterName };

        [Fact]
        public void HasName() 
        {
            Assert.Equal(CounterName, _counter.Name);
        }

        [Fact]
        public void GetCounterStatistics_IncludesCounterName() 
        { 
            var statistics = _counter.GetStatistics();
            Assert.Equal(CounterName, statistics.Name);
        }
    }

    public class Counter
    {
        public string Name { get; set; }

        public Counter()
        {
        }

        internal Counter GetStatistics()
        {
            return this; 
        }
    }
}

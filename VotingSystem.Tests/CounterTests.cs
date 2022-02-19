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
        public Counter _counter = new Counter() { Name = CounterName, Count = 5};

        [Fact]
        public void HasName() 
        {
            Equal(CounterName, _counter.Name);
        }

        [Fact]
        public void GetCounterStatistics_IncludesCounterName() 
        { 
            var statistics = _counter.GetStatistics(5);
            Equal(CounterName, statistics.Name);
        }

        [Fact]
        public void GetCounterStatistics_IncludesCounterCount() 
        { 
            var statistics = _counter.GetStatistics(5);
            Equal(5, statistics.Count);
        }

        [Fact]
        public void GetStatistics_ShowsPercentageBasedOnTotalCount() 
        { 
            var statistics = _counter.GetStatistics(10);
            Equal(50, statistics.Percentage);
        }
 
    }

    public class Counter
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public double Percentage { get; set; }

        internal Counter GetStatistics(int totalCount)
        {
            return this; 
        }
    }
}

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

        [Theory]
        [InlineData(5, 10, 50)]
        [InlineData(1, 3, 33.33)]
        public void GetStatistics_ShowsPercentageBasedOnTotalCount(int count, int total, double expected) 
        {
            _counter.Count = count; 
            var statistics = _counter.GetStatistics(total);
            Equal(expected, statistics.Percentage);
        }
 
    }

    public class Counter
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public double Percentage { get; set; }

        internal Counter GetStatistics(int totalCount)
        {
            Percentage = Math.Round(Count * 100.0 / totalCount, 2);
            return this; 
        }
    }
}

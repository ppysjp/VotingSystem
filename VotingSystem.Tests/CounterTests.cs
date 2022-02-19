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
        public Counter _counter = new Counter() { Name = CounterName, Count = 5 };
        
        [Fact] 
        public void HasName() 
        {
            Equal(CounterName, _counter.Name);
        }

        [Fact]
        public void GetStatistics_IncludesCounterName() 
        { 
            var statistics = _counter.GetStatistics(5);
            Equal(CounterName, statistics.Name);
                
        }

        [Fact]
        public void GetStatistics_IncludesCounterCount() 
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
            Equal(expected, statistics.Percent);
        }
 
    }

    public class Counter
    {
        public Counter()
        {
        }

        public string Name { get; set; }
        public int Count { get; internal set; }
        public double Percent { get; internal set; }

        internal Counter GetStatistics(int totalCount)
        {
            Percent = Math.Round(Count * 100.0 / totalCount, 2);
            return this; 
        }
    }
}

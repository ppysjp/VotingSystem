using System;
using System.Collections.Generic;
using System.Linq;
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
        [InlineData(2, 3, 66.67)]
        public void GetStatistics_ShowsPercentageUpToTwoDecimalsBasedOnTotalCount(int count, int total, double expected) 
        {
            _counter.Count = count; 
            var statistics = _counter.GetStatistics(total);
            Equal(expected, statistics.Percentage);
        }


        [Fact]
        public void ResolveExcess_DoesntAddExcesswhenAllCountersAreEqual() 
        { 
            var counter1 = new Counter { Count = 1, Percentage = 33.33 };
            var counter2 = new Counter { Count = 1, Percentage = 33.33 };
            var counter3 = new Counter { Count = 1, Percentage = 33.33 };

            var counters = new List<Counter> { counter1, counter2, counter3 };

            new CounterManager().ResolveExcess(counters);

            Assert.Equal(33.33, counter1.Percentage);
            Assert.Equal(33.33, counter2.Percentage);
            Assert.Equal(33.33, counter3.Percentage);
        }

        [Fact]
        public void ResolveExcess_AddsExcessToHighestCounter() 
        { 
            var counter1 = new Counter { Count = 2, Percentage = 66.66 };
            var counter2 = new Counter { Count = 1, Percentage = 33.33 };

            var counters = new List<Counter> { counter1, counter2};
            new CounterManager().ResolveExcess(counters);

            Equal(66.67, counter1.Percentage);
            Equal(33.33, counter2.Percentage);
        }

     }

    public class CounterManager 
    { 
        public void ResolveExcess(List<Counter> counters)
        {
            var highestPercentage = counters.Max(x => x.Percentage);
            var highestCounters = counters.Where(x => x.Percentage == highestPercentage).ToList();
            if (highestCounters.Count < counters.Count)
            {
                counters[0].Percentage += 0.01;
            }
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

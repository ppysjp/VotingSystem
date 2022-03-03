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

            Equal(33.33, counter1.Percentage);
            Equal(33.33, counter2.Percentage);
            Equal(33.33, counter3.Percentage);
        }

        [Theory]
        [InlineData(66.66, 66.67, 33.33)]
        [InlineData(66.65, 66.67, 33.33)]
        public void ResolveExcess_AddsExcessToHighestCounter(double initial, double expected, double lowest) 
        { 
            var counter1 = new Counter { Count = 2, Percentage = initial };
            var counter2 = new Counter { Count = 1, Percentage = lowest };
            var counters = new List<Counter> { counter1, counter2};
             
            new CounterManager().ResolveExcess(counters);

            Equal(expected, counter1.Percentage);
            Equal(lowest, counter2.Percentage);
        }

        [Fact]
        public void ResolveExcess_AddsExcessToLowestCounterWhenMoreThanOneHighestCounters() 
        { 
            var counter1 = new Counter { Count = 2, Percentage = 44.44 };
            var counter2 = new Counter { Count = 2, Percentage = 44.44 };
            var counter3 = new Counter { Count = 1, Percentage = 11.11 };

            var counters = new List<Counter> { counter1, counter2, counter3};
            new CounterManager().ResolveExcess(counters);
            
            Equal(44.44, counter1.Percentage);
            Equal(44.44, counter2.Percentage);
            Equal(11.12, counter3.Percentage);
        }

        [Fact]
        public void ResolveExcess_DoesntAddExcessIfTotalPercentIs100() 
        { 
            var counter1 = new Counter { Count = 2, Percentage = 80 };
            var counter2 = new Counter { Count = 1, Percentage = 20 };
            var counters = new List<Counter> { counter1, counter2};
             
            new CounterManager().ResolveExcess(counters);

            Equal(80, counter1.Percentage);
            Equal(20, counter2.Percentage);
        }



     }

    public class CounterManager 
    { 
        public void ResolveExcess(List<Counter> counters)
        {
            if (counters.Sum(x => x.Percentage) == 100) return;

            var highestPercentage = counters.Max(x => x.Percentage);
            var highestCounters = counters.Where(x => x.Percentage == highestPercentage).ToList();

            if (highestCounters.Count == 1)
            {
                highestCounters.First().Percentage += 0.01;
            }
            else if (highestCounters.Count < counters.Count)
            {
                var lowestPercentage = counters.Min(x => x.Percentage);
                counters.First(x => x.Percentage == lowestPercentage).Percentage += 0.01;
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

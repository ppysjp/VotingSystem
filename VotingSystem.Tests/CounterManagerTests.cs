using System.Collections.Generic;
using System.Text;
using VotingSystem.Models;
using Xunit;
using static Xunit.Assert;

namespace VotingSystem.Tests
{
    public class CounterManagerTests
    {
        public const string CounterName = "Counter Name";
        public Counter _counter = new Counter() { Name = CounterName, Count = 5};

        [Fact]
        public void GetCounterStatistics_IncludesCounterName() 
        {
            var statistics = new CounterManager().GetStatistics(_counter, 5);

            Equal(CounterName, statistics.Name);
        }

        [Fact]
        public void GetCounterStatistics_IncludesCounterCount() 
        { 
            var statistics = new CounterManager().GetStatistics(_counter, 5);

            Equal(5, statistics.Count);
        }

        [Theory]
        [InlineData(5, 10, 50)]
        [InlineData(1, 3, 33.33)]
        [InlineData(2, 3, 66.67)]
        public void GetStatistics_ShowsPercentageUpToTwoDecimalsBasedOnTotalCount(int count, int total, double expected) 
        {
            _counter.Count = count; 

            var statistics = new CounterManager().GetStatistics(_counter, total);

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
        [InlineData(66.66, 66.68, 33.32)]
        public void ResolveExcess_AddsExcessToHighestCounter(double initial, double expected, double lowest) 
        { 
            var counter1 = new Counter { Percentage = initial };
            var counter2 = new Counter { Percentage = lowest };
            var counters = new List<Counter> { counter1, counter2};
             
            new CounterManager().ResolveExcess(counters);

            Equal(expected, counter1.Percentage);
            Equal(lowest, counter2.Percentage);

            var counter3 = new Counter { Percentage = initial };
            var counter4 = new Counter { Percentage = lowest };
            counters = new List<Counter> { counter4, counter3};
             
            new CounterManager().ResolveExcess(counters);

            Equal(expected, counter1.Percentage);
            Equal(lowest, counter2.Percentage);

        }

        [Theory]
        [InlineData(11.11, 11.12, 44.44)]
        [InlineData(11.10, 11.12, 44.44)]
        public void ResolveExcess_AddsExcessToLowestCounterWhenMoreThanOneHighestCounters(double initial, double expected, double highest) 
        { 
            var counter1 = new Counter { Percentage = highest };
            var counter2 = new Counter { Percentage = highest };
            var counter3 = new Counter { Percentage = initial };

            var counters = new List<Counter> { counter1, counter2, counter3};
            new CounterManager().ResolveExcess(counters);
            
            Equal(highest, counter1.Percentage);
            Equal(highest, counter2.Percentage);
            Equal(expected, counter3.Percentage);
        }

        [Fact]
        public void ResolveExcess_DoesntAddExcessIfTotalPercentIs100() 
        { 
            var counter1 = new Counter { Percentage = 80 };
            var counter2 = new Counter { Percentage = 20 };
            var counters = new List<Counter> { counter1, counter2};
             
            new CounterManager().ResolveExcess(counters);

            Equal(80, counter1.Percentage);
            Equal(20, counter2.Percentage);
        }

     }

}

using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotingSystem.Models;
using Xunit;
using static Xunit.Assert;

namespace VotingSystem.Tests
{
    public class CounterManagerTests
    {
        public const string CounterName = "Counter Name";
        public const int CounterId = 1; 
        public Counter _counter = new Counter() { Id = CounterId, Name = CounterName, Count = 5};

        [Fact]
        public void GetCounterStatistics_IncludesCounterName() 
        {
            var statistics = new CounterManager().GetStatistics(new[] {_counter }).First();

            Equal(CounterId, statistics.Id);
        }

        [Fact]
        public void GetCounterStatistics_IncludesCounterId() 
        { 
            var statistics = new CounterManager().GetStatistics(new[] {_counter }).First();

            Equal(CounterId, statistics.Id);
        }

        [Fact]
        public void GetCounterStatistics_IncludesCounterCount() 
        { 
            var statistics = new CounterManager().GetStatistics(new[] {_counter }).First();

            Equal(5, statistics.Count);
        }

        [Theory]
        [InlineData(5, 10, 50)]
        [InlineData(1, 3, 33.33)]
        [InlineData(2, 3, 66.67)]
        [InlineData(2, 8, 25)]
        [InlineData(0, 0, 0)]
        public void GetStatistics_ShowsPercentageUpToTwoDecimalsBasedOnTotalCount(int count, int total, double expected) 
        {
            _counter.Count = count;
            var counter = new Counter { Count = total - count };
            var statistics = new CounterManager().GetStatistics(new[] {_counter, counter }).First();

            Equal(expected, statistics.Percentage);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(33.33)]
        public void ResolveExcess_DoesntAddExcesswhenAllCountersAreEqual(double percent) 
        { 
            var counter1 = new CounterStatistics { Count = 1, Percentage = percent };
            var counter2 = new CounterStatistics { Count = 1, Percentage = percent };
            var counter3 = new CounterStatistics { Count = 1, Percentage = percent };
            var counters = new List<CounterStatistics> { counter1, counter2, counter3 };

            new CounterManager().ResolveExcess(counters);

            Equal(percent, counter1.Percentage);
            Equal(percent, counter2.Percentage);
            Equal(percent, counter3.Percentage);
        }

        [Theory]
        [InlineData(66.66, 66.67, 33.33)]
        [InlineData(66.65, 66.67, 33.33)]
        [InlineData(66.66, 66.68, 33.32)]
        public void ResolveExcess_AddsExcessToHighestCounter(double initial, double expected, double lowest) 
        { 
            var counter1 = new CounterStatistics { Percentage = initial };
            var counter2 = new CounterStatistics { Percentage = lowest };
            var counters = new List<CounterStatistics> { counter1, counter2};
             
            new CounterManager().ResolveExcess(counters);

            Equal(expected, counter1.Percentage);
            Equal(lowest, counter2.Percentage);

            var counter3 = new CounterStatistics { Percentage = initial };
            var counter4 = new CounterStatistics { Percentage = lowest };
            counters = new List<CounterStatistics> { counter4, counter3};
             
            new CounterManager().ResolveExcess(counters);

            Equal(expected, counter1.Percentage);
            Equal(lowest, counter2.Percentage);

        }

        [Theory]
        [InlineData(11.11, 11.12, 44.44)]
        [InlineData(11.10, 11.12, 44.44)]
        public void ResolveExcess_AddsExcessToLowestCounterWhenMoreThanOneHighestCounters(double initial, double expected, double highest) 
        { 
            var counter1 = new CounterStatistics { Percentage = highest };
            var counter2 = new CounterStatistics { Percentage = highest };
            var counter3 = new CounterStatistics { Percentage = initial };

            var counters = new List<CounterStatistics> { counter1, counter2, counter3};
            new CounterManager().ResolveExcess(counters);
            
            Equal(highest, counter1.Percentage);
            Equal(highest, counter2.Percentage);
            Equal(expected, counter3.Percentage);
        }

        [Fact]
        public void ResolveExcess_DoesntAddExcessIfTotalPercentIs100() 
        { 
            var counter1 = new CounterStatistics { Percentage = 80 };
            var counter2 = new CounterStatistics { Percentage = 20 };
            var counters = new List<CounterStatistics> { counter1, counter2};
             
            new CounterManager().ResolveExcess(counters);

            Equal(80, counter1.Percentage);
            Equal(20, counter2.Percentage);
        }

     }

}

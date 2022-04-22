using System;
using System.Collections.Generic;
using System.Linq;
using VotingSystem.Models;

namespace VotingSystem
{
    public class CounterManager : ICounterManager
    { 

        public List<CounterStatistics> GetStatistics(ICollection<Counter> counters)
        {
            var totalCount = counters.Sum(x => x.Count);

            return counters.Select(x => new CounterStatistics
            {
                Name = x.Name,
                Count = x.Count,
                Percentage = RoundUp(x.Count * 100.0 / totalCount)
            }).ToList();
        }

        public Counter GetStatistics(Counter counter, int totalCount)
        {
            counter.Percentage = RoundUp(counter.Count * 100.0 / totalCount);
            return counter; 
        }
 
        public void ResolveExcess(List<Counter> counters)
        {
            var totalPercent = counters.Sum(x => x.Percentage); 
            if (totalPercent == 100) return;

            var excess = 100 - totalPercent;

            var highestPercentage = counters.Max(x => x.Percentage);
            var highestCounters = counters.Where(x => x.Percentage == highestPercentage).ToList();

            if (highestCounters.Count == 1)
            {
                highestCounters.First().Percentage += excess;
            }
            else if (highestCounters.Count < counters.Count)
            {
                var lowestPercentage = counters.Min(x => x.Percentage);
                var lowestCounter = counters.First(x => x.Percentage == lowestPercentage);
                lowestCounter.Percentage = RoundUp(lowestCounter.Percentage + excess);
            }
        }

        private static double RoundUp(double num) => Math.Round(num, 2);


        public void ResolveExcess(List<CounterStatistics> counterStats)
        {
            throw new NotImplementedException();
        }
    }

}

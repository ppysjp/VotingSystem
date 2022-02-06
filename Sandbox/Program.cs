﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox
{
    class Program
    {

        public class Counter 
        {
            private double? _percentage;

            public Counter(string name, int count) 
            {
                Name = name;
                Count = count;
            }
            public string Name { get; }
            public int Count { get; }
            
            public double GetPercent(int total) => _percentage ?? 
                (_percentage = Math.Round(this.Count * 100.0 / total, 2)).Value; 

            public void AddExcess(double excess) => _percentage += excess; 
        }
        
        public class CounterManager 
        {
            public CounterManager(params Counter[] counters)
            {
                Counters = new List<Counter>(counters);                 
            }

            public List<Counter> Counters { get; set; }

            public int Total() =>  Counters.Sum(x => x.Count);

            public double TotalPercentage() =>  Counters.Sum(x => x.GetPercent(Total()));

            public void AnnounceWinner() 
            {
                var excess = Math.Round(100 - TotalPercentage(), 2);

                Console.WriteLine($"Excess: {excess}");

                var biggestAmmountOfVotes = Counters.Max(x => x.Count);

                var winners = Counters.Where(x => x.Count == biggestAmmountOfVotes).ToList();

                if (winners.Count == 1)
                {
                    var winner = winners.First();
                    winner.AddExcess(excess);
                    Console.WriteLine($"{winner.Name} Won!");
                }
                else
                {
                    if (winners.Count != Counters.Count)
                    {
                        var lowestAmmountOfVotes = Counters.Min(x => x.Count);
                        var loser = Counters.First(x => x.Count == lowestAmmountOfVotes);
                        loser.AddExcess(excess);
                    }
                    Console.WriteLine(string.Join(" -DRAW- ", winners.Select(x => x.Name)));
                }

                foreach (var c in Counters)
                {
                    Console.WriteLine($"{c.Name} Counts: {c.Count}, Percentage: {c.GetPercent(Total())}%");
                }

                Console.WriteLine($"Total Percentage: {Math.Round(TotalPercentage(), 2)}%");

            }
        }


        static void Main(string[] args)
        {
            var yes = new Counter("Yes", 4);
            var no = new Counter("No", 4);
            var maybe = new Counter("Maybe", 4);
            var hopefully = new Counter("Hopefully", 4);

            var manager = new CounterManager(yes, no, maybe, hopefully);
            manager.AnnounceWinner();
        }

    }
}

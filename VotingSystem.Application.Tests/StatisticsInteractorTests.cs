using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Models;
using Xunit;

namespace VotingSystem.Application.Tests
{
    public class StatisticsInteractorTests
    {

        private readonly Mock<IVotingSystemPersistance> _mockPersistance = new Mock<IVotingSystemPersistance>();
        private readonly Mock<ICounterManager> _mockCounterManager = new Mock<ICounterManager>();

        [Fact] 
        public void DisplayPollStatistics() 
        {
            var pollId = 1;

            var counter1 = new Counter {Name = "One", Count = 2};
            var counter2 = new Counter {Name = "Two", Count = 1};

            var counterStats1 = new CounterStatistics {Name = "One", Count = 2, Percent = 60};
            var counterStats2 = new CounterStatistics {Name = "Two", Count = 1, Percent = 40};
            var counterStats = new[] {counterStats1, counterStats2};
             
            var poll = new VotingPoll
            {
                Title = "title",
                Description = "desc",
                Counters = new List<Counter> { counter1, counter2 }
            };

            _mockPersistance.Setup(x => x.GetPoll(pollId)).Returns(poll);
            _mockCounterManager.Setup(x => x.ResolvePercentage(poll.Counters)).Returns(counterStats);
 
            var interactor = new StatisticsInteractor(_mockPersistance.Object, _mockCounterManager.Object);
            var pollStatistics = interactor.GetStatistics(pollId);

            Assert.Equal(poll.Title, pollStatistics.Title);
            Assert.Equal(poll.Description, pollStatistics.Description);

            var stats1 = pollStatistics.Counters[0];
            Assert.Equal(counterStats1.Name, stats1.Name);
            Assert.Equal(counterStats1.Count, stats1.Count);
            Assert.Equal(counterStats1.Percent, stats1.Percentage);

            var stats2 = pollStatistics.Counters[1];
            Assert.Equal(counterStats2.Name, stats2.Name);
            Assert.Equal(counterStats2.Count, stats2.Count);
            Assert.Equal(counterStats2.Percent, stats2.Percentage);

        }
    }

    public class CounterStatistics
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public int Percent { get; set; }
    }

    public interface ICounterManager
    {
        ICollection<CounterStatistics> ResolvePercentage(ICollection<Counter> counters);
    }

    public class StatisticsInteractor
    {
        private IVotingSystemPersistance _persistance;
        private ICounterManager _counterManager;

        public StatisticsInteractor(IVotingSystemPersistance persistance, ICounterManager counterManager)
        {
             _persistance= persistance;
             _counterManager = counterManager;
        }

        public VotingPoll GetStatistics(int pollId)
        {
            return _persistance.GetPoll(pollId);
        }
    }

}






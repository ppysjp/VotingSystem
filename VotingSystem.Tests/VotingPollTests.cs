using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  Xunit;
using static Xunit.Assert;

namespace VotingSystem.Tests
{
    public class VotingPollTests
    {
        [Fact]
        public void ZeroCountersWhenCreated() 
        {
            var poll = new VotingPoll();
            Empty(poll.Counters);
        }
    }
    public class VotingPollFactoryTests
    {

        private VotingPollFactory _factory = new VotingPollFactory();
        private string[] _names = new[] { "names1", "name2" };
        private string _title = "title";

        [Fact]
        public void Create_AddsCounterToThePollForEachName() 
        {
            var poll = _factory.Create("", _names);

            foreach (var name in _names)
            {
                Contains(name, poll.Counters.Select(x => x.Name));
            }
        }

        [Fact]
        public void Create_ThrowIfLessThanTwoCounterNamesAndTitle() 
        {
            Throws<ArgumentException>(() => _factory.Create("", new[] { "name" }));
            Throws<ArgumentException>(() => _factory.Create("", new string[] { }));
        }

       [Fact]
        public void Create_ThrowIfLessThanTwoCounterNamesAndEmptyTitle() 
        {
            Throws<ArgumentException>(() => _factory.Create(_title, new[] { "name" }));
            Throws<ArgumentException>(() => _factory.Create(_title, new string[] { }));
        }

        [Fact]
        public void Create_AddsTitleToThePoll() 
        {
            var poll = _factory.Create(_title, _names);

            Equal(_title, poll.Title);
        }
 
    }

    public class VotingPollFactory
    {
        public VotingPollFactory()
        {
        }

        public VotingPoll Create(string title, string[] names)
        {
            if (names.Length < 2) throw new ArgumentException();

            return new VotingPoll
            {
                Title = title,
                Counters = names.Select(name => new Counter { Name = name })
            };
        }
    }

    public class VotingPoll
    {
        public VotingPoll()
        {
            Counters = Enumerable.Empty<Counter>();
        }

        public string Title { get; set; }

        public IEnumerable<Counter> Counters { get; set; }
    }
}


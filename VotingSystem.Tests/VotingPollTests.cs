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
        private string _description = "descrition";

        [Fact]
        public void Create_AddsCounterToThePollForEachName() 
        {
            var poll = _factory.Create(_title, _description, _names);

            foreach (var name in _names)
            {
                Contains(name, poll.Counters.Select(x => x.Name));
            }
        }

        [Fact]
        public void Create_ThrowIfEmptyTitle() 
        {
            Throws<ArgumentException>(() => _factory.Create("", _description, _names));
        }

        [Fact]
        public void Create_ThrowIfEmptyDescription() 
        {
            Throws<ArgumentException>(() => _factory.Create(_title, "", _names));
        }

       [Fact]
        public void Create_ThrowIfLessThanTwoCounterNames() 
        {
            Throws<ArgumentException>(() => _factory.Create(_title, _description, new[] { "name" }));
            Throws<ArgumentException>(() => _factory.Create(_title, _description, new string[] { }));
        }

        [Fact]
        public void Create_AddsTitleToThePoll() 
        {
            var poll = _factory.Create(_title, _description, _names);

            Equal(_title, poll.Title);
        }
        
        [Fact]
        public void Create_AddsDescriptionToThePoll() 
        {
            var poll = _factory.Create(_title, _description, _names);

            Equal(_description, poll.Description);
        }
 
    }

    public class VotingPollFactory
    {
        public VotingPollFactory()
        {
        }

        public VotingPoll Create(string title, string description, string[] names)
        {

            if (string.IsNullOrEmpty(title)) throw new ArgumentException();
            if (string.IsNullOrEmpty(description)) throw new ArgumentException();
            if (names.Length < 2) throw new ArgumentException();

            return new VotingPoll
            {
                Title = title,
                Description = description,
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
        public string Description { get; internal set; }
        public IEnumerable<Counter> Counters { get; set; }
    }
}


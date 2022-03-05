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

        private VotingPollFactory.Request _request = new VotingPollFactory.Request 
        { 
            Title = "title",
            Description = "descrition",
            Names = new[] { "names1", "name2" }
        };

        private string _title = "title";
        private string _description = "descrition";
        private string[] _names = new[] { "names1", "name2" };

        [Fact]
        public void Create_AddsCounterToThePollForEachName() 
        {
            var poll = _factory.Create(_request);

            foreach (var name in _names)
            {
                Contains(name, poll.Counters.Select(x => x.Name));
            }
        }

        //[Fact (Skip = "Exception thrown when ran interferes with workflow.")]
        [Fact]
        public void Create_ThrowIfRequestEmptyTitle() 
        {

            var request = new VotingPollFactory.Request 
            { 
                Title = "",
                Description = _description,
                Names = _names
            };

            var ex = Throws<ArgumentException>(() => _factory.Create(request));
            
        }

        //[Fact (Skip = "Exception thrown when ran interferes with workflow.")]
        [Fact]
        public void Create_ThrowIfRequestEmptyDescription() 
        {
            var request = new VotingPollFactory.Request 
            { 
                Title = _title,
                Description = "",
                Names = _names
            };

            Throws<ArgumentException>(() => _factory.Create(request));
        }

        //[Fact (Skip = "Exception thrown when ran interferes with workflow.")]
        [Fact]
        public void Create_ThrowIfRequestLessThanTwoCounterNames() 
        {
            var request1 = new VotingPollFactory.Request 
            { 
                Title = _title,
                Description = _description,
                Names = new string[] { }
            };

            var request2 = new VotingPollFactory.Request 
            { 
                Title = _title,
                Description = _description,
                Names = new[] { "name" }
            };

            Throws<ArgumentException>(() => _factory.Create(request1));
            Throws<ArgumentException>(() => _factory.Create(request2));
        }

        [Fact]
        public void Create_AddsTitleToThePoll() 
        {
            var poll = _factory.Create(_request);

            Equal(_title, poll.Title);
        }
        
        [Fact]
        public void Create_AddsDescriptionToThePoll() 
        {
            var poll = _factory.Create(_request);

            Equal(_description, poll.Description);
        }
 
    }

    public class VotingPollFactory
    {
        public class Request 
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string[] Names { get; set; }
        }
        
        public VotingPoll Create(Request request)
        {

            if (string.IsNullOrEmpty(request.Title)) throw new ArgumentException();
            if (string.IsNullOrEmpty(request.Description)) throw new ArgumentException();
            if (request.Names.Length < 2) throw new ArgumentException();

            return new VotingPoll
            {
                Title = request.Title,
                Description = request.Description,
                Counters = request.Names.Select(name => new Counter { Name = name })
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


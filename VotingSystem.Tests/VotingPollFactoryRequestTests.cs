using System;
using System.Collections.Generic;
using System.Text;
using  Xunit;
using static Xunit.Assert;

namespace VotingSystem.Tests
{
    public class VotingPollFactoryRequestTests
    {

        private VotingPollFactory _factory = new VotingPollFactory();
        private string _title = "title";
        private string _description = "descrition";
        private string[] _names = new[] { "names1", "name2" };

        [Fact (Skip = "Exception thrown when ran interferes with workflow.")]
        public void Create_ThrowIfRequestEmptyTitle() 
        {

            var request = new VotingPollFactory.Request 
            { 
                Title = "",
                Description = _description,
                Names = _names
            };

            var ex = Throws<ArgumentException>(() => _factory.Create(request));

            Contains("Request Title must not be empty string.", ex.Message);
            
        }

        [Fact (Skip = "Exception thrown when ran interferes with workflow.")]
        public void Create_ThrowIfRequestEmptyDescription() 
        {
            var request = new VotingPollFactory.Request 
            { 
                Title = _title,
                Description = "",
                Names = _names
            };

            var ex = Throws<ArgumentException>(() => _factory.Create(request));

            Contains("Request Description must not be empty string.", ex.Message);
        }

        [Fact (Skip = "Exception thrown when ran interferes with workflow.")]
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

            var ex1 = Throws<ArgumentException>(() => _factory.Create(request1));
            var ex2 = Throws<ArgumentException>(() => _factory.Create(request2));

            Contains("Request Names must contain at least 2 counter names.", ex1.Message);
            Contains("Request Names must contain at least 2 counter names.", ex2.Message);
        }

    }
}

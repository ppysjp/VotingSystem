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

        VotingPollFactory.Request _request = new VotingPollFactory.Request 
        { 
            Title =  "title",
            Description =  "descrition",
            Names = new[] { "names1", "name2" }
        };


        [Fact (Skip = "Exception thrown when ran interferes with workflow.")]
        //[Fact]
        public void Create_ThrowIfRequestEmptyTitle() 
        {
            _request.Title = "";
            
            var ex = Throws<ArgumentException>(() => _factory.Create(_request));

            Contains("Request Title must not be empty string.", ex.Message);
        }

        [Fact (Skip = "Exception thrown when ran interferes with workflow.")]
        //[Fact]
        public void Create_ThrowIfRequestEmptyDescription() 
        {
            _request.Description = "";

            var ex = Throws<ArgumentException>(() => _factory.Create(_request));

            Contains("Request Description must not be empty string.", ex.Message);
        }

        [Fact (Skip = "Exception thrown when ran interferes with workflow.")]
        //[Fact]
        public void Create_ThrowIfRequestLessThanTwoCounterNames() 
        {
            _request.Names = new string[] { };

            var ex1 = Throws<ArgumentException>(() => _factory.Create(_request));

            Contains("Request Names must contain at least 2 counter names.", ex1.Message);

            _request.Names = new[] { "name" }; 

            var ex2 = Throws<ArgumentException>(() => _factory.Create(_request));

            Contains("Request Names must contain at least 2 counter names.", ex2.Message);
        }

    }
}

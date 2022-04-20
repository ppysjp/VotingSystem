using System;
using System.Linq;
using VotingSystem.Models;

namespace VotingSystem
{
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

            if (string.IsNullOrEmpty(request.Title)) throw new ArgumentException("Request Title must not be empty string.");
            if (string.IsNullOrEmpty(request.Description)) throw new ArgumentException("Request Description must not be empty string.");
            if (request.Names.Length < 2) throw new ArgumentException("Request Names must contain at least 2 counter names.");

            return new VotingPoll
            {
                Title = request.Title,
                Description = request.Description,
                Counters = request.Names.Select(name => new Counter { Name = name }).ToList()
            };
        }
    }
}


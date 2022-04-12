using VotingSystem.Models;

namespace VotingSystem
{
    public interface IVotingPollFactory 
    {
        public VotingPoll Create(VotingPollFactory.Request request);
    }
}


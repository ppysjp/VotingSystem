using VotingSystem.Models;

namespace VotingSystem.Application
{
    public class VotingInteractor
    {
        private IVotingSystemPersistance _persistance;  

        public VotingInteractor(IVotingSystemPersistance persistance)
        {
            _persistance = persistance;
        }

        public void Vote(Vote vote)
        {
            if (!_persistance.VoteExists(vote))
            {
                _persistance.SaveVote(vote); 
            }
        }
    }
}

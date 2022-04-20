using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using VotingSystem.Models;

namespace VotingSystem.Application.Tests
{
    public class VotingInteractorTests
    {
        private Mock<IVotingSystemPersistance> _mockPersistance = new Mock<IVotingSystemPersistance>();
        
        [Fact]
        public void Vote_PersistsVote() 
        {
            var vote = new Vote();
            var interactor = new VotingInteractor(_mockPersistance.Object);

            interactor.Vote(vote);

            _mockPersistance.Verify(x => x.SaveVote(vote));
        }
        
        [Fact]
        public void Vote_DoesNotPersistsVoteWhenUserAlreadyVoted() 
        {
            var vote = new Vote();
            var interactor = new VotingInteractor(_mockPersistance.Object);
            _mockPersistance.Setup(x => x.VoteExists(vote)).Returns(true);

            interactor.Vote(vote);

            _mockPersistance.Verify(x => x.SaveVote(vote), Times.Never);
        }
 
    }

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

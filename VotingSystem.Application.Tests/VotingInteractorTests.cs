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
        private Vote _vote; 
        private VotingInteractor _interactor; 

        public VotingInteractorTests()
        {
            _vote = new Vote() { UserId = "user", CounterId = 1 };
            _interactor = new VotingInteractor(_mockPersistance.Object);
        }
        
        [Fact]
        public void Vote_PersistsVoteWhenUserHasNotAlreadyVoted() 
        {
            _mockPersistance.Setup(x => x.VoteExists(_vote)).Returns(false);

            _interactor.Vote(_vote);

            _mockPersistance.Verify(x => x.SaveVote(_vote));
        }
        
        [Fact]
        public void Vote_DoesNotPersistsVoteWhenUserHasAlreadyVoted() 
        {
            _mockPersistance.Setup(x => x.VoteExists(_vote)).Returns(true);

            _interactor.Vote(_vote);

            _mockPersistance.Verify(x => x.SaveVote(_vote), Times.Never);
        }
 
    }
}

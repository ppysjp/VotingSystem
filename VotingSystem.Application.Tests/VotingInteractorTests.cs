using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;

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
        }
        
    }

    public class Vote
    {
        public Vote()
        {
        }
    }

    public class VotingInteractor
    {
        private IVotingSystemPersistance @object;

        public VotingInteractor(IVotingSystemPersistance @object)
        {
            this.@object = @object;
        }
    }
}

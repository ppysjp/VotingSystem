using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using  Xunit;
using static Xunit.Assert;

namespace VotingSystem.Tests
{
    public class VotingPollInteractorTests
    {
        [Fact]
        public void CreateVotingPoll_UsesVotingPollFactoryToCreateVotingPoll()
        {
            var request = new VotingPollFactory.Request();
            var mockFactory = new Mock<IVotingPollFactory>();
            var mockPersistance = new Mock<IVotingSystemPersistance>();
            var interactor = new VotingPollInteractor(mockFactory.Object, mockPersistance.Object);

            interactor.CreateVotingPoll(request);

            mockFactory.Verify(x => x.Create(request));
        }

        [Fact]
        public void CreateVoting_PersistsCreatedPoll() 
        { 
            var request = new VotingPollFactory.Request();
            var mockFactory = new Mock<IVotingPollFactory>();
            var mockPersistance = new Mock<IVotingSystemPersistance>();

            var poll = new VotingPoll();
            mockFactory.Setup(x => x.Create(request)).Returns(poll);

            var interactor = new VotingPollInteractor(mockFactory.Object, mockPersistance.Object);

            interactor.CreateVotingPoll(request);

            mockPersistance.Verify(x => x.SaveVotingPoll(poll));
        }
    }

    public interface IVotingSystemPersistance
    {
        void SaveVotingPoll(VotingPoll votingPoll);
    }

    public class VotingPollInteractor
    {
        private readonly IVotingPollFactory _factory;
        private readonly IVotingSystemPersistance _peristance;

        public VotingPollInteractor(IVotingPollFactory factory, IVotingSystemPersistance persistance)
        {
            _factory = factory;
            _peristance = persistance;
        }

        internal void CreateVotingPoll(VotingPollFactory.Request request)
        {
            var poll = _factory.Create(request);
            _peristance.SaveVotingPoll(poll);
        }
    }

}

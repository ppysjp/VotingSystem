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
            var interactor = new VotingPollInteractor(mockFactory.Object);

            interactor.CreateVotingPoll(request);

            mockFactory.Verify(x => x.Create(request));
        }
    }

    public class VotingPollInteractor
    {
        private readonly IVotingPollFactory _factory;

        public VotingPollInteractor(IVotingPollFactory factory)
        {
            _factory = factory;
        }

        internal void CreateVotingPoll(VotingPollFactory.Request request)
        {
            _factory.Create(request);
        }
    }
}

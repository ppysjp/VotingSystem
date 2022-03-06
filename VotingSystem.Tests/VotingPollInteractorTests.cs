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
            var mockLibrary = new Mock<VotingPollFactory>();
            var factory = new VotingPollFactory();
            var interactor = new VotingPollInteractor(mockLibrary.Object);

            interactor.CreateVotingPoll(request);

            mockLibrary.Verify(x => x.Create(request));
        }
    }

    public class VotingPollInteractor
    {
        private readonly VotingPollFactory _factory;

        public VotingPollInteractor(VotingPollFactory factory)
        {
            _factory = factory;
        }

        internal void CreateVotingPoll(VotingPollFactory.Request request)
        {
            _factory.Create(request);
        }
    }
}

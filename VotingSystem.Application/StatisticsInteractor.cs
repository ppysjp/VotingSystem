﻿namespace VotingSystem.Application
{
    public class StatisticsInteractor
    {
        private IVotingSystemPersistance _persistance;
        private ICounterManager _counterManager;

        public StatisticsInteractor(IVotingSystemPersistance persistance, ICounterManager counterManager)
        {
             _persistance= persistance;
             _counterManager = counterManager;
        }

        public PollStatistics GetStatistics(int pollId)
        {
            var poll = _persistance.GetPoll(pollId);
            var statistics = _counterManager.GetStatistics(poll.Counters);
            _counterManager.ResolveExcess(statistics);

            return new PollStatistics
            {
                Title = poll.Title,
                Description = poll.Description,
                Counters = statistics
            };
        }
    }


}






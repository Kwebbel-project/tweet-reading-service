using tweet_reading_service.Data;
using tweet_reading_service.Models;
using tweet_reading_service.Services.Interfaces;

namespace tweet_reading_service.Services
{
    public class FeedService : IFeedService
    {
        private readonly ITweetRepository _repository;
        public FeedService(ITweetRepository repository)
        { 
            _repository = repository;
        }

        public List<Tweet> GetTweets()
        {
            return _repository.GetAllTweets();
        }
    }
}

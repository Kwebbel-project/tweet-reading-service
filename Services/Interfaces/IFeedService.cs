using tweet_reading_service.Models;

namespace tweet_reading_service.Services.Interfaces
{
    public interface IFeedService
    {
        List<Tweet> GetTweets();
    }
}

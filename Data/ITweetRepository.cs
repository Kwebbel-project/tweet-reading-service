using tweet_reading_service.Models;

namespace tweet_reading_service.Data
{
    public interface ITweetRepository
    {
        void CreateTweet(Tweet tweet);
        Tweet? GetTweetById(int id);
        List<Tweet?> GetAllTweets();
        void UpdateTweet(Tweet tweet); 
        void DeleteTweet(int id);
    }
}

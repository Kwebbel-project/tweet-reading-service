using StackExchange.Redis;
using System.Text.Json;
using tweet_reading_service.Models;
using static System.Net.Mime.MediaTypeNames;

namespace tweet_reading_service.Data
{
    public class RedisTweetRepository : ITweetRepository
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisTweetRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }
        public void CreateTweet(Tweet tweet)
        {
            if (tweet != null)
            {
                var db = _redis.GetDatabase();

                var serialTweet = JsonSerializer.Serialize(tweet);
                db.StringSet(tweet.Id, serialTweet, TimeSpan.FromDays(3));
                db.SetAdd("TweetsSet", serialTweet);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(tweet));
            }
        }

        public void DeleteTweet(int id)
        {
            throw new NotImplementedException();
        }

        public List<Tweet?> GetAllTweets()
        {
            var db = _redis.GetDatabase();

            var completeSet = db.SetMembers("TweetsSet");

            if (completeSet.Length > 0)
            {
                return Array.ConvertAll(completeSet, val => JsonSerializer.Deserialize<Tweet>(val)).ToList();
            }
            return null;
        }

        public Tweet? GetTweetById(int id)
        {
            var db = _redis.GetDatabase();
            var tweet = db.StringGet(id.ToString());

            if (!string.IsNullOrEmpty(tweet))
            {
                return JsonSerializer.Deserialize<Tweet>(tweet);
            }
            else
            {
                return null;
            }
        }

        public void UpdateTweet(Tweet tweet)
        {
            throw new NotImplementedException();
        }
    }
}

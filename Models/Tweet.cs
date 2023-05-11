using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace tweet_reading_service.Models
{
    public class Tweet
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public int Likes { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
    }
}

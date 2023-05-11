using System.ComponentModel.DataAnnotations;

namespace tweet_service.Models.Dto
{
    public class TweetReadDto
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public int Likes;
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public long userId { get; set; }
    }
}

using Confluent.Kafka;
using Kafka.Public;
using Kafka.Public.Loggers;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Text;
using System.Text.Json;
using tweet_reading_service.Data;
using tweet_reading_service.Models;

namespace tweet_reading_service.Common.Kafka
{
    public class KafkaConsumerHandler : IHostedService
    {
        private ClusterClient _cluster;
        private readonly ITweetRepository _repository;

        public KafkaConsumerHandler(ITweetRepository tweetRepository)
        {
                _repository = tweetRepository;

                _cluster = new ClusterClient(new Configuration
                {
                    Seeds = "localhost:9092"
                }, new ConsoleLogger());
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cluster.ConsumeFromLatest("TWEET_CREATED");
            _cluster.MessageReceived += record =>
            {
                try
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    string json = Encoding.UTF8.GetString(record.Value as byte[]);
                    Tweet tweet = JsonSerializer.Deserialize<Tweet>(json, options);

                    _repository.CreateTweet(tweet);
                }
                catch (JsonException ex)
                {
                   

                }

            };
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cluster.Dispose();
            return Task.CompletedTask;
        }
    }
}

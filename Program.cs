using StackExchange.Redis;
using tweet_reading_service.Common.Kafka;
using tweet_reading_service.Data;
using tweet_reading_service.Services.Interfaces;
using tweet_reading_service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var multiplexer = ConnectionMultiplexer.Connect("localhost");
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);
builder.Services.AddSingleton<ITweetRepository, RedisTweetRepository>();
builder.Services.AddScoped<IFeedService, FeedService>();

// Add the KafkaConsumerHandler as a hosted service.
builder.Services.AddSingleton<IHostedService, KafkaConsumerHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseWebSockets();

app.UseAuthorization();
app.MapControllers();

app.Run();

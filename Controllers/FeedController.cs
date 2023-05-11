using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text;
using tweet_reading_service.Models;
using tweet_reading_service.Services.Interfaces;
using tweet_reading_service.Services;
using Microsoft.Extensions.Logging;

namespace tweet_reading_service.Controllers
{
    [Route("api/feed")]
    [ApiController]
    public class FeedController : ControllerBase
    {
        private readonly ILogger<FeedController> _logger;

        private readonly IFeedService _feedService;
        public FeedController(IFeedService feedService, ILogger<FeedController> logger) 
        {
            _feedService = feedService;
            _logger = logger;

        }

        [HttpGet("/ws")]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using (WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync())
                {
                    _logger.Log(LogLevel.Information, "WebSocket connection established");

                    while (webSocket.State == WebSocketState.Open)
                    {
                        var dataJson = JsonSerializer.Serialize(_feedService.GetTweets());
                        var dataBytes = Encoding.UTF8.GetBytes(dataJson);
                        var dataBuffer = new ArraySegment<byte>(dataBytes);

                        await webSocket.SendAsync(dataBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                        await Task.Delay(5000);
                    }
                }
                
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }
    }
}

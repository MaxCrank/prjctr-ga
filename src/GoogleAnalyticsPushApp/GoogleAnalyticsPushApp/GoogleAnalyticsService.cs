using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleAnalyticsPushApp
{
    public class GoogleAnalyticsService : BackgroundService
    {
        private IHttpClientFactory _clientFactory;

        public GoogleAnalyticsService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var gaClient = _clientFactory.CreateClient("ga");
            var nbuClient = _clientFactory.CreateClient("nbu");

            while (!stoppingToken.IsCancellationRequested)
            {
                var nbuJsonString = await nbuClient.GetStringAsync("NBUStatService/v1/statdirectory/exchange?json");
                var nbuJson = JsonSerializer.Deserialize<List<NbuRate>>(nbuJsonString);
                var rate = nbuJson.First(r => r.Cc == "USD").Rate;

                var query = new Dictionary<string, string>();
                query.Add("v", "v1");
                query.Add("t", "event");
                query.Add("cid", "1111");
                query.Add("tid", "UA-212233624-1");
                query.Add("ec", "finance");
                query.Add("ea", "change");
                query.Add("el", "usd_to_uah");
                query.Add("ev", ((int)(rate * 1000)).ToString());
                query.Add("rate", rate.ToString());
                var content = new FormUrlEncodedContent(query);

                await gaClient.PostAsync("collect", content);
                await Task.Delay(5000);
            }
        }
    }
}

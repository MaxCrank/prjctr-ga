using System;
using System.Text.Json.Serialization;

namespace GoogleAnalyticsPushApp
{
    [Serializable]
    public class NbuRate
    {
        [JsonPropertyName("r030")]
        public int R030 { get; set; }

        [JsonPropertyName("txt")]
        public string Txt { get; set; }

        [JsonPropertyName("rate")]
        public decimal Rate { get; set; }

        [JsonPropertyName("cc")]
        public string Cc { get; set; }

        [JsonPropertyName("exchangedate")]
        public string ExchangeDate { get; set; }
    }
}

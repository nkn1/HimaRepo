using Newtonsoft.Json;

namespace nkn1
{
    public class HoroscopeObject
    {
        [JsonProperty("horoscope")]
        public Horoscope[] Horoscopes { get; set; }
    }

    public class Horoscope
    {
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("item")]
        public string Item { get; set; }
        [JsonProperty("money")]
        public string Money { get; set; }
        [JsonProperty("total")]
        public string Total { get; set; }
        [JsonProperty("job")]
        public string Job { get; set; }
        [JsonProperty("color")]
        public string Color { get; set; }
        [JsonProperty("love")]
        public string Love { get; set; }
        [JsonProperty("rank")]
        public string Rank { get; set; }
        [JsonProperty("sign")]
        public string Sign { get; set; }
    }
}
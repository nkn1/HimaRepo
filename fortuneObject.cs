using Newtonsoft.Json;

namespace nkn1
{
    /// <summary>
    /// 12星座分の占い
    /// </summary>
    public class HoroscopeObject
    {
        /// <value>星座占い（12星座分を配列で提供）</value>
        [JsonProperty("horoscope")]
        public Horoscope[] Horoscopes { get; set; }
    }

    /// <summary>
    /// 星座占い
    /// </summary>
    public class Horoscope
    {
        /// <value>内容</value>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <value>ラッキーアイテム</value>
        [JsonProperty("item")]
        public string Item { get; set; }

        /// <value>金銭運</value>
        [JsonProperty("money")]
        public string Money { get; set; }

        /// <value>総合運</value>
        [JsonProperty("total")]
        public string Total { get; set; }

        /// <value>仕事運</value>
        [JsonProperty("job")]
        public string Job { get; set; }

        /// <value>ラッキーカラー</value>
        [JsonProperty("color")]
        public string Color { get; set; }

        /// <value>恋愛運</value>
        [JsonProperty("love")]
        public string Love { get; set; }

        /// <value>順位</value>
        [JsonProperty("rank")]
        public string Rank { get; set; }

        /// <value>星座</value>
        [JsonProperty("sign")]
        public string Sign { get; set; }
    }
}
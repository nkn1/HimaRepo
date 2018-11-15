using Newtonsoft.Json;

namespace nkn1
{
    /// <summary>
    /// 回答パターンリスト
    /// </summary>
    public class AnswerPatternObject
    {
        /// <value>キーワードと回答の組み合わせリスト</value>
        [JsonProperty("AnswerPattern")]
        public AnswerPattern[] AnswerPatterns { get; set; }
    }

    /// <summary>
    /// キーワードと回答の組み合わせ
    /// </summary>
    public class AnswerPattern
    {
        /// <value>キーワード</value>
        [JsonProperty("keyword")]
        public string Keyword { get; set; }

        /// <value>回答</value>
        [JsonProperty("answer")]
        public string Answer { get; set; }
    }
}
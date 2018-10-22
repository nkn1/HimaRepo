using Newtonsoft.Json;

namespace nkn1
{
    public class AnswerPatternObject
    {
        [JsonProperty("AnswerPattern")]
        public AnswerPattern[] AnswerPatterns { get; set; }
    }

    public class AnswerPattern
    {
        [JsonProperty("keyword")]
        public string Keyword { get; set; }
        [JsonProperty("answer")]
        public string Answer { get; set; }
    }
}
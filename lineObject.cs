using Newtonsoft.Json;

namespace nkn1
{
    /// <summary>
    /// LINEイベント オブジェクト
    /// </summary>
    public class LineEventObject
    {
        /// <value>LINEイベントのリスト</value>
        [JsonProperty("events")]
        public Event[] Events { get; set; }
    }

    /// <summary>
    /// LINEイベント
    /// </summary>
    public class Event
    {
        /// <value>応答トークン</value>
        [JsonProperty("replyToken")]
        public string ReplyToken { get; set; }

        /// <value>イベント種別</value>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <value>送信元</value>
        [JsonProperty("source")]
        public Source Source { get; set; }

        /// <value>イベント メッセージ</value>
        [JsonProperty("message")]
        public EventMessage Message { get; set; }
    }

    /// <summary>
    /// 送信元
    /// </summary>
    public class Source
    {
        /// <value>送信元の種別</value>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <value>送信元のID</value>
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }

    /// <summary>
    /// メッセージ イベント
    /// </summary>
    public class EventMessage
    {
        /// <value>メッセージID</value>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <value>メッセージ種別</value>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <value>メッセージ テキスト</value>
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    /// <summary>
    /// 応答メッセージ オブジェクト
    /// </summary>
    public class ReplyMessageObject
    {
        /// <value>応答トークン</value>
        [JsonProperty("replyToken")]
        public string ReplyToken { get; set; }

        /// <value>応答メッセージ群（最大５個まで）</value>
        [JsonProperty("messages")]
        public UnitMessage[] Messages { get; set; }
    }

    /// <summary>
    /// プッシュ型メッセージ オブジェクト
    /// </summary>
    public class PushMessageObject
    {
        /// <value>宛先ID</value>
        [JsonProperty("to")]
        public string To { get; set; }

        /// <value>プッシュ型メッセージ群（最大５個まで）</value>
        [JsonProperty("messages")]
        public UnitMessage[] Messages { get; set; }
    }

    /// <summary>
    /// 単体メッセージ
    /// </summary>
    public class UnitMessage
    {
        /// <value>メッセージ種別</value>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <value>メッセージテキスト</value>
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}

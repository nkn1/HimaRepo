using Newtonsoft.Json;

namespace nkn1
{
    public class LineEventObject
    {
        [JsonProperty("events")]
        public Event[] Events { get; set; }
    }

    public class Event
    {
        [JsonProperty("replyToken")]
        public string ReplyToken { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
        [JsonProperty("source")]
        public Source Source { get; set; }
        [JsonProperty("message")]
        public EventMessage Message { get; set; }
    }
    public class Source
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }

    public class EventMessage
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class ReplyMessageObject
    {
        [JsonProperty("replyToken")]
        public string ReplyToken { get; set; }
        [JsonProperty("messages")]
        public ReplyMessage[] Messages { get; set; }
    }

    public class ReplyMessage
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("packageId")]
        public string PackageId { get; set; }
        [JsonProperty("stickerId")]
        public string StickerId { get; set; }
        [JsonProperty("originalContentUrl")]
        public string OriginalContentUrl { get; set; }
        [JsonProperty("previewImageUrl")]
        public string PreviewImageUrl { get; set; }
    }
    public class PushMessageObject
    {
        [JsonProperty("to")]
        public string To { get; set; }
        [JsonProperty("messages")]
        public PushMessage[] Messages { get; set; }
    }

    public class PushMessage
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}

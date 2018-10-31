using Newtonsoft.Json;

namespace nkn1
{
    public class FaceObject
    {
        [JsonProperty("faceRectangle")]
        public FaceRectangle faceRectangle { get; set; }

        [JsonProperty("faceAttributes")]
        public FaceAttributes faceAttributes { get; set; }
    }

    public class FaceRectangle
    {
        [JsonProperty("top")]
        public int Top { get; set; }

        [JsonProperty("left")]
        public int Left { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }
    
    public class FaceAttributes
    {
        [JsonProperty("emotion")]
        public Emotion emotion { get; set; }
    }
    
    public class Emotion
    {
        [JsonProperty("anger")]
        public float Anger { get; set; }

        [JsonProperty("contempt")]
        public float Contempt { get; set; }

        [JsonProperty("disgust")]
        public float Disgust { get; set; }

        [JsonProperty("fear")]
        public float Fear { get; set; }

        [JsonProperty("happiness")]
        public float Happiness { get; set; }

        [JsonProperty("neutral")]
        public float Neutral { get; set; }

        [JsonProperty("sadness")]
        public float Sadness { get; set; }

        [JsonProperty("surprise")]
        public float Surprise { get; set; }
    }
    
}
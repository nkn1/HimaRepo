using Newtonsoft.Json;

namespace nkn1
{
    /// <summary>
    /// Microsoft Azure Face APIの応答オブジェクト
    /// </summary>
    public class FaceObject
    {
        /// <value>検出した顔を囲う矩形</value>
        [JsonProperty("faceRectangle")]
        public FaceRectangle faceRectangle { get; set; }

        /// <value>検出した顔の属性</value>
        [JsonProperty("faceAttributes")]
        public FaceAttributes faceAttributes { get; set; }
    }

    /// <summary>
    /// 検出した顔を囲う矩形
    /// </summary>
    public class FaceRectangle
    {
        /// <value>上辺 y座標</value>
        [JsonProperty("top")]
        public int Top { get; set; }

        /// <value>左辺 x座標</value>
        [JsonProperty("left")]
        public int Left { get; set; }

        /// <value>幅</value>
        [JsonProperty("width")]
        public int Width { get; set; }

        /// <value>高さ</value>
        [JsonProperty("height")]
        public int Height { get; set; }
    }
    
    /// <summary>
    /// 検出した顔の属性
    /// </summary>
    public class FaceAttributes
    {
        /// <value>感情</value>
        [JsonProperty("emotion")]
        public Emotion emotion { get; set; }
    }
    
    /// <summary>
    /// 感情
    /// </summary>
    public class Emotion
    {
        /// <value>怒り</value>
        [JsonProperty("anger")]
        public float Anger { get; set; }

        /// <value>侮辱</value>
        [JsonProperty("contempt")]
        public float Contempt { get; set; }

        /// <value>嫌悪</value>
        [JsonProperty("disgust")]
        public float Disgust { get; set; }

        /// <value>恐れ</value>
        [JsonProperty("fear")]
        public float Fear { get; set; }

        /// <value>幸福</value>
        [JsonProperty("happiness")]
        public float Happiness { get; set; }

        /// <value>無表情</value>
        [JsonProperty("neutral")]
        public float Neutral { get; set; }

        /// <value>悲しみ</value>
        [JsonProperty("sadness")]
        public float Sadness { get; set; }

        /// <value>驚き</value>
        [JsonProperty("surprise")]
        public float Surprise { get; set; }
    }
    
}
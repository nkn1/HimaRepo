
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace nkn1
{
    public static class answer
    {
        [FunctionName("answer")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger]HttpRequestMessage req, ILogger log)
        {
            var jsonContent = await req.Content.ReadAsStringAsync();
            var eventObj = JsonConvert.DeserializeObject<LineEventObject>(jsonContent);
            var data = eventObj.Events[0];

            var replyMessage = "";
            if (data.Type == "message")
            {
                switch (data.Message.Type)
                {
                case "text":
                    // 応答メッセージ本文
                    replyMessage = GetReplyMessage(data.Message.Text, log);
                    break;

                case "image":
                    replyMessage = await GetEmotion(data.Message.Id, log);
                    break;

                default:
                    // NOP
                    break;
                }
            }

            if (0 < replyMessage.Length)
            {
                var result = await ReplyText(replyMessage, data.ReplyToken);
            }

            return req.CreateResponse(HttpStatusCode.OK);
        }


        /// <summary>
        /// LINEへの返信メッセージ本文を返す。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="log"></param>
        private static string GetReplyMessage(string message, ILogger log)
        {
            var reply = "";
            var answerPatternFile = System.Environment.GetEnvironmentVariable("ANSWER_PATTERN_FILE");
            var answerPatternText = File.ReadAllText(answerPatternFile);
            var answerPatternObject = JsonConvert.DeserializeObject<AnswerPatternObject>(answerPatternText);

            foreach (var pattern in answerPatternObject.AnswerPatterns)
            {
                if (message.Contains(pattern.Keyword))
                {
                    reply = pattern.Answer;
                    break;
                }
            }
            return reply;
        }


        /// <summary>
        /// LINEへ投稿された写真に写っている人物の感情を返す。
        /// </summary>
        /// <param name="id"></param>
        private static async Task<string> GetEmotion(string id, ILogger log)
        {
            string emotionKeyWord = "";
            using (var client = new HttpClient())
            {
                // LINEへ投稿された画像(JPEG形式のバイナリデータ)を取得する。
                var lineChannelAccessToken = System.Environment.GetEnvironmentVariable("LINE_CHANNEL_ACCESS_TOKEN");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {{{lineChannelAccessToken}}}");
                var imageData = await client.GetByteArrayAsync($"https://api.line.me/v2/bot/message/{id}/content");

                // FACE API呼出し
                var subscriptionKey = System.Environment.GetEnvironmentVariable("AZURE_FACE_API_KEY");
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                using (var content = new ByteArrayContent(imageData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    var faceApiUri = "https://japaneast.api.cognitive.microsoft.com/face/v1.0/detect?returnFaceId=false&returnFaceLandmarks=false&returnFaceAttributes=emotion";
                    var res = await client.PostAsync(faceApiUri, content);
                    var body = await res.Content.ReadAsStringAsync();
                    var faceObj = JsonConvert.DeserializeObject<FaceObject[]>(body);
                    if (0 < faceObj.Length)
                    {
                        var emotion = faceObj[0].faceAttributes.emotion;
                        var threshold = 0.5;
                        if (threshold < emotion.Happiness)
                        {
                            emotionKeyWord = "EMOTION_HAPPINESS";
                        }
                        else if (threshold < emotion.Anger)
                        {
                            emotionKeyWord = "EMOTION_ANGER";
                        }
                        else if (threshold < emotion.Contempt)
                        {
                            emotionKeyWord = "EMOTION_CONTEMPT";
                        }
                        else if (threshold < emotion.Disgust)
                        {
                            emotionKeyWord = "EMOTION_DISGUST";
                        }
                        else if (threshold < emotion.Fear)
                        {
                            emotionKeyWord = "EMOTION_FEAR";
                        }
                        else if (threshold < emotion.Neutral)
                        {
                            emotionKeyWord = "EMOTION_NEUTRAL";
                        }
                        else if (threshold < emotion.Sadness)
                        {
                            emotionKeyWord = "EMOTION_SADNESS";
                        }
                        else if (threshold < emotion.Surprise)
                        {
                            emotionKeyWord = "EMOTION_SUPRIZE";
                        }
                        else
                        {
                            // NOP
                        }
                    }
                }
            }
            return GetReplyMessage(emotionKeyWord, log);
        }


        private static async Task<bool> ReplyText(string message, string replyToken)
        {
            using (var client = new HttpClient())
            {

                // 認証情報
                var lineChannelAccessToken = System.Environment.GetEnvironmentVariable("LINE_CHANNEL_ACCESS_TOKEN");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {{{lineChannelAccessToken}}}");

                // LINEメッセージ返信
                var res = await client.PostAsJsonAsync("https://api.line.me/v2/bot/message/reply",
                    // 送信データを作成
                    new ReplyMessageObject()
                    {
                        ReplyToken = replyToken,
                        Messages = new ReplyMessage[]
                                    {
                                        new ReplyMessage()
                                        {
                                            Type = "text",
                                            Text = message
                                        }
                                    }
                    }
                );
            }
            return true;
        }
    }
}

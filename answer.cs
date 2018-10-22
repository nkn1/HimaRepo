
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
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

            if (data.Type == "message")
            {
                // 応答メッセージ本文
                if (GenerateReplyMessage(data.Message.Text, out string replyMessage, log))
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
                                ReplyToken = data.ReplyToken,
                                Messages = new ReplyMessage[]
                                            {
                                                new ReplyMessage()
                                                {
                                                    Type = "text",
                                                    Text = replyMessage
                                                }
                                            }
                            }
                        );
                    }
                }
            }
            return req.CreateResponse(HttpStatusCode.OK);
        }

        private static bool GenerateReplyMessage(string message, out string reply, ILogger log)
        {
            reply = "";
            var isNeedReply = true;
            var AnswerPatternFile = System.Environment.GetEnvironmentVariable("ANSWER_PATTERN_FILE");
            var AnswerPatternText = File.ReadAllText(AnswerPatternFile);
            var answerPatternObject = JsonConvert.DeserializeObject<AnswerPatternObject>(AnswerPatternText);

            foreach (var pattern in answerPatternObject.AnswerPatterns)
            {
                if (message.Contains(pattern.Keyword))
                {
                    reply = pattern.Answer;
                    break;
                }
            }
            return isNeedReply;
        }
    }
}

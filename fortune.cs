using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace nkn1
{
    public static class fortune
    {
        [FunctionName("fortune")]
        public static async void Run([TimerTrigger("0 5 0 * * *")]TimerInfo myTimer, ILogger log)
        {
            var (libra, pisces) = await GetHoroscope(log);

            using (var client = new HttpClient())
            {

                // 認証情報
                var lineChannelAccessToken = System.Environment.GetEnvironmentVariable("LINE_CHANNEL_ACCESS_TOKEN");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {{{lineChannelAccessToken}}}");

                // LINEプッシュメッセージ送信
                var res = await client.PostAsJsonAsync("https://api.line.me/v2/bot/message/push",
                            // 送信データを作成
                            new PushMessageObject()
                            {
                                To = System.Environment.GetEnvironmentVariable("LINE_PUSH_TO"),
                                Messages = new UnitMessage[]
                                            {
                                                new UnitMessage()
                                                {
                                                    Type = "text",
                                                    Text = "あたちは ＨＩ・ＭＡ・ＲＩ。\n"
                                                        + "四角い者・・・。\n"
                                                        + "トトとカカの運勢を占ってあげうからネ。\n"
                                                        + "聞いてる？"
                                                },
                                                new UnitMessage()
                                                {
                                                    Type = "text",
                                                    Text = libra
                                                },
                                                new UnitMessage()
                                                {
                                                    Type = "text",
                                                    Text = pisces
                                                },
                                                new UnitMessage()
                                                {
                                                    Type = "text",
                                                    Text = "powerd by JugemKey\n"
                                                         + "http://jugemkey.jp/api/waf/api_free.php\n"
                                                         + "【PR】原宿占い館 塔里木\n"
                                                         + "http://www.tarim.co.jp/"
                                                }
                                            }
                            }
                );
            }
        }

        private static async Task<(string libra, string pisces)> GetHoroscope(ILogger log)
        {
            var libra = "天秤座の運勢は、、、お休みちぅ";
            var pisces = "魚座の運勢は、、、お休みちぅ";
            var today = System.DateTime.Today.ToString("yyyy/MM/dd");

            using (var client = new HttpClient())
            {
                var res = await client.GetAsync($"http://api.jugemkey.jp/api/horoscope/free/{today}");
                var jsonContent = await res.Content.ReadAsStringAsync();
                var startPos = ($@"{{""horoscope"":{{""{today}"":").Length;
                var subJsonContentLength = jsonContent.Length - startPos - 1;
                var subJsonContent = @"{""horoscope"":" + jsonContent.Substring(startPos, subJsonContentLength);
                var horoscopeObj = JsonConvert.DeserializeObject<HoroscopeObject>(subJsonContent);

                foreach (var data in horoscopeObj.Horoscopes)
                {
                    var CommonMsg = $"{today}の{data.Sign}は第{data.Rank}位～。\n" + 
                                    $"「{data.Content}」だって。\n" +
                                    $"らっきーあいてむはー「{data.Item}」でー、" +
                                    $"らっきーからーはー「{data.Color}」だからネ。\n";

                    switch (data.Sign)
                    {
                        case "天秤座":
                            libra =  CommonMsg + "わかった？";
                            break;

                        case "魚座":
                            pisces = CommonMsg + "いい？";
                            break;

                        default:
                            break;
                    }
                }
            }

            return (libra, pisces);
        }
    }
}

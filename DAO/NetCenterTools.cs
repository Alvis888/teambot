using teambot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using teambot.Controllers;

namespace teambot.DAO
{
    public class NetCenterTools
    {
        xmlController xml = new xmlController();
        /// <summary>
        /// 调用LUIS
        /// </summary>
        /// <param name="Query">提问</param>
        /// <returns></returns>
        private static async Task<Rootobject> GetEntityFromLUIS(string Query)
        {
            Query = Uri.EscapeDataString(Query);
            Rootobject Data = new Rootobject();
            using (HttpClient client = new HttpClient())
            {
                string RequestURI = " https://api.projectoxford.ai/luis/v1/application?id=d685b5c0-1b94-4************80c02a5c36&subscription-key=356220b970***********36380da&q=" + Query;
                HttpResponseMessage msg = await client.GetAsync(RequestURI);

                if (msg.IsSuccessStatusCode)
                {
                    var JsonDataResponse = await msg.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<Rootobject>(JsonDataResponse);
                }
            }
            return Data;
        }
        public async Task<string> Tools(string requestKey, string requestFrom)
        {
            string returnInfo = "";
            Rootobject StLUIS = await GetEntityFromLUIS(requestKey);
            switch (StLUIS.intents[0].intent)
            {
                case "phoneNumber":
                    if (StLUIS.entities.Length > 0)
                        returnInfo = xml.GetPhoneNumber(StLUIS.entities[0].entity);
                    else
                    {
                        string[] defaultReturnArray = { "请检查一下被查询人的姓名", "请检查姓名是否正确", "您所查询的可能不是实验室成员" };
                        Random random = new Random();
                        int index = random.Next(0, defaultReturnArray.Length);
                        returnInfo = defaultReturnArray[index];
                    }
                    return returnInfo;

                //case "StockPrice2":
                //    StockRateString = await GetStock(StLUIS.entities[0].entity);
                //    break;
                case "email":
                    if (StLUIS.entities.Length > 0)
                        returnInfo = xml.getEmail(StLUIS.entities[0].entity);
                    else
                    {
                        string[] defaultReturnArray = { "请检查一下被查询人的姓名", "请检查姓名是否正确", "您所查询的可能不是实验室成员","抱歉，查无此人" };
                        Random random = new Random();
                        int index = random.Next(0, defaultReturnArray.Length);
                        returnInfo = defaultReturnArray[index];
                    }
                   
                    return returnInfo;
                case "detailInfo":
                    if (StLUIS.entities.Length > 0)
                        returnInfo = xml.getAllInfo(StLUIS.entities[0].entity);
                    else
                    {
                        string[] defaultReturnArray = { "请检查一下被查询人的姓名", "请检查姓名是否正确", "这不是实验室成员吧" };
                        Random random = new Random();
                        int index = random.Next(0, defaultReturnArray.Length);
                        returnInfo = defaultReturnArray[index];
                    }
                    return returnInfo;
                default:
                    {
                        string[] defaultReturnArray = { "抱歉，我不太理解您的问题，请尝试输入“张三的电话”", "对不起，我不明白您的意思,请尝试输入“张三的电话”", "没看太懂，请尝试输入“张三的电话”", "刚学会说话，还听不太懂...请尝试输入“张三的电话”" };
                        Random random = new Random();
                        int index = random.Next(0, defaultReturnArray.Length);
                        return defaultReturnArray[index];
                    }
            }
        }
    }
}

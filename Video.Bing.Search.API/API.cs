using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Video.Bing.Search.API.Helpers;

namespace Video.Bing.Search.API
{
    public class API
    {
        const string accessKey = "enter key here";
        const string uriBase = "https://api.cognitive.microsoft.com/bing/v7.0/videos/search";

        public async static Task<string> Search(string textInput)
        {
            BingVideoResult result = Query(textInput);
            return JsonTools.ToJson(result.jsonResult);
        }

        static BingVideoResult Query(string searchQuery)
        {
            var uri = uriBase + "?q=" + Uri.EscapeDataString(searchQuery);
            WebRequest webRequest = HttpWebRequest.Create(uri);
            webRequest.Headers["Ocp-Apim-Subscription-Key"] = accessKey;
            HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponseAsync().Result;
            string json = new StreamReader(httpWebResponse.GetResponseStream()).ReadToEnd();

            var bingVideoResult = new BingVideoResult();
            bingVideoResult.jsonResult = json;
            bingVideoResult.relevantHeaders = new Dictionary<String, String>();

            foreach (String header in httpWebResponse.Headers)
            {
                if (header.StartsWith("BingAPIs-") || header.StartsWith("X-MSEdge-"))
                    bingVideoResult.relevantHeaders[header] = httpWebResponse.Headers[header];
            }
            return bingVideoResult;
        }
    }
}

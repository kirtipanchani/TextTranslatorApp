using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TextTranslatorApp.OnException;
using TextTranslatorApp.Service;

namespace TextTranslatorApp.ServiceContract
{
    /// <summary>
    /// Translator class wich implents Itranslator.
    /// </summary>
    public class Translator : Itranslator
    {
        /// <summary>
        /// cognitive api call which tacks inputtext and laguage code 
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="lngcod"></param>
        /// <returns>Json String</returns>
        [OnExceptions]
        public async Task<string> Trans(string inputText, string lngcod)
        {

            string translation_appKey = ConfigurationManager.AppSettings["TranslationKey"];
            string url_endpoint = ConfigurationManager.AppSettings["Translation_urlendpoint"];
            string route = ConfigurationManager.AppSettings["Translation_urlroute"];

            object[] body = new object[] { new { Text = inputText } };
            var requestBody = JsonConvert.SerializeObject(body);
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(url_endpoint + route + lngcod);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", translation_appKey);
                // location required if you're using a multi-service or regional (not global) resource.
                request.Headers.Add("Ocp-Apim-Subscription-Region", "canadacentral");

                // Send the request and get response.
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                // Read response as a string.
                string resultbody = await response.Content.ReadAsStringAsync();
                string result = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(resultbody), Formatting.Indented);
                return result;
            }
        }
    }
}
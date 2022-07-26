using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Onlinekhan.SSO.Common
{
    public static class CoreApi
    {
        private static string coreUrl = "http://159.69.82.251:63839/api";
        public static string GetFromCore(string url, string parameter)
        {
            HttpClient http = new HttpClient();
            //http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("", "");
            return http.GetAsync(coreUrl+url+parameter).Result.Content.ReadAsStringAsync().Result;
        }
        public static string PostToCore(string url, string parameter)
        {
            HttpClient http = new HttpClient();
            StringContent httpContent = new StringContent(parameter, System.Text.Encoding.UTF8, "application/json");
            return http.PostAsync(coreUrl+url,httpContent).Result.Content.ReadAsStringAsync().Result;
        }
    }
}

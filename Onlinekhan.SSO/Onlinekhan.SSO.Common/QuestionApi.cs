using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Onlinekhan.SSO.Common
{
    public static class QuestionApi
    {
        private static string questionUrl = "http://localhost:63839/api";
        public static string GetFromQuestion(string url, string parameter)
        {
            HttpClient http = new HttpClient();
            //http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Your Oauth token");
            return http.GetAsync(questionUrl+url+parameter).Result.Content.ReadAsStringAsync().Result;
        }
        public static string PostToQuestion(string url, string parameter)
        {
            HttpClient http = new HttpClient();
            StringContent httpContent = new StringContent(parameter, System.Text.Encoding.UTF8, "application/json");
            return http.PostAsync(questionUrl+url,httpContent).Result.Content.ReadAsStringAsync().Result;
        }
    }
}

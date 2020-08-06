using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace webDataRead
{
    class DataScraper
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> getParagraphAsync()
        {
            string subject1 = "";
            string subject2 = "";

            Dictionary<string, string> parameters = new Dictionary<string, string>()
        {
            { "Subject1", subject1 },
            { "Subject2", subject2 }
        };
            try
            {
                string result = await PostHTTPRequestAsync("http://watchout4snakes.com/wo4snakes/Random/RandomParagraph", parameters);
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static async Task<string> PostHTTPRequestAsync(string url, Dictionary<string, string> data)
        {
            using (HttpContent formContent = new FormUrlEncodedContent(data))
            using (HttpResponseMessage response = await client.PostAsync(url, formContent).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }
    }
}


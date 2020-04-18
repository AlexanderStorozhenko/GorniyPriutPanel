using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.NetworkInformation;

namespace GorniyPriutPanel
{
    class Request
    {
        const string URL = "http://gorniypriut/";

        private readonly HttpClient client;

        private static Request instance;

        private Request()
        {
            client = new HttpClient();
        }

        public static Request GetInstance()
        {
            if (instance is null) instance = new Request();
            return instance;
        }
        public async Task<string> Post(string url, Dictionary<string, string> parameters)
        {
            var content = new FormUrlEncodedContent(parameters);

            var response = await client.PostAsync(URL + url, content);

            var responseString = response.Content.ReadAsStringAsync().Result.Trim();

            return responseString;
        }
        public async Task<string> Get(string url, Dictionary<string, string> parameters)
        {
            string paramString = "?" + string.Join("&", parameters.Select(x => $"{x.Key}={x.Value}"));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL + url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }

        }

        public bool HasConnection()
        { 
            bool result = false;
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(URL, 3000);
                if (reply.Status == IPStatus.Success)
                    return true;
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            return result;
        }
    }
}

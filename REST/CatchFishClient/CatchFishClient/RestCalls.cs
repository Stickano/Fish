using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CatchFishClient
{
    class RestCalls
    {
        const string url = "http://localhost:50597/Service1.svc/";

        public string ReadAll()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage result = client.GetAsync(url + "read").Result;
                HttpContent content = result.Content;
                return content.ReadAsStringAsync().Result;
            }
        }

        public string Read(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage result = client.GetAsync(url + "read/" + id).Result;
                HttpContent content = result.Content;
                return content.ReadAsStringAsync().Result;
            }
        }
    }
}

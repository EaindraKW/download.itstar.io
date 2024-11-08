using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace download.itstar.io.Extension
{
     public static class HttpClientExtensions
    {
        public static async Task<long?> GetFileSizeAsync(this HttpClient client, string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Head, url))
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.Headers.ContentLength;
                }
            }

            return null;
        }
 }
}

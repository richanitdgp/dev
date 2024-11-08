using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncProgramming
{
    public static class SampleAsyncAwait
    {
        public static async Task Main()
        {
            Console.WriteLine("Downloading content...");

            string content = await DownloadContentAsync();

            Console.WriteLine("Download completed");
            Console.WriteLine($"Content length: {content.Length}");
        }

        public static async Task<string> DownloadContentAsync()
        {
            using(HttpClient client = new HttpClient())
            {
                string result = await client.GetStringAsync("https://microsoft.com");
                return result;
            }
        }
    }
}

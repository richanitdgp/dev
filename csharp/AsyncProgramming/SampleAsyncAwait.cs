using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncProgramming
{
    public static class SampleAsyncAwait
    {
        // Keyword async is used for async programming
        // If a method contains await expressions, it should be marked as async
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
                // The caller thread pauses execution and the await expression
                // gets executed within another thread from the thread pool.
                // The caller thread resumes when result is obtained from await expression
                // await is non-blocking for caller thread - good performance
                string result = await client.GetStringAsync("https://microsoft.com");
                return result;
            }
        }
    }
}

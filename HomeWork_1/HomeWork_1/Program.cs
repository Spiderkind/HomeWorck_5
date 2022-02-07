using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace HomeWork_1
{
    internal class Program
    {
        static readonly HttpClient client = new HttpClient();
        public static async Task Main(string[] args)
        {
            string file = "result.txt";
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            var tasks = new List<Task<string>>();
            for (int i = 4; i <= 13; i++)
            {
                var task = GetPost(i);
                tasks.AddRange(new[] { task });
            }
            await Task.WhenAll(tasks);
            foreach (var task in tasks)
            {
                string text = ProcessText(task.Result);
                File.AppendAllText(file, text);
                File.AppendAllText(file, Environment.NewLine);
            }
        }
        static async Task<string> GetPost(int i)
        {
            HttpResponseMessage response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts/" + i);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        static string ProcessText(string block)
        {
            string[] texts = block.Split('\n');
            string text = "";
                    texts[1] = texts[1].Remove(0, 12);
                    texts[1] = texts[1].TrimEnd(',');
                    text = texts[1] + "\n";
                    texts[2] = texts[2].Remove(0, 8);
                    texts[2] = texts[2].TrimEnd(',');
                    text = text + texts[2] + "\n";
                    texts[3] = texts[3].Remove(0, 12);
                    texts[3] = texts[3].TrimEnd(',','"');
                    text = text + texts[3] + "\n";
                    texts[4] = texts[4].Remove(0, 11);
                    texts[4] = texts[4].TrimEnd('"');
                    text = text + texts[4] + "\n";
            return text;
        }
    }
}
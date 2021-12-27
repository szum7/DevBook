/**
 * IAmTimCorey
 * C# Async / Await - Make your app more responsive and faster with asynchronous programming
 * https://www.youtube.com/watch?v=2moh18sh5p4
 * 
 * IAmTimCorey
 * C# Advanced Async - Getting progress reports, cancelling tasks, and more
 * https://www.youtube.com/watch?v=ZTKGRJy5P2M
 * 
 */

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DevBookPractice1.Chapters.Chapter14.TimCorey
{
    class AsyncAwaitTimCorey
    {
        private System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();

        public AsyncAwaitTimCorey()
        {
            this.RunProgram(() =>
            {
                this.RunDownloadAsync().Wait();
            }, "RunDownloadAsync");

            this.RunProgram(() =>
            {
                this.RunDownloadParallelAsync().Wait();
            }, "RunDownloadParallelAsync");

            this.RunProgram(() =>
            {
                this.RunDownloadParallelAsyncWithReport().Wait();
            }, "RunDownloadParallelAsyncWithReport");

            Console.WriteLine("End of AsyncAwaitTimCorey.");
        }

        private void RunProgram(Action action, string name)
        {
            watch.Restart();
            Console.WriteLine($"{name} begins.");
            action();
            watch.Stop();
            Console.WriteLine($"Elapsed ms: {watch.ElapsedMilliseconds} ms -> {name} ends.\n\n");
        }

        private List<string> GetTestUrls()
        {
            var list = new List<string>();

            list.Add("https://www.yahoo.com");
            list.Add("https://www.google.com");
            list.Add("https://www.microsoft.com");
            list.Add("https://www.youtube.com");
            list.Add("https://www.wikipedia.org");
            list.Add("https://www.hazipatika.com");

            return list;
        }

        private async Task RunDownloadAsync()
        {
            var sites = this.GetTestUrls();

            foreach (var site in sites)
            {
                var result = await Task.Run(() => this.DowloadWebsite(site));
                this.ReportWebsiteInfo(result);
            }
        }

        private async Task RunDownloadParallelAsync()
        {
            var sites = this.GetTestUrls();
            var tasks = new List<Task<WebsiteDataModel>>();

            foreach (var site in sites)
            {
                tasks.Add(Task.Run(() => this.DowloadWebsite(site)));
            }

            var results = await Task.WhenAll(tasks);

            foreach (var item in results)
            {
                this.ReportWebsiteInfo(item);
            }
        }

        private async Task RunDownloadParallelAsyncWithReport()
        {
            var sites = this.GetTestUrls();
            var tasks = new List<Task>();

            foreach (var site in sites)
            {
                // v1
                var work = Task
                    .Run(() => this.DowloadWebsite(site))
                    .ContinueWith(task => this.ReportWebsiteInfo(task.Result));
                tasks.Add(work);

                // v2
                //var t1 = new Task<WebsiteDataModel>(() => this.DowloadWebsite(site));
                //t1.Start();
                //var t2 = t1.ContinueWith(task => this.ReportWebsiteInfo(task.Result));
                //tasks.Add(t2);

                // "work" and "t2" returns Task, because we're using ContinueWith not ContinueWith<...someObjectType...> !

                // This method is fine if the Download and Report are separate methods and there's no way to link them
                // togeather other than with ContinueWith. Plus we need to see them printed line by line.
            }

            await Task.WhenAll(tasks);
        }

        private WebsiteDataModel DowloadWebsite(string url)
        {
            var o = new WebsiteDataModel();
            var c = new WebClient();

            o.Url = url;
            o.Data = c.DownloadString(url);

            return o;
        }

        private void ReportWebsiteInfo(WebsiteDataModel model)
        {
            Console.WriteLine($"Report: {model.Url}, page characters: {model.Data.Length}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevBookPractice1.Chapters.Chapter14.Asynchronity
{
    class AsynchronityMain
    {
        public AsynchronityMain()
        {
            VoidMethodReturningTask(); // #1 (can await it, will not block main program's end!)
            VoidMethodReturningTaskOldSolution(); // #2 (can't await it though)
            Console.WriteLine("End of AsynchronityMain.");
        }

        async Task VoidMethodReturningTask() // #1 Void async method
        {
            await Task.Delay(5000);
            int answer = 21 * 2;
            Console.WriteLine(answer);
        }

        Task VoidMethodReturningTaskOldSolution() // #2 Old implementation, functional equivalent to #1
        {
            var tcs = new TaskCompletionSource<object>();
            var awaiter = Task.Delay(5000).GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                try
                {
                    awaiter.GetResult();
                    int answer = 21 * 2;
                    Console.WriteLine(answer);
                    tcs.SetResult(null);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });
            return tcs.Task;
        }
    }
}

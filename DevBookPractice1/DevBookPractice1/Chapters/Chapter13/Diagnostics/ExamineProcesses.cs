using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace DevBookPractice1.Chapters.Chapter13.Diagnostics
{
    class ExamineProcesses
    {
        public ExamineProcesses()
        {
            //this.ExamineAllProcesses(); // #1 #2
            //this.StackTraceExceptionCaller(); // #3 StackTrace
            //this.MonitorWindowsLog(); // #4
            //this.ListPerformanceCounters(); // #5
            //this.ReadPerformanceCounter(); // #6
            this.SetupMonitor();

        }

        void ReadPerformanceCounter() // #6
        {
            using PerformanceCounter pc = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            Console.WriteLine(pc.NextValue());

            string procName = Process.GetCurrentProcess().ProcessName;
            using PerformanceCounter pc2 = new PerformanceCounter("Process", "Private Bytes", procName);
            Console.WriteLine(pc2.NextValue());
        }

        void SetupMonitor()
        {
            EventWaitHandle stopper = new ManualResetEvent(false);

            new Thread(() => Monitor("Processor", "% Processor Time", "_Total", stopper)).Start();

            new Thread(() => Monitor("LogicalDisk", "% Idle Time", "C:", stopper)).Start();
        }

        static void Monitor(string category, string counter, string instance, EventWaitHandle stopper)
        {
            if (!PerformanceCounterCategory.Exists(category))
                throw new InvalidOperationException("Category does not exist!");
            if (!PerformanceCounterCategory.Exists(counter, category))
                throw new InvalidOperationException("Counter does not exist!");
            if (instance == null)
                instance = "";
            if (instance != null && !PerformanceCounterCategory.InstanceExists(instance, category))
                throw new InvalidOperationException("Instance does not exist!");

            float lastValue = 0f;
            using (var pc = new PerformanceCounter(category, counter, instance))
            {
                while(!stopper.WaitOne(200, false))
                {
                    float value = pc.NextValue();
                    if (value != lastValue)
                    {
                        Console.WriteLine(value);
                        lastValue = value;
                    }
                }
            }
        }

        bool IsOverTheLimit(ref int counter, int limit)
        {
            if (counter >= limit)
                return true;

            counter++;
            return false;
        }

        void ListPerformanceCounters() // #5 List performance counters
        {
            int maxLineCount = 1000, counter = 0;
            var cats = PerformanceCounterCategory.GetCategories();

            foreach (var cat in cats)
            {
                Console.WriteLine("Category: " + cat.CategoryName);
                if (IsOverTheLimit(ref counter, maxLineCount)) return;

                var instances = cat.GetInstanceNames();

                if (instances.Length == 0)
                {
                    foreach (var ctr in cat.GetCounters())
                    {
                        Console.WriteLine(" Counter: " + ctr.CategoryName);
                        if (IsOverTheLimit(ref counter, maxLineCount)) return;
                    }
                }
                else
                {
                    foreach (var instance in instances)
                    {
                        Console.WriteLine(" Instance: " + instance);
                        if (cat.InstanceExists(instance))
                        {
                            foreach (var ctr in cat.GetCounters(instance))
                            {
                                Console.WriteLine(" Counter: " + ctr.CounterName);
                                if (IsOverTheLimit(ref counter, maxLineCount)) return;
                            }
                        }
                    }
                }
            }
        }

        void MonitorWindowsLog() // #4 Monitor windows log (application must be running)
        {
            using(var log = new EventLog("DemoApp"))
            {
                log.EnableRaisingEvents = true;
                log.EntryWritten += DisplayEntry;
            }
        }

        void DisplayEntry(object sender, EntryWrittenEventArgs e) // #4
        {
            EventLogEntry entry = e.Entry;
            Console.WriteLine(entry.Message);
        }

        void StackTraceExceptionCaller() // #3
        {
            try
            {
                this.StackTraceException();
            }
            catch (Exception ex)
            {
                var stack = new StackTrace(ex);
                Console.WriteLine("Total frames: " + stack.FrameCount);
                Console.WriteLine("Current method: " + stack.GetFrame(0).GetMethod().Name);
                Console.WriteLine("Calling method: " + stack.GetFrame(1).GetMethod().Name);
                Console.WriteLine("Entry method: " + stack.GetFrame(stack.FrameCount - 1).GetMethod().Name);
                Console.WriteLine("Call stack:");
                foreach (StackFrame sf in stack.GetFrames())
                {
                    Console.WriteLine(
                        " File: " + sf.GetFileName() +
                        " Line: " + sf.GetFileLineNumber() +
                        " Col: " + sf.GetFileColumnNumber() +
                        " Offset: " + sf.GetILOffset() +
                        " Method: " + sf.GetMethod().Name);
                }
                Console.WriteLine("StackTrace ToString:");
                Console.WriteLine(stack.ToString());
            }
        }

        void StackTraceException() // #3
        {
            var zero = 0;
            var a = 10 / zero;
            Console.WriteLine(a);
        }

        void ExamineAllProcesses() // #1 Get processes
        {
            foreach (Process p in Process.GetProcesses())
            {
                using (p)
                {
                    Console.WriteLine(p.ProcessName);
                    Console.Write($"    PID: {p.Id}");
                    Console.Write($"    Memory: {p.WorkingSet64}");
                    Console.Write($"    Threads: {p.Threads.Count}\n");

                    this.ExamineThread(p);
                }
            }
        }

        void ExamineThread(Process p) // #2 Get threads of process
        {
            try
            {
                foreach (ProcessThread pt in p.Threads)
                {
                    Console.Write($" ThreadId: {pt.Id}");
                    Console.Write($"    State: {pt.ThreadState}");
                    Console.Write($"    Priority: {pt.PriorityLevel}");
                    Console.Write($"    Started: {pt.StartTime}");
                    Console.Write($"    CPU time: {pt.TotalProcessorTime}");
                    Console.WriteLine("");
                }
            }
            catch
            {
                Console.WriteLine("");
            }
        }
    }
}

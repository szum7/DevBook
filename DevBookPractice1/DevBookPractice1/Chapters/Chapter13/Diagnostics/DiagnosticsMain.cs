#define TESTMODE // #1 - Using preprocessor directives
#define USENAMESPACE1 // #2 - Using different namespaces based on preprocessor directives

using System;
using System.Diagnostics; // #3
using BookNamspace = // #2
#if USENAMESPACE1
    Namespace1;
#else 
    Namespace2;
#endif

namespace DevBookPractice1.Chapters.Chapter13.Diagnostics
{
    class DiagnosticsMain
    {
        public DiagnosticsMain()
        {
#if TESTMODE && !PLAYMODE // #1
            Console.WriteLine("TESTMODE is defined. PLAYMODE is not defined.");
#endif
            var book = new BookNamspace.Book(); // #2
            this.LogStatus(); // #3
            this.TryDebugMethods(); // #4
            this.TryTraceListeners(); // #6
            this.TryTraceMethods(); // #5
        }

        [Conditional("LOGGINGMODE")] // #3 - Using conditional attributes
        public void LogStatus()
        {
            Console.WriteLine("Logstatus was called.");
        }

        void TryDebugMethods() // #4 Debug methods for debug builds
        {
            Debug.Write("Debug.Write");
            Debug.WriteLine("Debug.WriteLine");
            Debug.WriteIf(1 < 2, "Debug.WriteIf");

            var shouldAssertSucceed = true;
            Debug.Assert(shouldAssertSucceed, "Debug.Assert failed.");

            if (!shouldAssertSucceed)
                Debug.Fail("Debug failed after condition check.");
        }

        void TryTraceMethods() // #5 Trace methods for debug and release builds
        {
            Trace.Write("Trace.Write");
            Trace.WriteLine("Trace.WriteLine");
            Trace.WriteIf(1 < 2, "Trace.WriteIf");

            var shouldAssertSucceed = true;
            Trace.Assert(shouldAssertSucceed, "Trace.Assert failed.");

            Trace.TraceWarning("Orange alert");
        }

        void TryTraceListeners() // #6 Try Trace listeners
        {
            Trace.AutoFlush = true; // #7 Good practice to set it to true if writing to file

            // Clear the default listeners
            Trace.Listeners.Clear();
            // NOTE TO SELF: if I clear the listeners first, the debugger (Visual Studio)
            // will no longer stop the execution at Trace fails.

            // Add a writer that appends to the trace.txt file:
            Trace.Listeners.Add(new TextWriterTraceListener("trace.txt"));

            // Obtain the Console's output stream, then add that as a listener:
            System.IO.TextWriter tw = Console.Out;
            var twt = new TextWriterTraceListener(tw);
            twt.TraceOutputOptions = TraceOptions.DateTime | TraceOptions.Callstack;
            Trace.Listeners.Add(twt);

            // Set up the Windows Event log source and then create/add listener.
            // CreateEventSource requires administrative elevation, so this would
            // typically be done in application setup
            if (!EventLog.SourceExists("DemoApp"))
                EventLog.CreateEventSource("DemoApp", "Application");
            Trace.Listeners.Add(new EventLogTraceListener("DemoApp"));
        }
    }
}





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

namespace DevBookPractice1.Chapters.Diagnostics
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
        }

        [Conditional("LOGGINGMODE")] // #3 - Using conditional attributes
        public void LogStatus()
        {
            Console.WriteLine("Logstatus was called.");
        }
    }
}






namespace Example
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using TimeTaken;

    class Program
    {
        static void Main(string[] args)
        {
            var iterations = 100000;
            var testResults = (new Dictionary<string, Action>
                                { { "testString == \"\"", () => { var testString = ""; var isEmpty = testString == ""; } }
                                , { "testString == string.Empty", () => { var testString = ""; var isEmpty = testString == string.Empty; } }
                                , { "string.Equals(testString, \"\"", () => { var testString = ""; var isEmpty = string.Equals(testString, ""); } }
                                , { "string.Equals(testString, string.Empty)", () => { var testString = ""; var isEmpty = string.Equals(testString, string.Empty); } }
                                , { "string.IsNullOrEmpty(testString)", () => { var testString = ""; var isEmpty = string.IsNullOrEmpty(testString); } }
                                , { "\"\".Equals(testString)", () => { var testString = ""; var isEmpty = "".Equals(testString); } }
                                , { "string.Empty.Equals(testString)", () => { var testString = ""; var isEmpty = string.Empty.Equals(testString); } }
                                , { "testString.Any()", () => { var testString = ""; var isEmpty = testString.Any(); } }
                                , { "testString.Count() == 0", () => { var testString = ""; var isEmpty = testString.Count() == 0; } }
                                , { "testString.Length == 0", () => { var testString = ""; var isEmpty = testString.Length == 0; } }
                                }).ExecutionTimeGet(iterations);
            var myWriter = new TextWriterTraceListener(Console.Out);
            Trace.Listeners.Add(myWriter);
            testResults.ExecutionTimeDisplayElapsedTicks(iterations);
            Trace.Listeners.Remove(myWriter);
            Console.WriteLine("Press 'Esc' to exit.");
            while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}

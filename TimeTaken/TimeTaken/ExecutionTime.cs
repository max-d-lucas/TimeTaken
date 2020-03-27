

namespace TimeTaken
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;

    public static class ExecutionTime
    {

        /// <summary>
        /// Quick display of test results
        /// </summary>
        /// <param name="testResults"></param>
        /// <param name="iterations"></param>
        public static void ExecutionTimeDisplayElapsedTicks(this IReadOnlyDictionary<string, Stopwatch> testResults, long iterations)
        {
            Trace.WriteLine(string.Format("{0,20} | {1}", "Execution Time", "Test"));
            Trace.WriteLine(string.Empty.PadRight(50, '-'));
            foreach (var key in testResults.Keys)
            {
                Trace.WriteLine(string.Format("{0,14:N0} ticks | {1}", testResults[key].ElapsedTicks, key));
            }
            Trace.WriteLine(string.Empty.PadRight(50, '-'));
            Trace.WriteLine(string.Format("Test iterations: {0:N0}", iterations));
            Trace.WriteLine(string.Empty);
        }
        /// <summary>
        /// Quick display of test results
        /// </summary>
        /// <param name="testResults"></param>
        /// <param name="iterations"></param>
        public static void ExecutionTimeDisplayElapsedMilliseconds(this IReadOnlyDictionary<string, Stopwatch> testResults, long iterations)
        {
            Trace.WriteLine(string.Format("{0,20} | {1}", "Execution Time", "Test"));
            Trace.WriteLine(string.Empty.PadRight(50, '-'));
            foreach (var key in testResults.Keys)
            {
                Trace.WriteLine(string.Format("{0,14:N0} ms | {1}", testResults[key].ElapsedMilliseconds, key));
            }
            Trace.WriteLine(string.Empty.PadRight(50, '-'));
            Trace.WriteLine(string.Format("Test iterations: {0:N0}", iterations));
            Trace.WriteLine(string.Empty);
        }

        /// <summary>
        /// Run a set of named actions a set number of times and return a list Stopwatches indication how long each action took to run
        /// </summary>
        /// <param name="namedTests">named actions to execute</param>
        /// <param name="iterations">how many times to execute each action</param>
        /// <returns>how long each  action took to run</returns>
        public static IReadOnlyDictionary<string, Stopwatch> ExecutionTimeGet(this IDictionary<string,Action> namedTests, long iterations)
        {
            if (namedTests == null)
            {
                throw new ArgumentNullException(nameof(namedTests));
            }
            if (iterations < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(iterations), "must be a positive integer");
            }
            var result = new ReadOnlyDictionary<string, Stopwatch>(namedTests.ToDictionary(k => k.Key, v => ExecutionTimeGet(v.Value, iterations)));
            return result;
        }

        /// <summary>
        /// Run a set of actions a set number of times and return a list Stopwatchs indication how long each action took to run
        /// </summary>
        /// <param name="tests">actions to execute</param>
        /// <param name="iterations">how many times to execute each action</param>
        /// <returns>how long each  action took to run</returns>
        public static IEnumerable<Stopwatch> ExecutionTimeGet(this IEnumerable<Action> tests, long iterations)
        {
            if (tests == null)
            {
                throw new ArgumentNullException(nameof(tests));
            }
            if (iterations < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(iterations), "must be a positive integer");
            }
            var result = tests.Select(s => ExecutionTimeGet(s, iterations));
            return result;
        }

        /// <summary>
        /// Run an action a set number of times and return a Stopwatch indication how long it took to run
        /// </summary>
        /// <param name="test">action to execute</param>
        /// <param name="iterations">how many times to execute</param>
        /// <returns>how long it took to run</returns>
        public static Stopwatch ExecutionTimeGet(this Action test, long iterations)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }
            if (iterations < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(iterations), "must be a positive integer");
            }
            var result = new Stopwatch();
            result.Start();
            while (iterations-- > 0)
            {
                test();
            }
            result.Stop();
            return result;
        }
    }
}

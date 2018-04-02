﻿
namespace TimeTaken
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public static class ExecutionTimer
    {

        /// <summary>
        /// Quick display of test results
        /// </summary>
        /// <param name="testResults"></param>
        /// <param name="iterations"></param>
        /// <param name="output"></param>
        public static void ExecutionTimeDisplayElapsedTicks(this IDictionary<string, Stopwatch> testResults, long iterations)
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
        public static void ExecutionTimeDisplayElapsedMilliseconds(this IDictionary<string, Stopwatch> testResults, long iterations)
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
        /// Run a set of named actions a set number of times and return a list Stopwatchs indication how long each action took to run
        /// </summary>
        /// <param name="tests">named actions to execute</param>
        /// <param name="iterations">how many times to execute each action</param>
        /// <returns>how long each  action took to run</returns>
        public static IDictionary<string, Stopwatch> ExecutionTimeGet(this IDictionary<string,Action> namedTests, long iterations)
        {
            if (namedTests == null)
            {
                throw new ArgumentNullException("namedTests");
            }
            if (iterations < 1)
            {
                throw new ArgumentOutOfRangeException("iterations", "must be a positive integer");
            }
            var result = new Dictionary<string, Stopwatch>(namedTests.Select(s => new KeyValuePair<string, Stopwatch>(s.Key, ExecutionTimeGet(s.Value, iterations))));
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
                throw new ArgumentNullException("tests");
            }
            if (iterations < 1)
            {
                throw new ArgumentOutOfRangeException("iterations", "must be a positive integer");
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
                throw new ArgumentNullException("test");
            }
            if (iterations < 1)
            {
                throw new ArgumentOutOfRangeException("iterations", "must be a positive integer");
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

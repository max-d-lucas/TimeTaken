
namespace TimeTaken
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class TimeIt
    {

        /// <summary>
        /// Run a set of named actions a set number of times and return a list Stopwatchs indication how long each action took to run
        /// </summary>
        /// <param name="tests">named actions to execute</param>
        /// <param name="iterations">how many times to execute each action</param>
        /// <returns>how long each  action took to run</returns>
        public IDictionary<string, Stopwatch> Run(IDictionary<string,Action> namedTests, long iterations)
        {
            if (namedTests == null)
            {
                throw new ArgumentNullException("namedTests");
            }
            if (iterations < 1)
            {
                throw new ArgumentOutOfRangeException("iterations", "must be a positive integer");
            }
            var result = new Dictionary<string, Stopwatch>(namedTests.Select(s => new KeyValuePair<string, Stopwatch>(s.Key, Run(s.Value, iterations))));
            return result;
        }

        /// <summary>
        /// Run a set of actions a set number of times and return a list Stopwatchs indication how long each action took to run
        /// </summary>
        /// <param name="tests">actions to execute</param>
        /// <param name="iterations">how many times to execute each action</param>
        /// <returns>how long each  action took to run</returns>
        public IEnumerable<Stopwatch> Run(IEnumerable<Action> tests, long iterations)
        {
            if (tests == null)
            {
                throw new ArgumentNullException("tests");
            }
            if (iterations < 1)
            {
                throw new ArgumentOutOfRangeException("iterations", "must be a positive integer");
            }
            var result = tests.Select(s => Run(s, iterations));
            return result;
        }

        /// <summary>
        /// Run an action a set number of times and return a Stopwatch indication how long it took to run
        /// </summary>
        /// <param name="test">action to execute</param>
        /// <param name="iterations">how many times to execute</param>
        /// <returns>how long it took to run</returns>
        public Stopwatch Run(Action test, long iterations)
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

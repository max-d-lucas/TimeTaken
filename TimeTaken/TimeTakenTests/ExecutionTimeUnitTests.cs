namespace TimeTakenTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using TimeTaken;

    [TestClass]
    public class ExecutionTimeUnitTests
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RunNamedTestsZeroIterations()
        {
            (new Dictionary<string, Action> { { "test1", () => { var i = 0; i++; } } }).ExecutionTimeGet(0);
        }
        [TestMethod]
        public void RunNamedTestsTwoIterations()
        {
            var actual = (new Dictionary<string, Action> { { "test1", () => { Thread.Sleep(10); } }, { "test2", () => { Thread.Sleep(20); } } }).ExecutionTimeGet(2).ToList();
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Any());
            Assert.IsTrue(actual[0].Value.ElapsedMilliseconds >= 20);
            Assert.IsTrue(actual[0].Key == "test1");
            Assert.IsTrue(actual[1].Value.ElapsedMilliseconds >= 40);
            Assert.IsTrue(actual[1].Key == "test2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RunTestsZeroIterations()
        {
            (new Action[] { () => { var i = 0; i++; } }).ExecutionTimeGet(0);
        }

        [TestMethod]
        public void RunTestsTwoIterations()
        {
            var actual = (new Action[] { () => { Thread.Sleep(10); }, () => { Thread.Sleep(20); } }).ExecutionTimeGet(2).ToList();
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Any());
            Assert.IsTrue(actual[0].ElapsedMilliseconds >= 20);
            Assert.IsTrue(actual[1].ElapsedMilliseconds >= 40);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RunTestZeroIterations()
        {
            ((Action)(() => { var i = 0; i++; })).ExecutionTimeGet(0);
        }

        [TestMethod]
        public void RunTestTwoIterations()
        {
            var actual = ((Action)(() => { Thread.Sleep(10); })).ExecutionTimeGet(2);
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.ElapsedMilliseconds >= 20);
        }
    }
}


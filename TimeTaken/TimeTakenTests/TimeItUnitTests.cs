namespace TimeTakenTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using TimeTaken;

    [TestClass]
    public class TimeItUnitTests
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RunNamedTestsNullAction()
        {
            var assert = new TimeIt();
            assert.Run((Dictionary<string, Action>)null, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RunNamedTestsZeroIterations()
        {
            var assert = new TimeIt();
            assert.Run(new Dictionary<string, Action> { { "test1", () => { var i = 0; } } } , 0);
        }
        [TestMethod]
        public void RunNamedTestsTwoIterations()
        {
            var assert = new TimeIt();
            var actual = assert.Run(new Dictionary<string, Action> { { "test1", () => { Thread.Sleep(10); } }, { "test2", () => { Thread.Sleep(20); } } }, 2).ToList();
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Any());
            Assert.IsTrue(actual[0].Value.ElapsedMilliseconds >= 20);
            Assert.IsTrue(actual[0].Key == "test1");
            Assert.IsTrue(actual[1].Value.ElapsedMilliseconds >= 40);
            Assert.IsTrue(actual[1].Key == "test2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RunTestsNullAction()
        {
            var assert = new TimeIt();
            assert.Run((Action[])null, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RunTestsZeroIterations()
        {
            var assert = new TimeIt();
            assert.Run(new Action[] { () => { var i = 0; } }, 0);
        }

        [TestMethod]
        public void RunTestsTwoIterations()
        {
            var assert = new TimeIt();
            var actual = assert.Run(new Action[] { () => { Thread.Sleep(10); }, () => { Thread.Sleep(20); } }, 2).ToList();
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Any());
            Assert.IsTrue(actual[0].ElapsedMilliseconds >= 20);
            Assert.IsTrue(actual[1].ElapsedMilliseconds >= 40);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RunTestNullAction()
        {
            var assert = new TimeIt();
            assert.Run((Action)null, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RunTestZeroIterations()
        {
            var assert = new TimeIt();
            assert.Run(() => { var i = 0; }, 0);
        }

        [TestMethod]
        public void RunTestTwoIterations()
        {
            var assert = new TimeIt();
            var actual = assert.Run(() => { Thread.Sleep(10); }, 2);
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.ElapsedMilliseconds >= 20);
        }
    }
}


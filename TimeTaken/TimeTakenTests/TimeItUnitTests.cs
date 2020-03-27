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
            var assert =  new Dictionary<string, Action> { { "test1", null } };
            var actual = assert.ExecutionTimeGet(2);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RunNamedTestsZeroIterations()
        {
            var assert =new Dictionary<string, Action> { { "test1", () => { var i = 0; } } };
            var actual = assert.ExecutionTimeGet(0);
        }
        [TestMethod]
        public void RunNamedTestsTwoIterations()
        {
            var assert = new Dictionary<string, Action> { { "test1", () => { Thread.Sleep(10); } }, { "test2", () => { Thread.Sleep(20); } } };
            var actual = assert.ExecutionTimeGet(2);
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count == 2);
            var actualArray = actual.ToArray();
            for(var i = 0 ; i <2; i++)
            {
                Assert.IsTrue(actualArray[i].Value.ElapsedMilliseconds >= 10 * (i+1));
                Assert.AreEqual($"test{i+1}",actualArray[i].Key);
            }
        }
    }
}


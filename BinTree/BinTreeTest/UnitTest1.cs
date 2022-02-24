using Microsoft.VisualStudio.TestTools.UnitTesting;
using BinTree;
namespace BinTreeTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAdd()
        {
            BinTree<int,int> test = new BinTree<int, int>();
            test.Add(23);
            test.Add(32);
            test.Add(11);
            Assert.AreEqual(23, test.Key);
            Assert.AreEqual(32, test.More.value);
            Assert.AreEqual(11, test.Less.value);
        }
        [TestMethod]
        public void TestRemove()
        {
            var test = new BinTree<int, int>();

            test.Add(23);
            test.Add(32);
            test.Add(11);
            test.Remove(23);

            Assert.AreEqual(32, test.value);
            Assert.AreEqual(11, test.less.value);
        }
        [TestMethod]
        public void TestFind()
        {
            var test = new BinTree<int>();
            test.Add(23);
            test.Add(32);
            test.Add(11);
            test.Add(34);
            test.Add(35);
            test.Add(33);
            var found = test.Find(34);
            Assert.AreEqual(34, found.value);
            Assert.AreEqual(35, found.more.value);
            Assert.AreEqual(33, found.less.value);
        }
        [TestMethod]
        public void TestContains()
        {
            var test = new BinTree<int>();

            test.Add(23);
            test.Add(32);
            test.Add(11);
            test.Add(34);
            test.Add(35);
            test.Add(33);

            Assert.IsTrue(test.Contains(33));
            Assert.IsFalse(test.Contains(12));
            Assert.IsTrue(test.Contains(11));
        }
    }
}

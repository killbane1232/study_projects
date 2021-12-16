using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thinker;
namespace TestBuilder
{
    [TestClass]
    public class TestTryBuilder
    {
        [TestMethod]
        public void TestTryBuilderMethod1()
        {
            var builder = new OPZBuilder();
            Assert.IsFalse(builder.TryBuild("((à+à+à))"));
        }
        [TestMethod]
        public void TestTryBuilderMethod2()
        {
            var builder = new OPZBuilder();
            Assert.IsFalse(builder.TryBuild("~(x+(x)+x)"));
        }
        [TestMethod]
        public void TestTryBuilderMethod3()
        {
            var builder = new OPZBuilder();
            Assert.IsFalse(builder.TryBuild("((((à+à)+à))+a)"));
        }
        [TestMethod]
        public void TestTryBuilderMethod4()
        {
            var builder = new OPZBuilder();
            Assert.IsFalse(builder.TryBuild("(((à+à+à)))"));
        }
        [TestMethod]
        public void TestTryBuilderMethod5()
        {
            var builder = new OPZBuilder();
            Assert.IsTrue(builder.TryBuild("~(x>(y+(x*z)))"));
        }
        [TestMethod]
        public void TestTryBuilderMethod6()
        {
            var builder = new OPZBuilder();
            Assert.IsTrue(builder.TryBuild("x"));
        }
        [TestMethod]
        public void TestTryBuilderMethod7()
        {
            var builder = new OPZBuilder();
            Assert.IsFalse(builder.TryBuild("(a+(a+à)+à)"));
        }
        [TestMethod]
        public void TestTryBuilderMethod8()
        {
            var builder = new OPZBuilder();
            Assert.IsFalse(builder.TryBuild("(a+a+a+a)"));
        }
        [TestMethod]
        public void TestTryBuilderMethod9()
        {
            var builder = new OPZBuilder();
            Assert.IsTrue(builder.TryBuild("(a+(a+(a+a)))"));
        }
        [TestMethod]
        public void TestTryBuilderMethod10()
        {
            var builder = new OPZBuilder();
            Assert.IsTrue(builder.TryBuild("(((a+a)+a)+a)"));
        }
        [TestMethod]
        public void TestTryBuilderMethod11()
        {
            var builder = new OPZBuilder();
            Assert.IsTrue(builder.TryBuild("((a+a)+(a+a))"));
        }
        [TestMethod]
        public void TestTryBuilderMethod12()
        {
            var builder = new OPZBuilder();
            Assert.IsTrue(builder.TryBuild("(a+((a+a)+a))"));
        }
        [TestMethod]
        public void TestTryBuilderMethod13()
        {
            var builder = new OPZBuilder();
            Assert.IsTrue(builder.TryBuild("~(~x+~x)"));
        }
        [TestMethod]
        public void TestTryBuilderMethod14()
        {
            var builder = new OPZBuilder();
            Assert.IsFalse(builder.TryBuild("(((à+à)+à))+à"));
        }
        [TestMethod]
        public void TestTryBuilderMethod15()
        {
            var builder = new OPZBuilder();
            Assert.IsFalse(builder.TryBuild("à+(((à+à)+à))"));
        }
        [TestMethod]
        public void TestTryBuilderMethod16()
        {
            var builder = new OPZBuilder();
            Assert.IsFalse(builder.TryBuild("à+b"));
        }
    }
}

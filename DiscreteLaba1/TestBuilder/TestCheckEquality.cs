using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thinker;
namespace TestBuilder
{
    [TestClass]
    public class TestCheckEquality
    {
        [TestMethod]
        public void  TestTrutchTableCheckMethod1()
        {
            var builder1 = new OPZBuilder();
            var builder2 = new OPZBuilder();
            Assert.IsTrue(builder1.TryBuild("(a>b)"));
            Assert.IsTrue(builder2.TryBuild("(~a+b)"));
            Assert.IsTrue(OPZBuilder.TruthTableCheck(builder1, builder2));
        }
        [TestMethod]
        public void  TestTrutchTableCheckMethod2()
        {
            var builder1 = new OPZBuilder();
            var builder2 = new OPZBuilder();
            Assert.IsTrue(builder1.TryBuild("((a+b)*c)"));
            Assert.IsTrue(builder2.TryBuild("((a*c)+(b*c))"));
            Assert.IsTrue(OPZBuilder.TruthTableCheck(builder1, builder2));
        }
        [TestMethod]
        public void  TestTrutchTableCheckMethod3()
        {
            var builder1 = new OPZBuilder();
            var builder2 = new OPZBuilder();
            Assert.IsTrue(builder1.TryBuild("~(x>y)"));
            Assert.IsTrue(builder2.TryBuild("(x*~y)"));
            Assert.IsTrue(OPZBuilder.TruthTableCheck(builder1, builder2));
        }
        [TestMethod]
        public void  TestTrutchTableCheckMethod4()
        {
            var builder1 = new OPZBuilder();
            var builder2 = new OPZBuilder();
            Assert.IsTrue(builder1.TryBuild("(x>~y)"));
            Assert.IsTrue(builder2.TryBuild("(y>~x)"));
            Assert.IsTrue(OPZBuilder.TruthTableCheck(builder1, builder2));
        }
        [TestMethod]
        public void  TestTrutchTableCheckMethod5()
        {
            var builder1 = new OPZBuilder();
            var builder2 = new OPZBuilder();
            Assert.IsTrue(builder1.TryBuild("(x+(~x*y))"));
            Assert.IsTrue(builder2.TryBuild("(x+y)"));
            Assert.IsTrue(OPZBuilder.TruthTableCheck(builder1, builder2));
        }
        [TestMethod]
        public void  TestTrutchTableCheckMethod6()
        {
            var builder1 = new OPZBuilder();
            var builder2 = new OPZBuilder();
            Assert.IsTrue(builder1.TryBuild("((x+y)*(x+~y))"));
            Assert.IsTrue(builder2.TryBuild("x"));
            Assert.IsTrue(OPZBuilder.TruthTableCheck(builder1, builder2));
        }
        [TestMethod]
        public void  TestTrutchTableCheckMethod7()
        {
            var builder1 = new OPZBuilder();
            var builder2 = new OPZBuilder();
            Assert.IsTrue(builder1.TryBuild("(x>(y>z))"));
            Assert.IsTrue(builder2.TryBuild("((x*y)>z)"));
            Assert.IsTrue(OPZBuilder.TruthTableCheck(builder1, builder2));
        }
        [TestMethod]
        public void  TestTrutchTableCheckMethod8()
        {
            var builder1 = new OPZBuilder();
            var builder2 = new OPZBuilder();
            Assert.IsTrue(builder1.TryBuild("((x*(z>y))+((x>z)*y))"));
            Assert.IsTrue(builder2.TryBuild("((x+y)*(y+~z))"));
            Assert.IsTrue(OPZBuilder.TruthTableCheck(builder1, builder2));
        }
        [TestMethod]
        public void  TestTrutchTableCheckMethod9()
        {
            var builder1 = new OPZBuilder();
            var builder2 = new OPZBuilder();
            Assert.IsTrue(builder1.TryBuild("((((x*y)*z)+((x*y)*~z))+(((x*~y)*z)+((x*~y)*~z)))"));
            Assert.IsTrue(builder2.TryBuild("x"));
            Assert.IsTrue(OPZBuilder.TruthTableCheck(builder1, builder2));
        }
        [TestMethod]
        public void  TestTrutchTableCheckMethod10()
        {
            var builder1 = new OPZBuilder();
            var builder2 = new OPZBuilder();
            Assert.IsTrue(builder1.TryBuild("(((x*~x)+y)+(z*~z))"));
            Assert.IsTrue(builder2.TryBuild("y"));
            Assert.IsTrue(OPZBuilder.TruthTableCheck(builder1, builder2));
        }
        [TestMethod]
        public void TestTrutchTableCheckMethod11()
        {
            var builder1 = new OPZBuilder();
            var builder2 = new OPZBuilder();
            Assert.IsTrue(builder1.TryBuild("(~x+x)"));
            Assert.IsTrue(builder2.TryBuild("(~y+y)"));
            Assert.IsTrue(OPZBuilder.TruthTableCheck(builder1, builder2));
        }
    }
}

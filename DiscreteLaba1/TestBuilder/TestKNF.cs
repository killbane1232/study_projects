using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thinker;
namespace TestBuilder
{
    [TestClass]
    public class TestKNF
    {
        [TestMethod]
        public void TestKNFMethod1()
        {
            var builder = new OPZBuilder();
            var builder1 = new OPZBuilder();
            builder.TryBuild("(a+b)");
            var str = builder.BuildKNF();
            builder1.TryBuild(str);
            Assert.IsTrue(OPZBuilder.TruthTableCheck(builder,builder1));
        }
        [TestMethod]
        public void TestKNFMethod2()
        {
            var builder = new OPZBuilder();
            var builder1 = new OPZBuilder();
            builder.TryBuild("(a^b)");
            var str = builder.BuildKNF();
            builder1.TryBuild(str);
            Assert.IsTrue(OPZBuilder.TruthTableCheck(builder, builder1));
        }
        [TestMethod]
        public void TestKNFMethod3()
        {
            var builder = new OPZBuilder();
            var builder1 = new OPZBuilder();
            builder.TryBuild("(a+~a)");
            var str = builder.BuildKNF();
            builder1.TryBuild(str);
            Assert.IsTrue(OPZBuilder.TruthTableCheck(builder, builder1));
        }
        [TestMethod]
        public void TestKNFMethod4()
        {
            var builder = new OPZBuilder();
            var builder1 = new OPZBuilder();
            builder.TryBuild("(a^~a)");
            var str = builder.BuildKNF();
            builder1.TryBuild(str);
            Assert.IsTrue(OPZBuilder.TruthTableCheck(builder, builder1));
        }
    }
}

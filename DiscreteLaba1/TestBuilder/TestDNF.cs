using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thinker;
namespace TestBuilder
{
    [TestClass]
    public class TestDNF
    {
        [TestMethod]
        public void TestDNFMethod1()
        {
            var builder = new OPZBuilder();
            var builder1 = new OPZBuilder();
            builder.TryBuild("(a+b)");
            var str = builder.BuildDNF();
            builder1.TryBuild(str);
            Assert.IsTrue(OPZBuilder.TruthTableCheck(builder,builder1));
        }
        [TestMethod]
        public void TestDNFMethod2()
        {
            var builder = new OPZBuilder();
            var builder1 = new OPZBuilder();
            builder.TryBuild("(a+~a)");
            var str = builder.BuildDNF();
            builder1.TryBuild(str);
            Assert.IsTrue(OPZBuilder.TruthTableCheck(builder, builder1));
        }
        [TestMethod]
        public void TestDNFMethod3()
        {
            var builder = new OPZBuilder();
            var builder1 = new OPZBuilder();
            builder.TryBuild("(a^~a)");
            var str = builder.BuildDNF();
            builder1.TryBuild(str);
            Assert.IsTrue(OPZBuilder.TruthTableCheck(builder, builder1));
        }
        [TestMethod]
        public void TestDNFMethod4()
        {
            var builder = new OPZBuilder();
            var builder1 = new OPZBuilder();
            builder.TryBuild("(av~a)");
            var str = builder.BuildDNF();
            builder1.TryBuild(str);
            Assert.IsTrue(OPZBuilder.TruthTableCheck(builder, builder1));
        }
    }
}

using System.Diagnostics;
using System.Text;

namespace GrassHopperTest
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("TestText", "TestKey")]
        [InlineData("TheLongestTextICanWriteWithoutHelp", "TheLongestPasswordICanWriteWithoutHelp")]
        public void Test(string text, string key)
        {
            var hopper = new GrassHopper.GrassHopper();
            hopper.SetKey(key);
            var encoded = hopper.Encode(Encoding.Default.GetBytes(text));
            Assert.NotEqual(text, Encoding.Default.GetString(encoded));
            var decoded = hopper.Decode(encoded);
            Assert.Equal(text, Encoding.Default.GetString(decoded));
        }
        [Fact]
        public void Test2()
        {
            var text = File.ReadAllBytes("E:\\shit.wav");
            var hopper = new GrassHopper.GrassHopper();
            hopper.SetKey("NewFuckingInfiniteKEY");
            var encoded = hopper.Encode(text);
            var decoded = hopper.Decode(encoded);
            var flag = true;
            for (var i = 0; i < decoded.Length && flag; i++)
            {
                if (decoded[i] != text[i])
                    flag = false;
            }
            Assert.True(flag);
        }
        [Theory]
        [InlineData("TestText", "TestKey")]
        [InlineData("TheLongestTextICanWriteWithoutHelp", "TheLongestPasswordICanWriteWithoutHelp")]
        public void TestCBC(string text, string key)
        {
            var hopper = new GrassHopper.GrassHopper();
            var vector = new byte[16];
            var rand = new Random(DateTime.Now.Second);

            for (var i =0; i < 16; i++)
            {
                vector[i] = Convert.ToByte(rand.Next(0,255));
            }
            hopper.SetKey(key);
            var encoded = hopper.EncodeCBC(Encoding.Default.GetBytes(text), vector);
            Assert.NotEqual(text, Encoding.Default.GetString(encoded));
            var decoded = hopper.DecodeCBC(encoded, vector);
            Assert.Equal(text, Encoding.Default.GetString(decoded));
        }
        [Theory]
        [InlineData("TestText", "TestKey", "tesKe2t")]
        public void TestCBCTryTwice(string text, string key, string key2)
        {
            var hopper = new GrassHopper.GrassHopper();
            var hopper2 = new GrassHopper.GrassHopper();
            var vector = new byte[16];
            var vector2 = new byte[16];
            var rand = new Random(DateTime.Now.Second);

            for (var i = 0; i < 16; i++)
            {
                vector[i] = Convert.ToByte(rand.Next(0, 255));
                vector2[i] = Convert.ToByte(rand.Next(0, 255));
            }
            hopper.SetKey(key);
            hopper2.SetKey(key2);
            var encoded = hopper.EncodeCBC(Encoding.Default.GetBytes(text), vector);
            var encoded2 = hopper2.EncodeCBC(encoded, vector2);
            Assert.NotEqual(text, Encoding.Default.GetString(encoded));
            var decoded = hopper.DecodeCBC(encoded2, vector);
            var decoded2 = hopper2.DecodeCBC(decoded, vector2);
            Assert.Equal(text, Encoding.Default.GetString(decoded2));
        }
        [Fact]
        public void TestCBC2()
        {
            var text = File.ReadAllBytes("E:\\shit.wav");
            var hopper = new GrassHopper.GrassHopper();
            hopper.SetKey("NewFuckingInfiniteKEY");
            var vector = new byte[16];
            var rand = new Random(DateTime.Now.Second);

            for (var i = 0; i < 16; i++)
            {
                vector[i] = Convert.ToByte(rand.Next(0, 255));
            }
            var encoded = hopper.EncodeCBC(text, vector);
            Assert.NotEqual(text, encoded);
            var decoded = hopper.DecodeCBC(encoded, vector);
            Assert.Equal(text, decoded);
            var flag = true;
            for (var i = 0; i < decoded.Length && flag; i++)
            {
                if (decoded[i] != text[i])
                    flag = false;
            }
            Assert.True(flag);
        }
    }
}
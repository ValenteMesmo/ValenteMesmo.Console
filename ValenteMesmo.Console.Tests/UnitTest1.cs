using AutoFixture.Xunit2;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ValenteMesmo.Tests
{
    public class UnitTest1
    {
        private StringWriter CreateWriter()
        {
            var sw = new StringWriter();
            Console.SetOut(sw);

            return sw;
        }

        [Theory, AutoData]
        public void Write(string expected)
        {
            using (var sw = CreateWriter())
            {
                Console.Write(expected);

                Assert.Equal(expected, sw.ToString());
            }
        }

        [Theory, AutoData]
        public void WriteLine(string expected)
        {
            using (var sw = CreateWriter())
            {
                Console.WriteLine(expected);

                Assert.Equal(expected + Environment.NewLine, sw.ToString());
            }
        }

        [Fact]
        public void ProgressBar()
        {
            using (var sw = CreateWriter())
            {
                var sut = Console.ProgressBar(100);

                sut.Set(50);
                Task.Delay(10).Wait();

                var expected = @"


╔══════════════════╗ 25%║██████████████████║ Total:     00:00:00╚══════════════════╝ Remaining: 00:00:00";

                Assert.Equal(expected, sw.ToString());
            }
        }
    }
}

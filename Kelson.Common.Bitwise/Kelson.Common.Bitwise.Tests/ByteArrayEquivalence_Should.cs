using FluentAssertions;
using System;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace Kelson.Common.Bitwise.Tests
{
    public class ByteArrayEquivalence_Should
    {
        [Fact]
        public void CheckForEquality()
        {
            // equal arrays, length divisable by sizeof(long)
            new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07 }
                .IsEquivalentTo(new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07 })
                .Should()
                .BeTrue();

            // equal arrays, length not divisable by sizeof(long)
            new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A }
                .IsEquivalentTo(new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A })
                .Should()
                .BeTrue();

            // length mismatch
            new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 }
                .IsEquivalentTo(new byte[] { 0x00, 0x01 })
                .Should()
                .BeFalse();

            // empty equivalent arrays
            new byte[0]
                .IsEquivalentTo(new byte[0])
                .Should()
                .BeTrue();

            // index 4 differs
            new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07 }
                .IsEquivalentTo(new byte[] { 0x00, 0x01, 0x02, 0x03, 0x03, 0x05, 0x06, 0x07 })
                .Should()
                .BeFalse();
        }

        [Fact]
        public void CheckForEqualityOfLargeArrays()
        {
            Random rng = new Random();
            for (int i = 0; i < 5; i++)
            {
                var length = rng.Next(2000, 2500);
                var first = new byte[length];
                var second = new byte[length];
                rng.NextBytes(first);
                first.CopyTo(second, 0);
                first.IsEquivalentTo(second)
                    .Should()
                    .BeTrue();
            }
        }

        [Fact]
        public void CheckForInequalityOfLargeArrays()
        {
            Random rng = new Random();
            for (int i = 0; i < 5; i++)
            {
                var length = rng.Next(20000, 25000);
                var first = new byte[length];
                var second = new byte[length];
                rng.NextBytes(first);
                first.CopyTo(second, 0);
                second[length - 1] = (byte)~first[length - 1]; // change last byte
                first.IsEquivalentTo(second)
                    .Should()
                    .BeFalse();
            }
        }

        [Fact]
        public void BeFast()
        {
            bool SimpleIterativeCheck(byte[] a, byte[] b)
            {
                for (int i = 0; i < a.Length; i++)
                    if (a[i] != b[i])
                        return false;
                return true;
            }

            Random rng = new Random();
            for (int i = 0; i < 5; i++)
            {
                var length = rng.Next(20000, 25000);
                var first = new byte[length];
                var second = new byte[length];
                rng.NextBytes(first);
                first.CopyTo(second, 0);
                var clock1 = new Stopwatch();
                clock1.Start();
                var result1 = first.IsEquivalentTo(second);
                clock1.Stop();
                var clock2 = new Stopwatch();
                clock2.Start();
                var result2 = first.SequenceEqual(second);
                clock2.Stop();
                var clock3 = new Stopwatch();
                clock3.Start();
                var result3 = SimpleIterativeCheck(first, second);
                clock3.Stop();

                (result1 && result2 && result3).Should().BeTrue();

                (clock1.Elapsed.TotalMilliseconds < clock2.Elapsed.TotalMilliseconds).Should().BeTrue();
                (clock1.Elapsed.TotalMilliseconds < clock3.Elapsed.TotalMilliseconds).Should().BeTrue();
            }
        }
    }
}

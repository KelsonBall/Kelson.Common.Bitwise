using FluentAssertions;
using Xunit;

namespace Kelson.Common.Bitwise.Tests
{
    public class UShortExtensions_Should
    {
        [Fact]
        public void SetBits()
        {
            var one = ((ushort)0).Set(0);
            one.Should().Be(1);
            var three = one.Set(1);
            three.Should().Be(3);
        }

        [Fact]
        public void ClearBits()
        {
            var three = ((ushort)0).Set(0).Set(1);
            three.Should().Be(3);
            var two = three.Clear(0);
            two.Should().Be(2);
            var zero = two.Clear(1);
            zero.Should().Be(0);
        }

        [Fact]
        public void ToggleBits()
        {
            var three = ((ushort)0).Toggle(0).Toggle(1);
            three.Should().Be(3);
            var one = three.Toggle(1);
            one.Should().Be(1);
        }

        [Fact]
        public void CheckBitsSet()
        {
            ushort five = 5;
            five.IsSet(0).Should().BeTrue();
            five.IsSet(1).Should().BeFalse();
            five.IsSet(2).Should().BeTrue();
            five.IsSet(3).Should().BeFalse();
        }

        [Fact]
        public void SetMask()
        {
            var five = ((ushort)0).SetMask(5);
            five.Should().Be(5);
            var seven = five.SetMask(2);
            seven.Should().Be(7);
        }

        [Fact]
        public void ClearMask()
        {
            var two = ((ushort)0).SetMask(0b111).ClearMask(0b101);
            two.Should().Be(0b010);
        }

        [Fact]
        public void ToggleMask()
        {
            var thirteen = ((ushort)0).ToggleMask(0b0111).ToggleMask(0b1010);
            thirteen.Should().Be(0b1101);
        }

        [Fact]
        public void IsMask()
        {
            ((ushort)0b101).IsMask(0b101).Should().BeTrue();
            ((ushort)0b101).IsMask(0b111).Should().BeFalse();
        }

        [Fact]
        // Would fail on a big endian system?
        public void GetIndividualBytes()
        {
            ushort data = 0b00000001_00000010;
            data.Byte(UShort.Bytes.First).Should().Be(2);
            data.Byte(UShort.Bytes.Second).Should().Be(1);
        }

        [Fact]
        public void PackAndUnpack()
        {
            var data = UShort.Pack(1, 2);
            var (first, second) = data.Unpack();
            first.Should().Be(1);
            second.Should().Be(2);            
        }
    }
}

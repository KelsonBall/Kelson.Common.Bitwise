using FluentAssertions;
using Xunit;

namespace Kelson.Common.Bitwise.Tests
{
    public class ULongExtensions_Should
    {
        [Fact]
        public void SetBits()
        {
            var one = 0ul.Set(0);
            one.Should().Be(1);
            var three = one.Set(1);
            three.Should().Be(3);
        }

        [Fact]
        public void ClearBits()
        {
            var three = 0ul.Set(0).Set(1);
            three.Should().Be(3);
            var two = three.Clear(0);
            two.Should().Be(2);
            var zero = two.Clear(1);
            zero.Should().Be(0);
        }

        [Fact]
        public void ToggleBits()
        {
            var three = 0ul.Toggle(0).Toggle(1);
            three.Should().Be(3);
            var one = three.Toggle(1);
            one.Should().Be(1);
        }

        [Fact]
        public void CheckBitsSet()
        {
            ulong five = 5;
            five.IsSet(0).Should().BeTrue();
            five.IsSet(1).Should().BeFalse();
            five.IsSet(2).Should().BeTrue();
            five.IsSet(3).Should().BeFalse();
        }

        [Fact]
        public void SetMask()
        {
            var five = 0ul.SetMask(5);
            five.Should().Be(5);
            var seven = five.SetMask(2);
            seven.Should().Be(7);
        }

        [Fact]
        public void ClearMask()
        {
            var two = 0ul.SetMask(0b111).ClearMask(0b101);
            two.Should().Be(0b010);
        }

        [Fact]
        public void ToggleMask()
        {
            var thirteen = 0ul.ToggleMask(0b0111).ToggleMask(0b1010);
            thirteen.Should().Be(0b1101);
        }

        [Fact]
        public void IsMask()
        {
            0b101ul.IsMask(0b101).Should().BeTrue();
            0b101ul.IsMask(0b111).Should().BeFalse();
        }

        [Fact]
        // Would fail on a big endian system?
        public void GetIndividualBytes()
        {
            ulong data = 0b00000001_00000010_00000011_00000100_00000101_00000110_00000111_00001000ul;
            data.Byte(ULong.Bytes.First).Should().Be(8);
            data.Byte(ULong.Bytes.Second).Should().Be(7);
            data.Byte(ULong.Bytes.Third).Should().Be(6);
            data.Byte(ULong.Bytes.Fourth).Should().Be(5);
            data.Byte(ULong.Bytes.Fifth).Should().Be(4);
            data.Byte(ULong.Bytes.Sixth).Should().Be(3);
            data.Byte(ULong.Bytes.Seventh).Should().Be(2);
            data.Byte(ULong.Bytes.Eigth).Should().Be(1);
        }

        [Fact]
        public void PackAndUnpack()
        {
            var data = ULong.Pack(1, 2, 3, 4, 5, 6, 7, 8);
            var (first, second, third, fourth, fifth, sixth, seventh, eigth) = data.Unpack();
            first.Should().Be(1);
            second.Should().Be(2);
            third.Should().Be(3);
            fourth.Should().Be(4);
            fifth.Should().Be(5);
            sixth.Should().Be(6);
            seventh.Should().Be(7);
            eigth.Should().Be(8);
        }
    }
}

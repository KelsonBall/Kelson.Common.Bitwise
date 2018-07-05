using FluentAssertions;
using Xunit;

namespace Kelson.Common.Bitwise.Tests
{
    public class ByteExtensions_Should
    {
        [Fact]
        public void SetBits()
        {
            var one = ((byte)0).Set(0);
            one.Should().Be(1);
            var three = one.Set(1);
            three.Should().Be(3);
        }

        [Fact]
        public void ClearBits()
        {
            var three = ((byte)0).Set(0).Set(1);
            three.Should().Be(3);
            var two = three.Clear(0);
            two.Should().Be(2);
            var zero = two.Clear(1);
            zero.Should().Be(0);
        }

        [Fact]
        public void ToggleBits()
        {
            var three = ((byte)0).Toggle(0).Toggle(1);
            three.Should().Be(3);
            var one = three.Toggle(1);
            one.Should().Be(1);
        }

        [Fact]
        public void CheckBitsSet()
        {
            byte five = 5;
            five.IsSet(0).Should().BeTrue();
            five.IsSet(1).Should().BeFalse();
            five.IsSet(2).Should().BeTrue();
            five.IsSet(3).Should().BeFalse();
        }

        [Fact]
        public void SetMask()
        {
            var five = ((byte)0).SetMask(5);
            five.Should().Be(5);
            var seven = five.SetMask(2);
            seven.Should().Be(7);
        }

        [Fact]
        public void ClearMask()
        {
            var two = ((byte)0).SetMask(0b111).ClearMask(0b101);
            two.Should().Be(0b010);
        }

        [Fact]
        public void ToggleMask()
        {
            var thirteen = ((byte)0).ToggleMask(0b0111).ToggleMask(0b1010);
            thirteen.Should().Be(0b1101);
        }

        [Fact]
        public void IsMask()
        {
            ((byte)0b101).IsMask(0b101).Should().BeTrue();
            ((byte)0b101).IsMask(0b111).Should().BeFalse();
        }        
    }
}

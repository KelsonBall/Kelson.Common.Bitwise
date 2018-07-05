namespace Kelson.Common.Bitwise
{
    public static class Byte
    {
        public static byte Set(this byte data, int index)
            => (byte)(data | (1 << index));

        public static byte SetMask(this byte data, int mask)
            => (byte)(data | mask);

        public static byte Clear(this byte data, int index)
            => (byte)(data & ~(1 << index));

        public static byte ClearMask(this byte data, int mask)
            => (byte)(data & ~mask);

        public static byte Toggle(this byte data, int index)
            => (byte)(data ^ (1 << index));

        public static byte ToggleMask(this byte data, int mask)
            => (byte)(data ^ mask);

        public static bool IsSet(this byte data, int index)
            => ((data >> index) & 1) == 1;

        public static bool IsMask(this byte data, int mask)
            => data == mask;   
    }
}
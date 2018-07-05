namespace Kelson.Common.Bitwise
{
    public static class UShort
    {
        public static ushort Set(this ushort data, int index)
            => (ushort)(data | (1 << index));

        public static ushort SetMask(this ushort data, int mask)
            => (ushort)(data | mask);

        public static ushort Clear(this ushort data, int index)
            => (ushort)(data & ~(1 << index));

        public static ushort ClearMask(this ushort data, int mask)
            => (ushort)(data & ~mask);

        public static ushort Toggle(this ushort data, int index)
            => (ushort)(data ^ (1 << index));

        public static ushort ToggleMask(this ushort data, int mask)
            => (ushort)(data ^ mask);

        public static bool IsSet(this ushort data, int index)
            => ((data >> index) & 1) == 1;

        public static bool IsMask(this ushort data, int mask)
            => data == mask;

        public enum Bytes
        {
            First = 0,
            Second = 8,            
        }

        public static byte Byte(this ushort data, Bytes @byte)
            => (byte)((data & 0xFF << (int)@byte) >> (int)@byte);

        public static ushort Pack(byte first, byte second)
            => (ushort)(first << (int)Bytes.First | second << (int)Bytes.Second);

        public static (byte, byte) Unpack(this ushort data)
            => (data.Byte(Bytes.First), data.Byte(Bytes.Second));
    }
}
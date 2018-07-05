using System;

namespace Kelson.Common.Bitwise
{
    public static class Integer
    {
        public static int Set(this int data, int index)
            => data | (1 << index);

        public static int SetMask(this int data, int mask)
            => data | mask;

        public static int Clear(this int data, int index)
            => data & ~(1 << index);

        public static int ClearMask(this int data, int mask)
            => data & ~mask;

        public static int Toggle(this int data, int index)
            => data ^ (1 << index);

        public static int ToggleMask(this int data, int mask)
            => data ^ mask;

        public static bool IsSet(this int data, int index)
            => ((data >> index) & 1) == 1;

        public static bool IsMask(this int data, int mask)
            => data == mask;

        public enum Bytes
        {
            First = 0,
            Second = 8,
            Third = 16,
            Fourth = 24
        }

        /// <summary>
        /// Returns the specified byte of an integer.        
        /// </summary>
        /// <param name="data"></param>
        /// <param name="byte"></param>
        /// <returns></returns>
        public static byte Byte(this int data, Bytes @byte)
            => (byte)((data & 0xFF << (int)@byte) >> (int)@byte);

        public static int Pack(byte first, byte second, byte third, byte fourth)
            => first << (int)Bytes.Fourth | second << (int)Bytes.Third | third << (int)Bytes.Second | fourth;
        
        public static (byte, byte, byte, byte) Unpack(this int data)
            => (data.Byte(Bytes.First), data.Byte(Bytes.Second), data.Byte(Bytes.Third), data.Byte(Bytes.Fourth));
    }
}
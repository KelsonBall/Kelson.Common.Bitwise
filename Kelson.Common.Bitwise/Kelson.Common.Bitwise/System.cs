namespace Kelson.Common.Bitwise
{
    public static class System
    {
        public enum Endianess : byte
        {            
            Little      = 1,            
            Big         = 4,            
        }

        static readonly uint SYSTEM_ORDER = 0x01020304;

        /// <summary>
        /// Untested! I'm not really sure how to test this since I seem to only have little endian systems
        /// </summary>        
        public static Endianess GetEndianess() => (Endianess)(SYSTEM_ORDER >> 24);
    }
}

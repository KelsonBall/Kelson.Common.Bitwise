namespace Kelson.Common.Bitwise
{
    public static class ByteArrayExtensions
    {
        public static unsafe bool IsEquivalentTo(this byte[] data, byte[] compare)
        {
            if (data.Length != compare.Length)
                return false;
            if (data.Length == 0)
                return true;
            var longLength = (data.Length / 8) * 8;
            fixed (byte* longData = &data[0])
            {
                fixed (byte* longCompare = &compare[0])
                {
                    for (int i = 0; i < longLength; i += 8)
                        if (*(ulong*)(longData + i) != *(ulong*)(longCompare + i))
                            return false;
                }
            }
            for (int i = longLength; i < data.Length; i++)
                if (data[i] != compare[i])
                    return false;
            return true;
        }
    }
}

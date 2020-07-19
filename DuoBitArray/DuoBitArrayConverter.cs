using System;

namespace SkiDiveCode.DuoBitDataStructures.BitArrays
{
    /// <summary>
    /// Converts <see cref="IReadOnlyDuoBitArray"/> bit arrays to different types.  Typically used to convert a
    /// duo bit array into a format suitable for database storage, searching, etc.
    /// </summary>
    public class DuoBitArrayConverter
    {
        /// <summary>
        /// Returns the sub bit array from the given <paramref name="bitArray"/> as an <see langword=""="UInt64"/>
        /// value.  The sub bit array, left or right, is selected by the given <paramref name="getFromLeftBits"/>
        /// boolean.
        /// </summary>
        public static ulong ToUInt64(IReadOnlyDuoBitArray bitArray, bool getFromLeftBits)
        {
            var byteArray = getFromLeftBits
                ? bitArray
                    .GetLeftBits(0, 8 * sizeof(ulong))
                    .ToByteArray()
                : bitArray
                    .GetRightBits(0, 8 * sizeof(ulong))
                    .ToByteArray();

            return BitConverter.ToUInt64(byteArray, 0);
        }
    }
}

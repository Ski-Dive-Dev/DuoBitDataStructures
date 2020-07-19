using System;

namespace SkiDiveDev.DuoBitDataStructures.BitArrays
{
    /// <summary>
    /// Represents an array of bits that can be enumerated from both the "left" side and separately, from the
    /// "right" side, where "left" refers to bits in the lowest numbered indices and conversely, "right" refers to
    /// bits in the highest numbered indices, irrespective of "endianness."
    /// </summary>
    public interface IReadOnlyDuoBitArray : IBitCollection
    {
        int LeftLength { get; }

        int RightLength { get; }


        /// <summary>
        /// Dequeues <paramref name="numBits"/> consecutive bits starting at the given index.  If padding is
        /// required, the last byte will be padded with <c>(8 - <paramref name="numBits"/> % 8)</c> zeroes in the
        /// least-significant-bits.
        /// </summary>
        /// <remarks>
        /// The above formula is not to suggest that a byte might be padded with 8 zeroes.
        /// </remarks>
        /// <param name="index">The 0-based index of the first bit position to retrieve.</param>
        /// <param name="numBits">The number of bits to return.</param>
        /// <returns>A byte array whose first element's most-significant-bits are filled with data.</returns>
        IReadOnlyDuoBitArray GetLeftBits(int index, int numBits);


        /// <summary>
        /// Dequeues <paramref name="numBits"/> consecutive bits starting at the given index.  If padding is
        /// required, the first byte will be padded with <c>(8 - <paramref name="numBits"/> % 8)</c> zeroes in the
        /// most-significant-bits.
        /// </summary>
        /// <remarks>
        /// The above formula is not to suggest that a byte might be padded with 8 zeroes.
        /// </remarks>
        /// <param name="index">The 0-based index of the first bit position to retrieve.</param>
        /// <param name="numBits">The number of bits to return.</param>
        /// <returns>A byte array whose last element's least-significant-bits are filled with data.</returns>
        IReadOnlyDuoBitArray GetRightBits(int index, int numBits);


        /// <summary>
        /// References the bit identified with the given index within the bit array that underlies both the left
        /// and the right bit arrays.
        /// </summary>
        /// <remarks>
        /// For non-C# developers, this is called an "indexer".  In the setter, the <see langword="value"/> is the
        /// value provided on the right-side of an assignment (e.g., <c>fooBitmap[7] = value</c>.)
        /// </remarks>
        /// <param name="index">The 0-based index of the bit within the bit array.</param>
        bool this[int index] { get; }


        byte GetBit(int index);
        void ClearBit(int index);
    }
}

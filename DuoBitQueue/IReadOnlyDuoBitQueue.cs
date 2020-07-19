using System;
using SkiDiveDev.DuoBitDataStructures.BitArrays;

namespace SkiDiveDev.DuoBitDataStructures.BitQueues
{
    /// <summary>
    /// Represents an array of bits that can be enumerated from both the "left" side and separately, from the
    /// "right" side, where "left" refers to bits in the lowest numbered indices and conversely, "right" refers to
    /// bits in the highest numbered indices, irrespective of "endianness."
    /// </summary>
    public interface IReadOnlyDuoBitQueue
    {
        /// <summary>
        /// Returns <see langword="true"/> if any values encoded within the bit array are little-endian, or
        /// <see langword="false"/> if any values encoded within the bit array are big-endian, as provided at
        /// object construction.
        /// </summary>
        bool EncodedValuesAreLittleEndian { get; }


        /// <summary>
        /// Returns the number of bits set in <see cref="GetMaskOfUnusedBits"/>.
        /// </summary>
        int NumRemainingBits { get; }


        /// <summary>
        /// Gets the mask of bits that have not been decoded yet.
        /// </summary>
        IReadOnlyDuoBitArray GetMaskOfUnusedBits { get; }


        /// <summary>
        /// Returns the starting position of the next value or sub bit-array to be set or read with a "FromTheLeft"
        /// method.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The "left-most" element in the array is the element at index-0, and the "right-most" element in the
        /// array is the one at <see cref="Array.Length"/> minus 1 (i.e., n-1).
        /// </para>
        /// </remarks>
        int LeftBitPointer { get; }


        /// <summary>
        /// Returns the starting position of the next value or sub bit-array to be set or read with an "OnTheRight"
        /// method.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The "left-most" element in the array is the element at index-0, and the "right-most" element in the
        /// array is the one at <see cref="Array.Length"/> minus 1 (i.e., n-1).
        /// </para>
        /// </remarks>
        int RightBitPointer { get; }


        /// <summary>
        /// Moves <see cref="LeftBitPointer"/> forward (towards the right) by the given number of bits.
        /// </summary>
        IReadOnlyDuoBitQueue SkipForwardOnTheLeft(int numBitsMoveForward);


        /// <summary>
        /// Moves <see cref="RightBitPointer"/> forward (towards the left) by the given number of bits.
        /// </summary>
        IReadOnlyDuoBitQueue SkipForwardOnTheRight(int numBitsMoveForward);
    }
}

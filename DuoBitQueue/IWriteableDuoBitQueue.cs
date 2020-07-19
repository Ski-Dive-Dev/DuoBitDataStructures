using System;
using System.Collections.Generic;
using SkiDiveDev.DuoBitDataStructures.BitArrays;

namespace SkiDiveDev.DuoBitDataStructures.BitQueues
{
    /// <summary>
    /// Represents an object that has both a bitmap and a mask which indicate which bits of that bitmap have valid
    /// data.  The bitmap is read/write, meaning data can both be enqueued and dequeued.
    /// </summary>
    public interface IWriteableDuoBitQueue : IReadOnlyDuoBitArray
    {
        /// <summary>
        /// Returns <see langword="true"/> if any values encoded within the bitmap are little-endian, or
        /// <see langword="false"/> if any values encoded within the bitmap are big-endian, as provided at object
        /// construction.
        /// </summary>
        bool EncodedValuesAreLittleEndian { get; }


        /// <summary>
        /// Returns the number of bits set in <see cref="GetMaskOfUnusedBits"/>.
        /// </summary>
        int NumRemainingBits { get; }


        IWriteableDuoBitQueue EnqueueLeft(byte[] valueToSet, int numSignificantBits);

        IWriteableDuoBitQueue EnqueueRight(byte[] valueToSet, int numSignificantBits);
    }


    [Obsolete]
    public interface IWriteableDuoBitQueueX
    {
        /// <summary>
        /// Encodes and enqueues (appends) the specified number of bits from the given value into the bitmap,
        /// starting with the current left bitpointer.
        /// </summary>
        /// <remarks>
        /// Only bits that pass the mask are considered, and the mask may include non-adjacent bits.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown if <paramref name="numSignificantBits"/> is
        /// greater than the number of bits in <typeparamref name="T"/>.</exception>
        /// <param name="numSignificantBits">The number of bits within the given value to encode.  The bits that
        /// are encoded from the value are the least significant ones.</param>
        void NqLeft(byte[] valueToSet, int numSignificantBits);


        /// <summary>
        /// Encodes and enqueues (appends) the specified number of bits from the given value into the bitmap,
        /// starting with the current right bitpointer.
        /// </summary>
        /// <remarks>
        /// Only bits that pass the mask are considered, and the mask may include non-adjacent bits.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown if <paramref name="numSignificantBits"/> is
        /// greater than the number of bits in <typeparamref name="T"/>.</exception>
        /// <param name="numSignificantBits">The number of bits within the given value to encode.  The bits that
        /// are encoded from the value are the least significant ones.</param>
        void NqRight(byte[] valueToSet, int numSignificantBits);
    }
}

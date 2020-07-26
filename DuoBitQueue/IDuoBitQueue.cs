using System;
using SkiDiveDev.DuoBitDataStructures.BitArrays;

namespace SkiDiveDev.DuoBitDataStructures.BitQueues
{
    /// <summary>
    /// Represents a single data structure which incorporates two separate bit-oriented queues, referred to as the
    /// "left" and "right" queues.
    /// </summary>
    public interface IDuoBitQueue : IBitCollection, IBitArrayMask
    {
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
        /// Enqueues the least <paramref name="numSignificantBits"/> significant bits of the given byte array into
        /// the "left" queue.
        /// </summary>
        /// <remarks>
        /// This method supports "method-chaining" for convenient successive queueing of data.
        /// </remarks>
        /// <param name="valueToSet">The value to set, with the endianness set according to the platform that the
        /// code is currently running in.</param>
        /// <param name="numSignificantBits">The number of least significant bits from
        /// <paramref name="valueToSet"/> to enqueue.</param>
        /// <returns>A <i>mutable</i> object for method-chaining -- all references to the duo bit queue are the
        /// same, and therefore, the returned object may be discarded if another object holds a reference (for
        /// additional enqueueing or dequeueing -- from either the "left" or "right queues.)</returns>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown if <paramref name="numSignificantBits"/> is
        /// greater than the number of bits indicated by <see cref="IBitCollection.RemainingCapacity"/>.
        /// </exception>
        IDuoBitQueue EnqueueLeft(byte[] valueToSet, int numSignificantBits);


        /// <summary>
        /// Enqueues the least <paramref name="numSignificantBits"/> significant bits of the given byte array into
        /// the "right" queue.
        /// </summary>
        /// <remarks>
        /// This method supports "method-chaining" for convenient successive queueing of data.
        /// </remarks>
        /// <param name="valueToSet">The value to set, with the endianness set according to the platform that the
        /// code is currently running in.</param>
        /// <param name="numSignificantBits">The number of least significant bits from
        /// <paramref name="valueToSet"/> to enqueue.</param>
        /// <returns>A <i>mutable</i> object for method-chaining -- all references to the duo bit queue are the
        /// same, and therefore, the returned object may be discarded if another object holds a reference (for
        /// additional enqueueing or dequeueing -- from either the "left" or "right queues.)</returns>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown if <paramref name="numSignificantBits"/> is
        /// greater than the number of bits indicated by <see cref="IBitCollection.RemainingCapacity"/>.
        /// </exception>
        IDuoBitQueue EnqueueRight(byte[] valueToSet, int numSignificantBits);


        /// <summary>
        /// Dequeues the given number of bits from the "left" queue.
        /// </summary>
        /// <param name="numBits">The number of bits to dequeue from the "left" queue.</param>
        /// <returns>An <see cref="IReadOnlyDuoBitArray"/> structure which contains the dequeued bits.
        /// </returns>
        /// <exception cref="InvalidOperationException">Is thrown if <paramref name="numBits"/> exceeds
        /// the number of bits indicated by <see cref="IBitCollection.Length"/>.</exception>
        IReadOnlyDuoBitArray DequeueLeft(int numBits);


        /// <summary>
        /// Dequeues the given number of bits from the "right" queue.
        /// </summary>
        /// <param name="numBits">The number of bits to dequeue from the "right" queue.</param>
        /// <returns>An <see cref="IReadOnlyDuoBitArray"/> structure which contains the dequeued bits.
        /// </returns>
        /// <exception cref="InvalidOperationException">Is thrown if <paramref name="numBits"/> exceeds
        /// the number of bits indicated by <see cref="IBitCollection.Length"/>.</exception>
        IReadOnlyDuoBitArray DequeueRight(int numBits);
    }
}

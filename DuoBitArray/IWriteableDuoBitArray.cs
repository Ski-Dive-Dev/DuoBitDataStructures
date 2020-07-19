namespace SkiDiveDev.DuoBitDataStructures.BitArrays
{
    /// <summary>
    /// An interface to a read/write duo bit array, with support for a "fluent" interface.
    /// </summary>
    public interface IWriteableDuoBitArray : IReadOnlyDuoBitArray
    {
        /// <summary>
        /// Sets a range of bits from the given source bits array to the 0-based index in the destination "left"
        /// bit array.
        /// </summary>
        /// <param name="sourceBits">The source data to set to the "left" bit array.</param>
        /// <param name="sourceArrayBitIndex">The 0-based bit index within the source array where copying starts.
        /// </param>
        /// <param name="destinationBitIndex">The 0-based bit index within the "right" bit array that starts to
        /// received the copied bits.</param>
        /// <param name="numBits">The number of bits to copy from the source bit array to the "left" bit array.
        /// </param>
        /// <returns>A reference to the invoked object for a "fluent" interface.</returns>
        IWriteableDuoBitArray SetLeftBits(byte[] sourceBits, int sourceArrayBitIndex, int destinationBitIndex,
            int numBits);

        IWriteableDuoBitArray SetRightBits(byte[] sourceBits, int sourceArrayBitIndex, int destinationBitIndex,
            int numBits);

        void SetBit(int index);
    }
}

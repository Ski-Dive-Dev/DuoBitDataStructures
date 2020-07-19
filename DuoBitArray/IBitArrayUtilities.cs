namespace SkiDiveDev.DuoBitDataStructures.BitArrays
{
    public interface IBitArrayUtilities
    {
        byte AlignAndMergeBytes(byte sourceByte, int sourceMsbIndex, byte destinationByte,
            int destinationLsbIndex, IBitMaskUtilities maskUtilities);

        byte ShiftBitsInOneByte(byte[] sourceBytes, int sourceByteIndex, int shiftAmount);


        /// <summary>
        /// Returns the 0-based byte index of the byte that contains the bit with the given 0-based bitIndex.
        /// </summary>
        int GetByteIndexFromBitIndex(int bitIndex);


        /// <summary>
        /// Returns a copy of <see cref="bitArray"/> with all its bits inverted.
        /// </summary>
        byte[] GetInvertedBytes(byte[] bitArray);


        /// <summary>
        /// Given the number of bits, returns the minimum number of bytes that would be required to store those
        /// bits.
        /// </summary>
        int GetMinNumBytesToStoreBits(int numEnclosedBits);


        int GetNumBitsInUseInLastByte(int numBitsInArray);


        /// <summary>
        /// Given the total number of bits encoded in a byte array, returns the number of bits that are unused in
        /// the last byte of the array (if left-aligned), or the number of bits that are unused in the first byte
        /// of the array (if right-aligned).
        /// </summary>
        int GetNumUnusedBitsInByteArray(int totalNumEncodedBits);
    }
}

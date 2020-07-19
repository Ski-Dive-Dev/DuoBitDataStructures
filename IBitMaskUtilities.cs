namespace SkiDiveCode.DuoBitDataStructures.BitArrays
{
    public interface IBitMaskUtilities
    {
        byte GetByteLsbMask(int numBitsToMask);
        byte GetByteMsbMask(int numBitsToMask);


        /// <summary>
        /// Returns a byte with the bit identified by the given index set (and all other bits clear).
        /// </summary>
        /// <param name="bitIndex">The 0-based index of a bit, with index 0 being the position of the least
        /// significant bit.</param>
        byte GetMaskForBitIndex(int bitIndex);
    }
}
namespace SkiDiveCode.DuoBitDataStructures.BitArrays
{
    /// <summary>
    /// An interface to a method which calculates the Hamming weight of a bit array.
    /// </summary>
    public interface IBitCounter
    {
        /// <summary>
        /// A method to calculate the Hamming weight of a <see cref="IReadOnlyDuoBitArray"/>.
        /// </summary>
        /// <param name="bitMap"></param>
        /// <returns></returns>
        int CountSetBits(IReadOnlyDuoBitArray bitMap);
    }


    public class BitCounter : IBitCounter
    {
        /// <summary>
        /// An O(n) method to calculate the Hamming weight of a bit array, where n is the total number of bits in
        /// the array.
        /// </summary>
        public int CountSetBits(IReadOnlyDuoBitArray readOnlyBitMap)
        {
            var byteArray = readOnlyBitMap.ToByteArray();
            var numOneBits = 0;

            for (var i = 0; i < byteArray.Length; i++)
            {
                var thisByte = byteArray[i];
                while (thisByte != 0)
                {
                    numOneBits += (int)(thisByte % 2);
                    thisByte >>= 1;
                }
            }

            return numOneBits;
        }
    }
}

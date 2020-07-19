using System;

namespace SkiDiveDev.DuoBitDataStructures.BitArrays
{
    public class BitMaskUtilities : IBitMaskUtilities
    {
        protected BitMaskUtilities()
        { /* No additional construction required; constructor protected from public use. */ }


        public static IBitMaskUtilities Create() => new BitMaskUtilities();


        /// <summary>
        /// Returns a byte mask with the given number of high-order bits set.
        /// </summary>
        public byte GetByteMsbMask(int numBitsToMask)
        {
            if (numBitsToMask < 0 || numBitsToMask > 8)
            {
                throw new ArgumentOutOfRangeException(nameof(numBitsToMask));
            }

            return (byte)(0xFF << (7 - (numBitsToMask - 1) % 8) & 0xFF);
        }



        /// <summary>
        /// Returns a byte mask with the given number of low-order bits sets.
        /// </summary>
        public byte GetByteLsbMask(int numBitsToMask)
        {
            if (numBitsToMask < 0 || numBitsToMask > 8)
            {
                throw new ArgumentOutOfRangeException(nameof(numBitsToMask));
            }

            return (byte)((1 << numBitsToMask) - 1);
        }



        /// <summary>
        /// Returns a byte with the bit identified by the given index set (and all other bits clear).
        /// </summary>
        /// <param name="lsb0BitIndex">The 0-based index of a bit, with index 0 being the position of the least
        /// significant bit.</param>
        public byte GetMaskForBitIndex(int lsb0BitIndex)
        {
            if (lsb0BitIndex < 0 || lsb0BitIndex > 8)
            {
                throw new ArgumentOutOfRangeException($"The expected {nameof(lsb0BitIndex)} value should be the" +
                    $" index position of a bit within a single byte (not, for example, within a byte array.)");
            }

            return (byte)(1 << lsb0BitIndex);
        }
    }
}

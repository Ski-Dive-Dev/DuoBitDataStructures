using System;
using System.Linq;

namespace SkiDiveCode.DuoBitDataStructures.BitArrays
{
    /// <summary>
    /// A variety of bit array related methods that may be broadly useful.
    /// </summary>
    public class BitArrayUtilities : IBitArrayUtilities
    {
        protected BitArrayUtilities()
        { /* No additional construction required; constructor protected from public use. */ }


        public static IBitArrayUtilities Create()
        {
            return new BitArrayUtilities();
        }


        /// <summary>
        /// Takes source bits, starting at the given MSB-0 bit index, and merges it into the given destination byte,
        /// at the given destination MSB-0 bit index.
        /// </summary>
        /// <param name="sourceByte">The source bits that will be merged with the destination bits.</param>
        /// <param name="sourceMsbIndex">The MSB-0 index into the <paramref name="sourceByte"/>, which is the
        /// starting index of the bits to be merged; MSB-0 index 7 (which is the LSB) is the ending index.</param>
        /// <param name="destinationByte">The other group of bits that will be merged with the source bits.</param>
        /// <param name="destinationLsbIndex">The MSB-0 index into the <paramref name="destinationByte"/>, which
        /// is the <i>ending</i> index of the bits to be merged; MSB-0 index 0 (which is the MSB) is the starting
        /// index.</param>
        /// <param name="maskUtilities">An injected object which implements the <see cref="IBitMaskUtilities"/>
        /// interface.</param>
        /// <returns>The two bytes, blended together, at their given offsets.</returns>
        public byte AlignAndMergeBytes(byte sourceByte, int sourceMsbIndex, byte destinationByte,
            int destinationLsbIndex, IBitMaskUtilities maskUtilities)
        {
            const byte byteMaskToTruncateLeftBits = 0xFF;
            var sourceByteMask = (byte)~maskUtilities.GetByteMsbMask(sourceMsbIndex);
            var destinationByteMask = maskUtilities.GetByteMsbMask(destinationLsbIndex + 1);

            sourceByte &= sourceByteMask;

            var deltaShiftAmount = destinationLsbIndex - sourceMsbIndex + 1;
            if (deltaShiftAmount < 0)
            {
                sourceByte = (byte)((sourceByte << -deltaShiftAmount) & byteMaskToTruncateLeftBits);
            }
            else
            {
                sourceByte = (byte)(sourceByte >> deltaShiftAmount);
            }

            return (byte)(destinationByte & destinationByteMask | sourceByte);
        }


        public byte ShiftBitsInOneByte(byte[] sourceBytes, int indexOfByteToShift, int shiftAmount)
        {
            if (sourceBytes == null)
            {
                throw new NullReferenceException(nameof(sourceBytes));
            }
            else if (indexOfByteToShift < -1 || indexOfByteToShift > sourceBytes.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(indexOfByteToShift));
            }

            var shiftedByte = (indexOfByteToShift == -1 || indexOfByteToShift == sourceBytes.Length)
                ? (byte)0x00                                        // So that the borrowed bits can be obtained
                : sourceBytes[indexOfByteToShift];

            if (shiftAmount < 0)
            {
                shiftAmount = -shiftAmount;

                var borrowedBitsShiftedIntoPosition =
                    (indexOfByteToShift + 1 < sourceBytes.Length)
                    ? (byte)(sourceBytes[indexOfByteToShift + 1] >> (8 - shiftAmount))
                    : (byte)0x00;

                shiftedByte = (byte)(shiftedByte << shiftAmount | borrowedBitsShiftedIntoPosition);
            }
            else
            {
                var borrowedBitsShiftedIntoPosition = (indexOfByteToShift > 0)
                    ? (byte)(sourceBytes[indexOfByteToShift - 1] << (8 - shiftAmount))
                    : (byte)0x00;

                shiftedByte = (byte)(shiftedByte >> shiftAmount | borrowedBitsShiftedIntoPosition);
            }
            return shiftedByte;
        }


        /// <summary>
        /// Returns the 0-based byte index of the byte that contains the bit with the given 0-based bitIndex.
        /// </summary>
        public int GetByteIndexFromBitIndex(int bitIndex)
            => GetMinNumBytesToStoreBits(bitIndex + 1) - 1;


        /// <summary>
        /// Given the number of bits, returns the minimum number of bytes that would be required to store those
        /// bits.
        /// </summary>
        public int GetMinNumBytesToStoreBits(int numEnclosedBits) => (int)Math.Ceiling(numEnclosedBits / 8D);


        /// <summary>
        /// Given the total number of bits in use in a byte array, returns the number of bits that are in use in
        /// the array's last byte.
        /// </summary>
        public int GetNumBitsInUseInLastByte(int numBitsInArray)
        {
            if (numBitsInArray < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numBitsInArray));
            }

            if (numBitsInArray == 0)
            {
                return 0;
            }

            var moduloResult = numBitsInArray % 8;
            return moduloResult == 0 ? 8 : moduloResult;
        }


        /// <summary>
        /// Given the total number of bits encoded in a byte array, returns the number of bits that are unused in
        /// the last byte of the array (if left-aligned), or the number of bits that are unused in the first byte
        /// of the array (if right-aligned).
        /// </summary>
        public int GetNumUnusedBitsInByteArray(int totalNumEncodedBits)
            => 8 - GetNumBitsInUseInLastByte(totalNumEncodedBits);


        /// <summary>
        /// Returns a copy of <see cref="bitArray"/> with all its bits inverted.
        /// </summary>
        public byte[] GetInvertedBytes(byte[] bitArray) => bitArray
           .Select(b => (byte)~b)
           .ToArray<byte>();
    }
}

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SkiDiveDev.DuoBitDataStructures.BitArrays
{
    public class DuoBitArrayUtilities : IDuoBitArrayUtilities
    {
        /// <summary>
        /// Constructor for injecting utilities.
        /// </summary>
        public DuoBitArrayUtilities(IBitArrayUtilities bitArrayUtilities, IBitMaskUtilities bitMaskUtilities,
            IBitCounter bitCounter)
        {
            this.bitArrayUtilities = bitArrayUtilities;
            this.bitMaskUtilities = bitMaskUtilities;
            this.bitCounter = bitCounter;
        }


        /// <summary>
        /// Default constructor using default utilities.
        /// </summary>
        public DuoBitArrayUtilities()
        {
            bitArrayUtilities = BitArrayUtilities.Create();
            bitMaskUtilities = BitMaskUtilities.Create();
            bitCounter = new BitCounter();
        }


        IBitArrayUtilities bitArrayUtilities;
        IBitMaskUtilities bitMaskUtilities;
        IBitCounter bitCounter;

        public IDuoBitArrayUtilities AddBitCounter(IBitCounter bitCounter)
        {
            this.bitCounter = bitCounter;
            return this;
        }

        public IDuoBitArrayUtilities AddBitArrayUtilities(IBitArrayUtilities bitArrayUtilities)
        {
            this.bitArrayUtilities = bitArrayUtilities;
            return this;
        }

        public IBitMaskUtilities AddBitMaskUtilities(IBitMaskUtilities bitMaskUtilities)
        {
            this.bitMaskUtilities = bitMaskUtilities;
            return this;
        }


        public int CountSetBits(IReadOnlyDuoBitArray bitMap) => bitCounter.CountSetBits(bitMap);

        public byte AlignAndMergeBytes(byte sourceByte, int sourceMsbIndex, byte destinationByte,
            int destinationLsbIndex, IBitMaskUtilities maskUtilities)
            => bitArrayUtilities.AlignAndMergeBytes(sourceByte, sourceMsbIndex,
                destinationByte, destinationLsbIndex, maskUtilities);

        public byte ShiftBitsInOneByte(byte[] sourceBytes, int sourceByteIndex, int shiftAmount)
            => bitArrayUtilities.ShiftBitsInOneByte(sourceBytes, sourceByteIndex, shiftAmount);

        public int GetByteIndexFromBitIndex(int bitIndex) => bitArrayUtilities.GetByteIndexFromBitIndex(bitIndex);

        public byte GetByteLsbMask(int numBitsToMask) => bitMaskUtilities.GetByteLsbMask(numBitsToMask);
        public byte GetByteMsbMask(int numBitsToMask) => bitMaskUtilities.GetByteMsbMask(numBitsToMask);
        public byte GetMaskForBitIndex(int lsb0BitIndex) => bitMaskUtilities.GetMaskForBitIndex(lsb0BitIndex);
        
            public byte[] GetInvertedBytes(byte[] bitArray) => bitArrayUtilities.GetInvertedBytes(bitArray);


        public int GetMinNumBytesToStoreBits(int numEnclosedBits)
            => bitArrayUtilities.GetMinNumBytesToStoreBits(numEnclosedBits);
        
        public int GetNumBitsInUseInLastByte(int numBitsInArray)
            => bitArrayUtilities.GetNumBitsInUseInLastByte(numBitsInArray);

        public int GetNumUnusedBitsInByteArray(int totalNumEncodedBits)
            => bitArrayUtilities.GetNumUnusedBitsInByteArray(totalNumEncodedBits);
    }
}

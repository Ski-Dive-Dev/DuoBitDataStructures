using System.Linq;

namespace SkiDiveCode.DuoBitDataStructures.BitArrays
{
    /// <summary>
    /// Represents a data structure with two independent bit arrays (left and right) and maintains a mask of used
    /// bits witihn the underlying data structure.
    /// </summary>
    public class MaskedDuoBitArray : IBitArrayMask, IWriteableDuoBitArray
    {
        protected MaskedDuoBitArray(IDuoBitArrayUtilities duoBitArrayUtilities, int capacity)
        {
            this.duoBitArrayUtilities = duoBitArrayUtilities;
            bitArray = DuoBitArray.Create(capacity, duoBitArrayUtilities);
            mask = DuoBitArray.Create(capacity, duoBitArrayUtilities);
        }


        public static MaskedDuoBitArray Create(IDuoBitArrayUtilities duoBitArrayUtilities, int capacity)
        {
            return new MaskedDuoBitArray(duoBitArrayUtilities, capacity);
        }

        private readonly IWriteableDuoBitArray bitArray;
        private readonly IWriteableDuoBitArray mask;
        private readonly IDuoBitArrayUtilities duoBitArrayUtilities;

        public IReadOnlyDuoBitArray Mask => mask;

        public int Length => bitArray.Length;

        public int LeftLength => bitArray.LeftLength;

        public int RightLength => bitArray.RightLength;

        public int Capacity => bitArray.Capacity;

        public int RemainingCapacity => Capacity - duoBitArrayUtilities.CountSetBits(mask);

        public bool this[int index] => bitArray[index];

        public byte GetBit(int index) => bitArray.GetBit(index);

        public void SetBit(int index)
        {
            bitArray.SetBit(index);
            mask.SetBit(index);
        }

        public void ClearBit(int index)
        {
            bitArray.ClearBit(index);
            mask.ClearBit(index);
        }



        public IReadOnlyDuoBitArray GetMaskOfUnusedBits()
        {
            var invertedMask = duoBitArrayUtilities.GetInvertedBytes(mask.ToByteArray());

            var maskOfUnusedBits = DuoBitArray.Create(Capacity, duoBitArrayUtilities)
                .SetLeftBits(invertedMask,
                    sourceArrayBitIndex: 0,
                    destinationBitIndex: 0,
                    numBits: mask.Length);

            return maskOfUnusedBits;
        }


        public IWriteableDuoBitArray SetLeftBits(byte[] sourceBits, int sourceArrayBitIndex,
            int destinationBitIndex, int numBits)
        {
            bitArray.SetLeftBits(sourceBits, sourceArrayBitIndex, destinationBitIndex, numBits);

            var sourceBitsMask = GetMaskForConsecutiveLeftBits(numBits);
            mask.SetLeftBits(sourceBitsMask, sourceArrayBitIndex, destinationBitIndex, numBits);
            return this;
        }

        public IWriteableDuoBitArray SetRightBits(byte[] sourceBits, int sourceArrayBitIndex,
            int destinationBitIndex, int numBits)
        {
            bitArray.SetRightBits(sourceBits, sourceArrayBitIndex, destinationBitIndex, numBits);

            var sourceBitsMask = GetMaskForConsecutiveRightBits(numBits);
            mask.SetRightBits(sourceBitsMask, sourceArrayBitIndex, destinationBitIndex, numBits);
            return this;
        }


        public IReadOnlyDuoBitArray GetLeftBits(int index, int numBits) => bitArray.GetLeftBits(index, numBits);

        public IReadOnlyDuoBitArray GetRightBits(int index, int numBits) => bitArray.GetRightBits(index, numBits);

        public byte[] ToByteArray() => bitArray.ToByteArray();


        private byte[] GetMaskForConsecutiveLeftBits(int numBits)
        {
            var numBytes = duoBitArrayUtilities.GetMinNumBytesToStoreBits(numBits);
            var generatedMask = new byte[numBytes];
            generatedMask[numBytes - 1] =
                duoBitArrayUtilities.GetByteMsbMask(duoBitArrayUtilities.GetNumBitsInUseInLastByte(numBytes));

            for (var i = 0; i < numBytes - 2; i++)
            {
                generatedMask[i] = 0xFF;
            }

            return generatedMask;
        }

        private byte[] GetMaskForConsecutiveRightBits(int numBits)
        {
            var numBytes = duoBitArrayUtilities.GetMinNumBytesToStoreBits(numBits);
            var generatedMask = new byte[numBytes];

            generatedMask[0] =
                duoBitArrayUtilities.GetByteLsbMask(duoBitArrayUtilities.GetNumBitsInUseInLastByte(numBytes));

            for (var i = 1; i < numBytes - 1; i++)
            {
                generatedMask[i] = 0xFF;
            }

            return generatedMask;
        }
    }
}

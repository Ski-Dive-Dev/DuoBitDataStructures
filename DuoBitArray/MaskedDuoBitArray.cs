using System.Linq;

namespace SkiDiveDev.DuoBitDataStructures.BitArrays
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
            duoBitArray = DuoBitArray.Create(capacity, duoBitArrayUtilities);
            mask = DuoBitArray.Create(capacity, duoBitArrayUtilities);
        }


        public static MaskedDuoBitArray Create(IDuoBitArrayUtilities duoBitArrayUtilities, int capacity)
        {
            return new MaskedDuoBitArray(duoBitArrayUtilities, capacity);
        }

        private readonly IWriteableDuoBitArray duoBitArray;
        private readonly IWriteableDuoBitArray mask;
        private readonly IDuoBitArrayUtilities duoBitArrayUtilities;

        public IReadOnlyDuoBitArray Mask => mask;

        public int Length => duoBitArray.Length;

        public int LeftLength => duoBitArray.LeftLength;

        public int RightLength => duoBitArray.RightLength;

        public int Capacity => duoBitArray.Capacity;

        public int RemainingCapacity => Capacity - duoBitArrayUtilities.CountSetBits(mask);

        public bool this[int index] => duoBitArray[index];

        public byte GetBit(int index) => duoBitArray.GetBit(index);

        public void SetBit(int index)
        {
            duoBitArray.SetBit(index);
            mask.SetBit(index);
        }

        public void ClearBit(int index)
        {
            duoBitArray.ClearBit(index);
            mask.ClearBit(index);
        }



        public IReadOnlyDuoBitArray GetMaskOfUnusedBits()
        {
            var invertedMask = duoBitArrayUtilities.GetInvertedBytes(mask.ToByteArray());

            var maskOfUnusedBits = DuoBitArray.Create(Capacity, duoBitArrayUtilities)
                .SetLeftBits(invertedMask,
                    sourceArrayBitIndex: 0,
                    destinationBitIndex: 0,
                    numBits: mask.Capacity);

            return maskOfUnusedBits;
        }


        public IWriteableDuoBitArray SetLeftBits(byte[] sourceBits, int sourceArrayBitIndex,
            int destinationBitIndex, int numBits)
        {
            duoBitArray.SetLeftBits(sourceBits, sourceArrayBitIndex, destinationBitIndex, numBits);

            var sourceBitsMask = GetMaskForConsecutiveLeftBits(numBits);
            mask.SetLeftBits(sourceBitsMask, sourceArrayBitIndex, destinationBitIndex, numBits);
            return this;
        }

        public IWriteableDuoBitArray SetRightBits(byte[] sourceBits, int sourceArrayBitIndex,
            int destinationBitIndex, int numBits)
        {
            duoBitArray.SetRightBits(sourceBits, sourceArrayBitIndex, destinationBitIndex, numBits);

            var sourceBitsMask = GetMaskForConsecutiveRightBits(numBits);
            mask.SetRightBits(sourceBitsMask, sourceArrayBitIndex, destinationBitIndex, numBits);
            return this;
        }


        public IReadOnlyDuoBitArray GetLeftBits(int index, int numBits) => duoBitArray.GetLeftBits(index, numBits);

        public IReadOnlyDuoBitArray GetRightBits(int index, int numBits) => duoBitArray.GetRightBits(index, numBits);

        public byte[] ToByteArray() => duoBitArray.ToByteArray();


        private byte[] GetMaskForConsecutiveLeftBits(int numBits)
        {
            var numBytes = duoBitArrayUtilities.GetMinNumBytesToStoreBits(numBits);
            var generatedMask = new byte[numBytes];
            generatedMask[numBytes - 1] =
                duoBitArrayUtilities.GetByteMsbMask(duoBitArrayUtilities.GetNumBitsInUseInLastByte(numBits));

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

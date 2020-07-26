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


        /// <summary>
        /// Gets the current bit mask of valid data.
        /// </summary>
        public IReadOnlyDuoBitArray Mask => mask;


        /// <summary>
        /// The number of bits that are in use within the bit array.
        /// </summary>
        public int Length => duoBitArray.Length;

        public int LeftLength => duoBitArray.LeftLength;

        public int RightLength => duoBitArray.RightLength;


        /// <summary>
        /// The total capacity of the bit array, provided at object construction.
        /// </summary>
        public int Capacity => duoBitArray.Capacity;


        /// <summary>
        /// The array's remaining capacity is: (<see cref="Capacity"/> minus <see cref="Length"/>).
        /// </summary>
        public int RemainingCapacity => Capacity - duoBitArrayUtilities.CountSetBits(mask);


        /// <summary>
        /// References the bit identified with the given index within the bit array that underlies both the left
        /// and the right bit arrays.
        /// </summary>
        /// <remarks>
        /// For non-C# developers, this is called an "indexer".  In the setter, the <see langword="value"/> is the
        /// value provided on the right-side of an assignment (e.g., <c>fooBitmap[7] = value</c>.)
        /// </remarks>
        /// <param name="index">The 0-based index of the bit within the bit array.</param>
        public bool this[int index] => duoBitArray[index];


        /// <summary>
        /// Gets the value of the bit identified with the given index within the bit array that underlies both the
        /// left and right bit arrays.
        /// </summary>
        /// <param name="index">The 0-based index of the bit within the bit array.</param>
        public byte GetBit(int index) => duoBitArray.GetBit(index);


        /// <summary>
        /// Sets the value of the bit identified with the given index within the bit array that underlies both the
        /// left and right bit arrays.
        /// </summary>
        /// <param name="index">The 0-based index of the bit within the bit array.</param>
        public void SetBit(int index)
        {
            duoBitArray.SetBit(index);
            mask.SetBit(index);
        }


        /// <summary>
        /// Clears the value of the bit identified with the given index within the bit array that underlies both
        /// the left and right bit arrays.
        /// </summary>
        /// <param name="index">The 0-based index of the bit within the bit array.</param>
        public void ClearBit(int index)
        {
            duoBitArray.ClearBit(index);
            mask.ClearBit(index);
        }


        /// <summary>
        /// Gets the mask of bits that have (yet) to have valid data.
        /// </summary>
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


        /// <summary>
        /// Dequeues <paramref name="numBits"/> consecutive bits starting at the given index.  If padding is
        /// required, the last byte will be padded with <c>(8 - <paramref name="numBits"/> % 8)</c> zeroes in the
        /// least-significant-bits.
        /// </summary>
        /// <remarks>
        /// The above formula is not to suggest that a byte might be padded with 8 zeroes.
        /// </remarks>
        /// <param name="index">The 0-based index of the first bit position to retrieve.</param>
        /// <param name="numBits">The number of bits to return.</param>
        /// <returns>A byte array whose first element's most-significant-bits are filled with data.</returns>
        public IReadOnlyDuoBitArray GetLeftBits(int index, int numBits) => duoBitArray.GetLeftBits(index, numBits);


        /// <summary>
        /// Dequeues <paramref name="numBits"/> consecutive bits starting at the given index.  If padding is
        /// required, the first byte will be padded with <c>(8 - <paramref name="numBits"/> % 8)</c> zeroes in the
        /// most-significant-bits.
        /// </summary>
        /// <remarks>
        /// The above formula is not to suggest that a byte might be padded with 8 zeroes.
        /// </remarks>
        /// <param name="index">The 0-based index of the first bit position to retrieve.</param>
        /// <param name="numBits">The number of bits to return.</param>
        /// <returns>A byte array whose last element's least-significant-bits are filled with data.</returns>
        public IReadOnlyDuoBitArray GetRightBits(int index, int numBits)
            => duoBitArray.GetRightBits(index, numBits);


        /// <summary>
        /// Converts the underlying data structure into an array of bytes; the underlying data structure has both
        /// the "left" and the "right" bit arrays.
        /// </summary>
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

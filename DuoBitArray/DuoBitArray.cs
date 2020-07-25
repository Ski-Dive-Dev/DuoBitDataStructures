using System;

namespace SkiDiveDev.DuoBitDataStructures.BitArrays
{
    /// <summary>
    /// Represents a data structure with two independent bit arrays (left and right).
    /// </summary>
    public class DuoBitArray : IWriteableDuoBitArray
    {
        protected DuoBitArray(int capacity, IDuoBitArrayUtilities duoBitArrayUtilities)
        {
            Capacity = capacity;
            bitArray = new byte[capacity / 8];
            this.duoBitArrayUtilities = duoBitArrayUtilities;
        }

        public static IWriteableDuoBitArray Create(int capacity, IDuoBitArrayUtilities duoBitArrayUtilities)
        {
            return new DuoBitArray(capacity, duoBitArrayUtilities);
        }


        private readonly byte[] bitArray;
        private readonly IDuoBitArrayUtilities duoBitArrayUtilities;


        /// <summary>
        /// References the bit identified with the given index within the bit array that underlies both the left
        /// and the right bit arrays.
        /// </summary>
        /// <remarks>
        /// For non-C# developers, this is called an "indexer".  In the setter, the <see langword="value"/> is the
        /// value provided on the right-side of an assignment (e.g., <c>fooBitmap[7] = value</c>.)
        /// </remarks>
        /// <param name="index">The 0-based index of the bit within the bit array.</param>
        public bool this[int index]
        {
            get
            {
                return (GetBit(index) == 1);
            }

            private set
            {
                if (value == true)
                {
                    SetBit(index);
                }
                else
                {
                    ClearBit(index);
                }
            }
        }


        /// <summary>
        /// Gets the value of the bit identified with the given index within the bit array that underlies both the
        /// left and right bit arrays.
        /// </summary>
        /// <param name="index">The 0-based index of the bit within the bit array.</param>
        public byte GetBit(int index)
        {
            var (byteIndex, bitMaskForBitIndex) = GetByteIndexAndBitMaskForBitIndex(index);
            return (byte)((bitArray[byteIndex] & bitMaskForBitIndex) == 0 ? 0 : 1);
        }


        /// <summary>
        /// Sets the value of the bit identified with the given index within the bit array that underlies both the
        /// left and right bit arrays.
        /// </summary>
        /// <param name="index">The 0-based index of the bit within the bit array.</param>
        public void SetBit(int index)
        {
            var (byteIndex, bitMaskForBitIndex) = GetByteIndexAndBitMaskForBitIndex(index);
            bitArray[byteIndex] = (byte)(bitArray[byteIndex] | bitMaskForBitIndex);
        }


        /// <summary>
        /// Clears the value of the bit identified with the given index within the bit array that underlies both
        /// the left and right bit arrays.
        /// </summary>
        /// <param name="index">The 0-based index of the bit within the bit array.</param>
        public void ClearBit(int index)
        {
            var (byteIndex, bitMaskForBitIndex) = GetByteIndexAndBitMaskForBitIndex(index);
            bitArray[byteIndex] = (byte)(bitArray[byteIndex] & ~bitMaskForBitIndex);
        }

        private (int, byte) GetByteIndexAndBitMaskForBitIndex(int bitArrayIndex)
        {
            var byteIndex = GetByteIndexFromBitIndex(bitArrayIndex);
            var lsb0IntraByteIndex = 7 - bitArrayIndex % 8;             // MSB 0 to LSB 0 indexing
            var bitMaskForBitIndex = duoBitArrayUtilities.GetMaskForBitIndex(lsb0IntraByteIndex);
            return (byteIndex, bitMaskForBitIndex);
        }


        /// <summary>
        /// The total capacity of the bit array, provided at object construction.
        /// </summary>
        public int Capacity { get; private set; }


        /// <summary>
        /// Returns the number of bits within the underlying bit array that are available for use.
        /// </summary>
        public int RemainingCapacity => Capacity - Length;


        /// <summary>
        /// Returns the number of bits within the underlying bit array that have valid data.
        /// </summary>
        public int Length => (LeftLength + RightLength);

        public int LeftLength => leftBitPointer;


        /// <summary>
        /// Points to the <i>next</i> index in the "left" queue that bits will be dequeued from.
        /// </summary>
        private int leftBitPointer;

        private int LeftBytePointer => GetByteIndexFromBitIndex(leftBitPointer);



        public int RightLength => rightBitPointer;


        /// <summary>
        /// Points to the <i>next</i> index in the "right" bit array that bits will be dequeued from.
        /// </summary>
        private int rightBitPointer;

        private int RightBytePointer => GetByteIndexFromBitIndex(rightBitPointer);


        /// <summary>
        /// Gets the sequence of <paramref name="numBits"/> from the "left" bit array, starting at the given index.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Bit numbering is a confusing topic.  Note that conceptually, bit index 0 is the left-most bit.  For the
        /// "left" array, bit index 0 is the most significant bit.  This makes the "left" array 
        /// <see href="https://en.wikipedia.org/wiki/MSB0">MSB 0</see>.
        /// </para><para>
        /// MSB 0 can be extended into referencing the bits within an individual byte -- since we are indexing an
        /// array of consecutive bits within a byte array structure, MSB 0 also applies.  This means that bit index
        /// 0 refers to the MSB of the byte, and bit index 7 refers to the LSB of the byte.
        /// </para><para>
        /// Mathematically, LSB 0 applies, where bit index 7 refers to the MSB of the byte, and bit index 0 refers
        /// to the LSB of the byte (this is due to the powers-of-two technique for converting binary to decimal.)
        /// </para><para>
        /// When this code shifts-bits-left, it is shifting bits from a relative LSB position to a relative MSB
        /// position; similarly for shift-bits-right (more significant bits shift towards LSB positions).  This
        /// comports with standard usage as well as one's mental image of the operation.
        /// </para>
        /// </remarks>
        /// <param name="msb0Index">The MSB 0 based index of the bit in the "left" bit array to return.</param>
        /// <param name="numBits">The number of successive bits from the "left" bit array to return.</param>
        public IReadOnlyDuoBitArray GetLeftBits(int msb0Index, int numBits)  // TODO: Get only masked bits
        {
            var numBytes = duoBitArrayUtilities.GetMinNumBytesToStoreBits(numBits);
            var leftBytePointer = GetByteIndexFromBitIndex(msb0Index);
            var numBitsToShiftSourceBy = duoBitArrayUtilities.GetNumUnusedBitsInByteArray(msb0Index) % 8;

            var destinationBytes = new Byte[numBytes];
            var lastByteIndexInDest = numBytes - 1;

            for (var destByteIndex = 0; /* see 'if' statement */; destByteIndex++)
            {
                var thisUnmodifiedSourceByte = bitArray[leftBytePointer];

                var sourceByteShiftedIntoPositionWithPadding =
                    (byte)(thisUnmodifiedSourceByte << numBitsToShiftSourceBy);

                destinationBytes[destByteIndex] = sourceByteShiftedIntoPositionWithPadding;

                if (destByteIndex == lastByteIndexInDest)
                {
                    break;
                }

                leftBytePointer++;
                var nextUnmodifiedSourceByte = bitArray[leftBytePointer];

                var remainingSourceByteBitsShiftedIntoPositionWithTruncation =
                    (byte)(nextUnmodifiedSourceByte >> (8 - numBitsToShiftSourceBy));

                destinationBytes[destByteIndex] |= remainingSourceByteBitsShiftedIntoPositionWithTruncation;
            }


            var returnBitMap = Create(numBits, duoBitArrayUtilities);
            returnBitMap.SetLeftBits(destinationBytes, 0, 0, numBits);

            return returnBitMap;
        }


        /// <summary>
        /// Gets the sequence of <paramref name="numBits"/> from the "right" bit array, starting at the given
        /// 0-based index.
        /// </summary>
        /// <param name="index">The 0-based index of the bit in the "right" bit array to return.</param>
        /// <param name="numBits">The number of successive bits from the "right" bit array to return.</param>
        public IReadOnlyDuoBitArray GetRightBits(int index, int numBits) => throw new NotImplementedException();



        /// <summary>
        /// Sets <paramref name="numBits"/> bits from <paramref name="sourceBits"/>, starting at source index
        /// <paramref name="sourceArrayBitIndex"/>, to the "left" bit array, starting at the given 0-based index,
        /// <paramref name="destinationBitIndex"/>.
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
            if (numBits > RemainingCapacity)
            {
                throw new ArgumentOutOfRangeException($"There are not enough unused bits in the bit array" +
                    $" to add a value with a size requirement of {numBits} bits.");
            }
            else if (numBits > 8 * sourceBits.Length)
            {
                throw new ArgumentOutOfRangeException($"The provided {nameof(numBits)} is greater" +
                    $" than the number of bits in the given value.");
            }


            CopySourceBitsIntoLeftArray(sourceBits, sourceArrayBitIndex, destinationBitIndex, numBits);

            // FYI: It is only HERE that the dependent LeftBYTEPointer PROPERTY updates:
            leftBitPointer += numBits;

            return this;
        }

        private void CopySourceBitsIntoLeftArray(byte[] sourceBits, int sourceArrayBitIndex,
            int destinationBitIndex, int numBits)
        {

            var leftByteIndex = GetByteIndexFromBitIndex(destinationBitIndex);
            var startingBitIndexOfFirstSourceByte = sourceArrayBitIndex % 8;

            // Number of bit positions to shift the source bits to fill the unused positions in the last
            // destination byte:
            var numBitPositionsToShiftSourceBy = duoBitArrayUtilities
                .GetNumBitsInUseInLastByte(destinationBitIndex) - startingBitIndexOfFirstSourceByte;

            var firstByteIndexInSource = GetByteIndexFromBitIndex(sourceArrayBitIndex);

            var lastSourceBitIndex = sourceArrayBitIndex + numBits - 1;
            var lastByteIndexInSource = GetByteIndexFromBitIndex(lastSourceBitIndex);

            var sourceBitsCopy = GetOnlyRelevantSourceBytes(sourceBits, sourceArrayBitIndex, numBits,
                firstByteIndexInSource, lastByteIndexInSource, lastSourceBitIndex, numBitPositionsToShiftSourceBy);

            for (var sourceByteIndex = 0; 
                sourceByteIndex < sourceBitsCopy.Length + (numBitPositionsToShiftSourceBy > 0 ? 1 : 0);
                sourceByteIndex++, leftByteIndex++)
            {
                var sourceByteShiftedIntoPositionWithBorrowedBits = duoBitArrayUtilities
                    .ShiftBitsInOneByte(sourceBitsCopy, sourceByteIndex, numBitPositionsToShiftSourceBy);

                bitArray[leftByteIndex] |= sourceByteShiftedIntoPositionWithBorrowedBits;
            }

        }


        private byte[] GetOnlyRelevantSourceBytes(byte[] sourceBits, int sourceArrayBitIndex, int numBits,
            int firstByteIndexInSource, int lastByteIndexInSource, int lastSourceBitIndex,
            int numBitPositionsToShiftSourceBy)
        {
            if (numBits == 0)
            {
                return new byte[0];
            }


            // A copy of the source array allows us to mask-out bits before sourceArrayBitIndex and after the last
            // bit (sourceArrayBitIndex + numBits) so we don't accidently "borrow" bits we're not supposed to --
            // without modifying the array passed in as a reference.
            var nominalNumberOfBytesForCopy = GetByteIndexFromBitIndex(sourceArrayBitIndex % 8 + numBits - 1) + 1;
            var numBitsAvailableInLastSourceByte =
                duoBitArrayUtilities.GetNumUnusedBitsInByteArray(sourceArrayBitIndex + numBits);
            var extraByteNeededForBorrowBits = (numBitPositionsToShiftSourceBy > numBitsAvailableInLastSourceByte);
            var arrayCopySize = nominalNumberOfBytesForCopy + (extraByteNeededForBorrowBits ? 1 : 0);
            var sourceBitsCopy = new byte[arrayCopySize];

            for (var i = 1; i < sourceBitsCopy.Length - 1; i++)
            {
                sourceBitsCopy[i] = sourceBits[firstByteIndexInSource + i];
            }

            sourceBitsCopy[0] = GetLsbMaskedSourceByte(sourceBits[firstByteIndexInSource]);

            if (lastByteIndexInSource > 0)
            {
                sourceBitsCopy[sourceBitsCopy.Length - 1] =
                    GetMsbMaskedSourceByte(sourceBits[lastByteIndexInSource]);
            }

            return sourceBitsCopy;



            // These are C# 7 "local functions"; if porting to another language, make them private methods.
            byte GetMsbMaskedSourceByte(byte candidateDestinationByte)
            {
                var numBitsInLastSourceByteToKeep =
                    duoBitArrayUtilities.GetNumBitsInUseInLastByte(lastSourceBitIndex + 1);
                var sourceByteMask = duoBitArrayUtilities.GetByteMsbMask(numBitsInLastSourceByteToKeep);
                var maskedSourceByte = (byte)(candidateDestinationByte & sourceByteMask);
                return maskedSourceByte;
            }

            byte GetLsbMaskedSourceByte(byte candidateDestinationByte)
            {
                var numBitsInFirstSourceByteToKeep = 8 - sourceArrayBitIndex;
                var sourceByteMask = duoBitArrayUtilities.GetByteLsbMask(numBitsInFirstSourceByteToKeep);
                var maskedSourceByte = (byte)(candidateDestinationByte & sourceByteMask);
                return maskedSourceByte;
            }
        }



        public IWriteableDuoBitArray SetRightBits(byte[] sourceBits, int sourceArrayBitIndex,
             int destinationBitIndex, int numBits) => throw new NotImplementedException();


        /// <summary>
        /// Converts the underlying data structure into an array of bytes; the underlying data structure has both
        /// the "left" and the "right" bit arrays.
        /// </summary>
        public byte[] ToByteArray() => bitArray;


        /// <summary>
        /// Returns the 0-based byte index of the byte that contains the bit with the given 0-based bit index.
        /// </summary>
        private int GetByteIndexFromBitIndex(int bitIndex)
            => duoBitArrayUtilities.GetByteIndexFromBitIndex(bitIndex);

    }
}

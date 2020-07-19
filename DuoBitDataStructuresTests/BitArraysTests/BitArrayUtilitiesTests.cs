using System;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using SkiDiveDev.DuoBitDataStructures.BitArrays;

namespace SkiDiveDev.Test.BitArraysTests
{
    [TestFixture]
    class BitArrayUtilitiesTests
    {
        private readonly IBitArrayUtilities bau = BitArrayUtilities.Create();
        private readonly IBitMaskUtilities bmu = BitMaskUtilities.Create();


        [TestCase(0b00111000, 0, 0b11110000, 3, 0b11110011)]
        [TestCase(0b00011100, 3, 0b11100000, 2, 0b11111100)]
        [TestCase(0b00001110, 4, 0b11000000, 1, 0b11111000)]
        [TestCase(0b11111110, 4, 0b11000000, 1, 0b11111000)]
        [TestCase(0b00001110, 4, 0b11111111, 1, 0b11111000)]
        [TestCase(0b11101111, 0, 0b10101111, 3, 0b10101110)]
        [TestCase(0b11101111, 3, 0b10101111, 2, 0b10101111)]
        public void ShouldAlignAndMergeBytes(byte sourceByte, int sourceMsbIndex, byte destinationByte,
            int destinationLsbIndex, byte expected)
        {
            var actual = 
                bau.AlignAndMergeBytes(sourceByte, sourceMsbIndex, destinationByte, destinationLsbIndex, bmu);

            Assert.That(actual, Is.EqualTo(expected));
        }



        [TestCase(new byte[] { 0b00000000, 0b00000000 }, 0, 3, 0b00000000)]
        [TestCase(new byte[] { 0b11001100, 0b10101010 }, 0, 3, 0b00011001)]
        [TestCase(new byte[] { 0b11111111, 0b11111111 }, 0, 3, 0b00011111)]
        [TestCase(new byte[] { 0b00000000, 0b00000000 }, 0, 5, 0b00000000)]
        [TestCase(new byte[] { 0b11001100, 0b10101010 }, 0, 5, 0b00000110)]
        [TestCase(new byte[] { 0b11111111, 0b11111111 }, 0, 5, 0b00000111)]
        [TestCase(new byte[] { 0b00000000, 0b00000000 }, 0, -3, 0b00000000)]
        [TestCase(new byte[] { 0b11001100, 0b10101010 }, 0, -3, 0b01100101)]
        [TestCase(new byte[] { 0b11111111, 0b11111111 }, 0, -3, 0b11111111)]
        [TestCase(new byte[] { 0b00000000, 0b00000000 }, 0, -5, 0b00000000)]
        [TestCase(new byte[] { 0b11001100, 0b10101010 }, 0, -5, 0b10010101)]
        [TestCase(new byte[] { 0b11111111, 0b11111111 }, 0, -5, 0b11111111)]
        [TestCase(new byte[] { 0b00000000, 0b00000000 }, 1, 3, 0b00000000)]
        [TestCase(new byte[] { 0b10101010, 0b11001100 }, 1, 3, 0b01011001)]
        [TestCase(new byte[] { 0b11111111, 0b11111111 }, 1, 3, 0b11111111)]
        [TestCase(new byte[] { 0b00000000, 0b00000000 }, 1, 5, 0b00000000)]
        [TestCase(new byte[] { 0b10101010, 0b11001100 }, 1, 5, 0b01010110)]
        [TestCase(new byte[] { 0b11111111, 0b11111111 }, 1, 5, 0b11111111)]
        [TestCase(new byte[] { 0b00000000, 0b00000000 }, 1, -3, 0b00000000)]
        [TestCase(new byte[] { 0b10101010, 0b11001100 }, 1, -3, 0b01100000)]
        [TestCase(new byte[] { 0b11111111, 0b11111111 }, 1, -3, 0b11111000)]
        [TestCase(new byte[] { 0b00000000, 0b00000000 }, 1, -5, 0b00000000)]
        [TestCase(new byte[] { 0b10101010, 0b11001100 }, 1, -5, 0b10000000)]
        [TestCase(new byte[] { 0b11111111, 0b11111111 }, 1, -5, 0b11100000)]
        public void ShouldShiftBits(byte[] sourceBytes, int sourceByteIndex, int shiftAmount, byte expected)
        {
            var actual = bau.ShiftBitsInOneByte(sourceBytes, sourceByteIndex, shiftAmount);

            Assert.That(actual, Is.EqualTo(expected));
        }



        [TestCase(0, 0)]
        [TestCase(1, 0)]
        [TestCase(2, 0)]
        [TestCase(6, 0)]
        [TestCase(7, 0)]
        [TestCase(8, 1)]
        [TestCase(9, 1)]
        [TestCase(14, 1)]
        [TestCase(15, 1)]
        [TestCase(16, 2)]
        [TestCase(23, 2)]
        [TestCase(24, 3)]
        public void ShouldGetByteIndexFromBitIndex(int bitIndex, int expectedByteIndex)
        {
            var actualByteIndex = bau.GetByteIndexFromBitIndex(bitIndex);

            Assert.That(actualByteIndex, Is.EqualTo(expectedByteIndex));
        }


        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(6, 1)]
        [TestCase(7, 1)]
        [TestCase(8, 1)]
        [TestCase(9, 2)]
        [TestCase(14, 2)]
        [TestCase(15, 2)]
        [TestCase(16, 2)]
        [TestCase(23, 3)]
        [TestCase(24, 3)]
        public void ShouldGetMinNumBytesToStoreBits(int numEnclosedBits, int expectedNumBytes)
        {
            var actualNumBytes = bau.GetMinNumBytesToStoreBits(numEnclosedBits);

            Assert.That(actualNumBytes, Is.EqualTo(expectedNumBytes));
        }


        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(6, 6)]
        [TestCase(7, 7)]
        [TestCase(8, 8)]
        [TestCase(9, 1)]
        [TestCase(14, 6)]
        [TestCase(15, 7)]
        [TestCase(16, 8)]
        [TestCase(23, 7)]
        [TestCase(24, 8)]
        public void ShouldGetNumBitsInUseInLastByte(int numBitsInArray, int expectedNumBits)
        {
            var actualByteIndex = bau.GetNumBitsInUseInLastByte(numBitsInArray);

            Assert.That(actualByteIndex, Is.EqualTo(expectedNumBits));
        }

        public void ShouldThrowExceptionWhenGetNumBitsInUseInLastByteHasInvalidArg()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => bau.GetNumBitsInUseInLastByte(-1));
        }


        [TestCase(0, 8)]
        [TestCase(1, 7)]
        [TestCase(2, 6)]
        [TestCase(6, 2)]
        [TestCase(7, 1)]
        [TestCase(8, 0)]
        [TestCase(9, 7)]
        [TestCase(14, 2)]
        [TestCase(15, 1)]
        [TestCase(16, 0)]
        [TestCase(23, 1)]
        [TestCase(24, 0)]
        public void ShouldGetNumUnusedBitsInByteArray(int totalNumEncodedBits, int expectedNumBits)
        {
            var actualNumBits = bau.GetNumUnusedBitsInByteArray(totalNumEncodedBits);

            Assert.That(actualNumBits, Is.EqualTo(expectedNumBits));
        }


        [TestCase(new byte[] { 0xFF }, new byte[] { 0x00 })]
        [TestCase(new byte[] { 0x89, 0xC7 }, new byte[] { 0x76, 0x38 })]
        [TestCase(new byte[] { 0x00, 0x01, 0x02 }, new byte[] { 0xFF, 0xFE, 0xFD })]
        [TestCase(new byte[] { 0x35, 0x46, 0x12, 0x25 }, new byte[] { 0xCA, 0xB9, 0xED, 0xDA })]
        [TestCase(new byte[] { 0x43, 0x54, 0x6B, 0x9C, 0xAE }, new byte[] { 0xBC, 0xAB, 0x94, 0x63, 0x51 })]
        public void ShouldGetInvertedBytes(byte[] bitArray, byte[] expectedBitArray)
        {
            var actualBitArray = bau.GetInvertedBytes(bitArray);

            Assert.That(actualBitArray, Is.EqualTo(expectedBitArray));
        }
    }
}

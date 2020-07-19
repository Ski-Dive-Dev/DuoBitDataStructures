using System;
using NUnit.Framework;
using SkiDiveDev.DuoBitDataStructures.BitArrays;

namespace SkiDiveDev.Test.BitArraysTests
{
    [TestFixture]
    public class BitMaskUtilitiesTests
    {
        private readonly IBitMaskUtilities bmu = BitMaskUtilities.Create();


        [TestCase(0, 0b00000000)] // 0x00
        [TestCase(1, 0b10000000)] // 0x01
        [TestCase(2, 0b11000000)] // 0x03
        [TestCase(3, 0b11100000)] // 0x07
        [TestCase(4, 0b11110000)] // 0x0F
        [TestCase(5, 0b11111000)] // 0x1F
        [TestCase(6, 0b11111100)] // 0x3F
        [TestCase(7, 0b11111110)] // 0x7F
        [TestCase(8, 0b11111111)] // 0xFF
        public void ShouldGetByteMsbMask(int numBitsToMask, byte expectedMask)
        {
            var actualMask = bmu.GetByteMsbMask(numBitsToMask);

            Assert.That(actualMask, Is.EqualTo(expectedMask));
        }


        [TestCase(-1)]
        [TestCase(9)]
        public void ShouldThrowExceptionWhenGetByteMsbMaskHasInvalidArg(int numBitsToMask)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => bmu.GetByteMsbMask(numBitsToMask));
        }


        [TestCase(0, 0b00000000)] // 0x00
        [TestCase(1, 0b00000001)] // 0x01
        [TestCase(2, 0b00000011)] // 0x03
        [TestCase(3, 0b00000111)] // 0x07
        [TestCase(4, 0b00001111)] // 0x0F
        [TestCase(5, 0b00011111)] // 0x1F
        [TestCase(6, 0b00111111)] // 0x3F
        [TestCase(7, 0b01111111)] // 0x7F
        [TestCase(8, 0b11111111)] // 0xFF
        public void ShouldGetByteLsbMask(int numBitsToMask, byte expectedMask)
        {
            var actualMask = bmu.GetByteLsbMask(numBitsToMask);

            Assert.That(actualMask, Is.EqualTo(expectedMask));
        }


        [TestCase(-1)]
        [TestCase(9)]
        public void ShouldThrowExceptionWhenGetByteLsbMaskHasInvalidArg(int numBitsToMask)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => bmu.GetByteLsbMask(numBitsToMask));
        }


        [TestCase(0, 0b00000001)] // 0x01
        [TestCase(1, 0b00000010)] // 0x02
        [TestCase(2, 0b00000100)] // 0x04
        [TestCase(3, 0b00001000)] // 0x08
        [TestCase(4, 0b00010000)] // 0x10
        [TestCase(5, 0b00100000)] // 0x20
        [TestCase(6, 0b01000000)] // 0x40
        [TestCase(7, 0b10000000)] // 0x80
        public void ShouldGetMaskForBitIndex(int bitIndex, byte expectedMask)
        {
            var actualMask = bmu.GetMaskForBitIndex(bitIndex);

            Assert.That(actualMask, Is.EqualTo(expectedMask));
        }


        [TestCase(-1)]
        [TestCase(9)]
        public void ShouldThrowExceptionWhenGetMaskForBitIndexHasInvalidArg(int bitIndex)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => bmu.GetMaskForBitIndex(bitIndex));
        }
    }
}

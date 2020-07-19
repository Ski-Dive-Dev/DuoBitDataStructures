using NUnit.Framework;
using SkiDiveDev.DuoBitDataStructures.BitArrays;

namespace SkiDiveDev.Test.BitArraysTests
{
    [TestFixture]
    public class DuoBitArrayTests
    {
        public DuoBitArrayTests()
        {
            bau = new DuoBitArrayUtilities();
            dba = DuoBitArray.Create(25, bau);
        }

        private IWriteableDuoBitArray dba;
        private readonly DuoBitArrayUtilities bau;


        [SetUp]
        public void SetUp()
        {
            dba = DuoBitArray.Create(25, bau);
        }

        [TestCase(0, new byte[] { 0xFF, 0x00, 0x00 }, new byte[] { 0xFF, 0x00, 0x00 })]
        [TestCase(4, new byte[] { 0xFF, 0x00, 0x00 }, new byte[] { 0xFF, 0x00, 0x00 })]
        [TestCase(7, new byte[] { 0xFF, 0x00, 0x00 }, new byte[] { 0xFF, 0x00, 0x00 })]
        [TestCase(0, new byte[] { 0x00, 0x00, 0x00 }, new byte[] { 0x80, 0x00, 0x00 })]
        [TestCase(4, new byte[] { 0x00, 0x00, 0x00 }, new byte[] { 0x08, 0x00, 0x00 })]
        [TestCase(7, new byte[] { 0x00, 0x00, 0x00 }, new byte[] { 0x01, 0x00, 0x00 })]
        [TestCase(0, new byte[] { 0x53, 0x00, 0x00 }, new byte[] { 0xD3, 0x00, 0x00 })]
        [TestCase(4, new byte[] { 0x53, 0x00, 0x00 }, new byte[] { 0x5B, 0x00, 0x00 })]
        [TestCase(7, new byte[] { 0x53, 0x00, 0x00 }, new byte[] { 0x53, 0x00, 0x00 })]
        [TestCase(0, new byte[] { 0x53, 0x72, 0x00 }, new byte[] { 0xD3, 0x72, 0x00 })]
        [TestCase(7, new byte[] { 0x53, 0x72, 0x00 }, new byte[] { 0x53, 0x72, 0x00 })]
        [TestCase(8, new byte[] { 0x53, 0x72, 0x00 }, new byte[] { 0x53, 0xF2, 0x00 })]
        [TestCase(15, new byte[] { 0x53, 0x72, 0x00 }, new byte[] { 0x53, 0x73, 0x00 })]
        public void ShouldSetBit(int index, byte[] leftBits, byte[] expectedResult)
        {
            dba.SetLeftBits(leftBits, 0, 0, leftBits.Length * 8);
            dba.SetBit(index);
            var actualResult = dba.ToByteArray();

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [TestCase(0, new byte[] { 0xFF, 0x00, 0x00 }, new byte[] { 0x7F, 0x00, 0x00 })]
        [TestCase(4, new byte[] { 0xFF, 0x00, 0x00 }, new byte[] { 0xF7, 0x00, 0x00 })]
        [TestCase(7, new byte[] { 0xFF, 0x00, 0x00 }, new byte[] { 0xFE, 0x00, 0x00 })]
        [TestCase(0, new byte[] { 0x00, 0x00, 0x00 }, new byte[] { 0x00, 0x00, 0x00 })]
        [TestCase(4, new byte[] { 0x00, 0x00, 0x00 }, new byte[] { 0x00, 0x00, 0x00 })]
        [TestCase(7, new byte[] { 0x00, 0x00, 0x00 }, new byte[] { 0x00, 0x00, 0x00 })]
        [TestCase(0, new byte[] { 0x53, 0x00, 0x00 }, new byte[] { 0x53, 0x00, 0x00 })]
        [TestCase(4, new byte[] { 0x53, 0x00, 0x00 }, new byte[] { 0x53, 0x00, 0x00 })]
        [TestCase(7, new byte[] { 0x53, 0x00, 0x00 }, new byte[] { 0x52, 0x00, 0x00 })]
        [TestCase(0, new byte[] { 0x53, 0x72, 0x00 }, new byte[] { 0x53, 0x72, 0x00 })]
        [TestCase(7, new byte[] { 0x53, 0x72, 0x00 }, new byte[] { 0x52, 0x72, 0x00 })]
        [TestCase(8, new byte[] { 0x53, 0x72, 0x00 }, new byte[] { 0x53, 0x72, 0x00 })]
        [TestCase(15, new byte[] { 0x53, 0x72, 0x00 }, new byte[] { 0x53, 0x72, 0x00 })]
        public void ShouldClearBit(int index, byte[] leftBits, byte[] expectedResult)
        {
            dba.SetLeftBits(leftBits, 0, 0, leftBits.Length * 8);
            dba.ClearBit(index);
            var actualResult = dba.ToByteArray();

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [TestCase(0, new byte[] { 0xFF, 0x00, 0x00 }, 1)]
        [TestCase(4, new byte[] { 0xFF, 0x00, 0x00 }, 1)]
        [TestCase(7, new byte[] { 0xFF, 0x00, 0x00 }, 1)]
        [TestCase(0, new byte[] { 0x00, 0x00, 0x00 }, 0)]
        [TestCase(4, new byte[] { 0x00, 0x00, 0x00 }, 0)]
        [TestCase(7, new byte[] { 0x00, 0x00, 0x00 }, 0)]
        [TestCase(0, new byte[] { 0x53, 0x00, 0x00 }, 0)]
        [TestCase(4, new byte[] { 0x53, 0x00, 0x00 }, 0)]
        [TestCase(7, new byte[] { 0x53, 0x00, 0x00 }, 1)]
        [TestCase(0, new byte[] { 0x53, 0x72, 0x00 }, 0)]
        [TestCase(7, new byte[] { 0x53, 0x72, 0x00 }, 1)]
        [TestCase(8, new byte[] { 0x53, 0x72, 0x00 }, 0)]
        [TestCase(15, new byte[] { 0x53, 0x72, 0x00 },0)]
        public void ShouldGetBit(int index, byte[] leftBits, byte expectedResult)
        {
            dba.SetLeftBits(leftBits, 0, 0, leftBits.Length * 8);
            var actualResult = dba.GetBit(index);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }



        [TestCase(new byte[] { 0xFF }, 0, 0, 8, new byte[] { 0xFF, 0x00, 0x00 })]
        [TestCase(new byte[] { 0x00 }, 0, 0, 8, new byte[] { 0x00, 0x00, 0x00 })]
        [TestCase(new byte[] { 0x53 }, 0, 0, 8, new byte[] { 0x53, 0x00, 0x00 })]
        [TestCase(new byte[] { 0x53, 0x72 }, 0, 0, 8, new byte[] { 0x53, 0x00, 0x00 })]
        [TestCase(new byte[] { 0x53, 0x72 }, 0, 0, 9, new byte[] { 0x53, 0x00, 0x00 })]
        [TestCase(new byte[] { 0x53, 0x72 }, 0, 0, 10, new byte[] { 0x53, 0x40, 0x00 })]
        [TestCase(new byte[] { 0x53, 0x72 }, 0, 0, 11, new byte[] { 0x53, 0x60, 0x00 })]
        [TestCase(new byte[] { 0xFF }, 3, 0, 5, new byte[] { 0xF8, 0x00, 0x00 })]
        [TestCase(new byte[] { 0x00 }, 3, 0, 5, new byte[] { 0x00, 0x00, 0x00 })]
        [TestCase(new byte[] { 0x53 }, 3, 0, 5, new byte[] { 0x98, 0x00, 0x00 })]
        [TestCase(new byte[] { 0x53, 0x72 }, 3, 0, 8, new byte[] { 0x9B, 0x00, 0x00 })]
        [TestCase(new byte[] { 0x53, 0x72 }, 3, 0, 9, new byte[] { 0x9B, 0x80, 0x00 })]
        [TestCase(new byte[] { 0x53, 0x72 }, 3, 0, 10, new byte[] { 0x9B, 0x80, 0x00 })]
        [TestCase(new byte[] { 0x53, 0x72 }, 3, 0, 11, new byte[] { 0x9B, 0x80, 0x00 })]

        [TestCase(new byte[] { 0xFF }, 0, 4, 8, new byte[] { 0x0F, 0xF0, 0x00 })]
        [TestCase(new byte[] { 0x00 }, 0, 4, 8, new byte[] { 0x00, 0x00, 0x00 })]
        [TestCase(new byte[] { 0x53 }, 0, 4, 8, new byte[] { 0x05, 0x30, 0x00 })]
        [TestCase(new byte[] { 0x53, 0x72 }, 0, 4, 8, new byte[] { 0x05, 0x30, 0x00 })]
        [TestCase(new byte[] { 0x53, 0x72 }, 0, 4, 9, new byte[] { 0x05, 0x30, 0x00 })]
        [TestCase(new byte[] { 0x53, 0x72 }, 0, 4, 10, new byte[] { 0x05, 0x34, 0x00 })]
        [TestCase(new byte[] { 0x53, 0x72 }, 0, 4, 11, new byte[] { 0x05, 0x36, 0x00 })]
        [TestCase(new byte[] { 0xFF }, 3, 4, 5, new byte[] { 0x0F, 0x80, 0x00 })]
        [TestCase(new byte[] { 0x00 }, 3, 4, 5, new byte[] { 0x00, 0x00, 0x00 })]
        [TestCase(new byte[] { 0x53 }, 3, 4, 5, new byte[] { 0x09, 0x80, 0x00 })]
        [TestCase(new byte[] { 0x53, 0x72 }, 3, 4, 8, new byte[] { 0x09, 0xB0, 0x00 })]
        [TestCase(new byte[] { 0x53, 0x72 }, 3, 4, 9, new byte[] { 0x09, 0xB8, 0x00 })]
        [TestCase(new byte[] { 0x53, 0x72 }, 3, 4, 10, new byte[] { 0x09, 0xB8, 0x00 })]
        [TestCase(new byte[] { 0x53, 0x72 }, 3, 4, 11, new byte[] { 0x09, 0xB8, 0x00 })]
        public void ShouldSetLeftBits(byte[] sourceBits, int sourceArrayBitIndex, int destinationBitIndex,
            int numBits, byte[] expectedResult)
        {
            dba.SetLeftBits(sourceBits, sourceArrayBitIndex, destinationBitIndex, numBits);
            var actualResult = dba.ToByteArray();

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }
}

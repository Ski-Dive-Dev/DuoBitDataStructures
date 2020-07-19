using System;

namespace SkiDiveCode.DuoBitDataStructures.BitArrays
{
    interface IEndianness
    {
        byte[] FixEndianAndConvertRightToLeft(byte[] littleEndianBytes,
            bool rightToLeft = false);

        ulong FixEndianAndConvertRightToLeft(ulong littleEndianEncodedValue,
            bool rightToLeft = false);
    }



    public class Endianness : IEndianness
    {
        /// <summary>
        /// Given an array of little endian bytes, and a request to arrange those bytes
        /// endian), reaarrange them to suit the endianness of the hardware this code is running in.
        /// </summary>
        /// <param name="littleEndianBytes"></param>
        /// <param name="rightToLeft"></param>
        /// <returns></returns>
        public byte[] FixEndianAndConvertRightToLeft(byte[] littleEndianBytes,
            bool rightToLeft = false)
        {
            var endiannessOfThisArchitecture = BitConverter.IsLittleEndian;
            if (endiannessOfThisArchitecture == rightToLeft)
            {
                var bigEndianBytes = littleEndianBytes;
                Array.Reverse(bigEndianBytes);
                return bigEndianBytes;
            }
            else
            {
                return littleEndianBytes;
            }
        }


        /// <summary>
        /// Given a <see langword="ulong"/> argument, that was constructed from any array of bytes that were
        /// little-endian, correct the byte ordering per the hardware endianness that this code is running in.
        /// </summary>
        /// <param name="littleEndianEncodedValue">A value that was constructed from bytes that were ordered
        /// least-significant-byte to most-significant-byte.</param>
        /// <param name="rightToLeft">Indicates whether the original order is desired (left-to-right) or if the
        /// original order was reversed from what is desired.  This is irrespective of </param>
        /// <returns></returns>
        public ulong FixEndianAndConvertRightToLeft(ulong littleEndianEncodedValue,
            bool rightToLeft = false)
        {
            var endiannessOfThisArchitecture = BitConverter.IsLittleEndian;
            if (endiannessOfThisArchitecture == rightToLeft)
            {
                var littleEndianBytes = BitConverter.GetBytes(littleEndianEncodedValue);
                Array.Reverse(littleEndianBytes);
                return BitConverter.ToUInt64(littleEndianBytes, 0);
            }
            else
            {
                return littleEndianEncodedValue;
            }
        }
    }
}

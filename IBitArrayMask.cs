using System;
using System.Collections.Generic;
using System.Text;

namespace SkiDiveCode.DuoBitDataStructures.BitArrays
{
    /// <summary>
    /// Represents an object that has both a bitmap and a mask which indicate which bits of that bitmap have valid
    /// data.  The bitmap is read/write, meaning data can both be enqueued and dequeued.
    /// </summary>
    public interface IBitArrayMask
    {
        /// <summary>
        /// Gets the current bit mask of valid data.
        /// </summary>
        IReadOnlyDuoBitArray Mask { get; }

        /// <summary>
        /// Gets the mask of bits that have (yet) to have valid data.
        /// </summary>
        IReadOnlyDuoBitArray GetMaskOfUnusedBits();
    }
}

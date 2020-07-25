namespace SkiDiveDev.DuoBitDataStructures.BitArrays
{
    public interface IBitCollection
    {
        /// <summary>
        /// The number of bits that are in use within the bit structure.
        /// </summary>
        int Length { get; }


        /// <summary>
        /// The total capacity of the bit structure, provided at object construction.
        /// </summary>
        int Capacity { get; }


        /// <summary>
        /// The remaining capacity is: (<see cref="Capacity"/> minus <see cref="Length"/>).
        /// </summary>
        int RemainingCapacity { get; }


        /// <summary>
        /// Converts the underlying data structure into an array of bytes; the underlying data structure has both
        /// the "left" and the "right" bit arrays.
        /// </summary>
        byte[] ToByteArray();
    }
}

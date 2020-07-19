namespace SkiDiveDev.DuoBitDataStructures.BitQueues.MonoQueue
{
    /// <summary>
    /// An interface for specifying how different values can be enqueued into a bit queue.
    /// </summary>
    public interface IWriteableBitQueue
    {
        IWriteableBitQueue Enqueue(byte valueToSet, int numSignificantBits);
        IWriteableBitQueue Enqueue(byte[] valueToSet, int numSignificantBits);
        IWriteableBitQueue Enqueue(int valueToSet, int numSignificantBits);
        IWriteableBitQueue Enqueue(long valueToSet, int numSignificantBits);
        IWriteableBitQueue Enqueue(long[] valueToSet, int numSignificantBits);
        IWriteableBitQueue Enqueue(ulong valueToSet, int numSignificantBits);
        IWriteableBitQueue Enqueue(ulong[] valueToSet, int numSignificantBits);
    }
}
namespace Patterns.StateSnapshot.Domain
{
    public interface IStateSnapshotable<T>
    {
        T TakeSnapshot();
        void LoadSnapshot(T snapshot);
    }
}
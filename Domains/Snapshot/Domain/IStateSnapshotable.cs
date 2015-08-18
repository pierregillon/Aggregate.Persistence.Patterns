namespace Domains.Snapshot.Domain
{
    public interface IStateSnapshotable<T>
    {
        T TakeSnapshot();
        void LoadFromSnapshot(T orderState);
    }
}
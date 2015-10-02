using System.Collections.Generic;

namespace Patterns.Snapshot.Domain
{
    public static class StateSnapshotableExtensions
    {
        public static IEnumerable<TState> TakeSnapshot<TModel, TState>(this IList<TModel> models)
            where TModel : IStateSnapshotable<TState>
        {
            foreach (var model in models) {
                var state = model.TakeSnapshot();
                yield return state;
            }
        }

        public static void LoadFromSnapshot<TModel, TState>(this IList<TModel> models, IEnumerable<TState> states)
            where TModel : IStateSnapshotable<TState>, new()
        {
            foreach (var state in states) {
                var model = new TModel();
                model.LoadFromSnapshot(state);
                models.Add(model);
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace Domains.Snapshot.Domain
{
    public static class StateSnapshotableExtensions
    {
        public static IEnumerable<TState> TakeSnapshot<TModel, TState>(this IList<TModel> models)
            where TModel : IStateSnapshotable<TState>
        {
            return models.Select(model => model.TakeSnapshot());
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
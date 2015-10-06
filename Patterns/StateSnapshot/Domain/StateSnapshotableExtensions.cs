using System;
using System.Collections.Generic;

namespace Patterns.StateSnapshot.Domain
{
    public static class StateSnapshotableExtensions
    {
        public static IEnumerable<TState> TakeSnapshot<TModel, TState>(this IList<TModel> models, Action<TState> complementaryAction = null)
            where TModel : IStateSnapshotable<TState>
        {
            foreach (var model in models) {
                var state = model.TakeSnapshot();
                if (complementaryAction != null) {
                    complementaryAction(state);
                }
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
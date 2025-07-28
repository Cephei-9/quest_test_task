using System;
using System.Collections.Generic;
using Naninovel;
using UniRx;

namespace Inventory
{
    [InitializeAtRuntime]
    public class InventoryService : IStatefulService<GameStateMap>
    {
        public readonly ReactiveCollection<string> ItemsCollection = new();
        public readonly ReactiveProperty<bool> IsLoading = new();

        public UniTask InitializeServiceAsync()
        {
            return UniTask.CompletedTask;
        }

        public void ResetService()
        {
            ItemsCollection.Clear();
        }

        public void DestroyService()
        {
        }

        public void AddItem(string itemId)
        {
            if (string.IsNullOrWhiteSpace(itemId))
                return;

            if (ItemsCollection.Contains(itemId))
                return;

            ItemsCollection.Add(itemId);
        }

        public void RemoveItem(string itemId)
        {
            if (string.IsNullOrWhiteSpace(itemId))
                return;

            if (!ItemsCollection.Contains(itemId))
                return;

            ItemsCollection.Remove(itemId);
        }

        public void SaveServiceState(GameStateMap stateMap)
        {
            InventoryState state = new()
            {
                ItemIds = new List<string>(ItemsCollection)
            };
            stateMap.SetState(state);
        }

        public UniTask LoadServiceStateAsync(GameStateMap stateMap)
        {
            IsLoading.Value = true;
            
            ItemsCollection.Clear();
            InventoryState state = stateMap.GetState<InventoryState>();

            if (state is null || state.ItemIds is null)
                return UniTask.CompletedTask;

            foreach (string itemId in state.ItemIds)
            {
                ItemsCollection.Add(itemId);
            }

            IsLoading.Value = false;
            return UniTask.CompletedTask;
        }

        [Serializable]
        private class InventoryState
        {
            public List<string> ItemIds;
        }
    }
}
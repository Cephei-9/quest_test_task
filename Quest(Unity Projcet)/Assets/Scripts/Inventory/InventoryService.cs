using System;
using System.Collections.Generic;
using System.Globalization;
using Naninovel;
using UniRx;
using UnityEngine;

namespace Inventory
{
    // Класс реализующий логику инвентаря по строковым ключам. Он предоставляет интерфейс инвентаря, и работает с
    // сохранениями
    
    [InitializeAtRuntime]
    public class InventoryService : IStatefulService<GameStateMap>
    {
        private const string ItemVariablePrefix = "Has";
        
        public readonly ReactiveCollection<string> ItemsCollection = new();
        public readonly ReactiveProperty<bool> IsLoading = new();
        
        private readonly ICustomVariableManager _variableManager;

        public InventoryService(ICustomVariableManager variableManager)
        {
            _variableManager = variableManager;
        }

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
            SetValueToNaniVariable(itemId, true);
        }

        public void RemoveItem(string itemId)
        {
            if (string.IsNullOrWhiteSpace(itemId))
                return;

            if (!ItemsCollection.Contains(itemId))
                return;

            ItemsCollection.Remove(itemId);
            SetValueToNaniVariable(itemId, false);
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

        private void SetValueToNaniVariable(string itemId, bool value)
        {
            string varName = $"{ItemVariablePrefix}{itemId}";
            _variableManager.SetVariableValue(varName, value.ToString());
            
            string variableValue = _variableManager.GetVariableValue(varName);
            Debug.LogError($"var name: {varName}; Var value: {variableValue}");
        }

        [Serializable]
        private class InventoryState
        {
            public List<string> ItemIds;
        }
    }
}
using System;
using Naninovel;
using UniRx;

namespace Locations
{
    // В игре собственный UI разделен на локации и это сервис который работает с данными для этой игровой системы.
    // Он хранит данные о текущей локации и сохраняет их
    
    [InitializeAtRuntime]
    public class LocationService : IStatefulService<GameStateMap>
    {
        private const string DefaultLocation = "";

        public readonly ReactiveProperty<string> CurrentLocation = new(DefaultLocation);

        public UniTask InitializeServiceAsync()
        {
            return UniTask.CompletedTask;
        }

        public void ResetService()
        {
            CurrentLocation.Value = DefaultLocation;
        }

        public void DestroyService() { }

        public void ChangeLocation(string locationId)
        {
            if (!string.IsNullOrWhiteSpace(locationId) && locationId != CurrentLocation.Value)
                CurrentLocation.Value = locationId;
        }

        public void HideLocation()
        {
            CurrentLocation.Value = string.Empty;
        }

        public void SaveServiceState(GameStateMap stateMap)
        {
            LocationState state = new()
            {
                LocationId = CurrentLocation.Value
            };
            
            stateMap.SetState(state);
        }

        public UniTask LoadServiceStateAsync(GameStateMap stateMap)
        {
            CurrentLocation.Value = string.Empty;
            LocationState state = stateMap.GetState<LocationState>();
            
            if (state is null || string.IsNullOrEmpty(state.LocationId))
            {
                CurrentLocation.Value = DefaultLocation;
                return UniTask.CompletedTask;
            }

            CurrentLocation.Value =  state.LocationId;
            return UniTask.CompletedTask;
        }

        [Serializable]
        private class LocationState
        {
            public string LocationId;
        }
    }
}
using System;
using Naninovel;
using UniRx;

namespace Locations
{
    [InitializeAtRuntime]
    public class LocationService : IStatefulService<GameStateMap>
    {
        private const string DefaultLocation = "";

        public ReactiveProperty<string> CurrentLocation { get; } = new(DefaultLocation);

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
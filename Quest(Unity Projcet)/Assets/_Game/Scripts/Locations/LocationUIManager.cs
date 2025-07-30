using System.Collections.Generic;
using Naninovel;
using UniRx;
using UnityEngine;

namespace Locations
{
    // Класс который работает с всеми объектами локаций, и включает и выключает их смотря на данные из сервиса
    
    public class LocationUIManager : MonoBehaviour
    {
        [SerializeField] private List<Location> _locations = new();

        private LocationUI _current;
        private readonly Dictionary<string, LocationUI> _map = new();

        private void Awake()
        {
            foreach (Location location in _locations)
            {
                if (location.UIObject == null || string.IsNullOrWhiteSpace(location.LocationId))
                    continue;

                if (!_map.ContainsKey(location.LocationId)) 
                    _map.Add(location.LocationId, location.UIObject);
                
                location.UIObject.Hide().Forget();
            }
        }

        private void Start()
        {
            LocationService service = Engine.GetService<LocationService>();
            service.CurrentLocation.Subscribe(OnLocationChanged).AddTo(this);

            string initialId = service.CurrentLocation.Value;
            OnLocationChanged(initialId);
        }

        private void OnLocationChanged(string id)
        {
            if (_current != null) 
                _current.Hide().Forget();

            if (_map.TryGetValue(id, out LocationUI ui))
            {
                ui.Show().Forget();
                _current = ui;
            }
        }

        [System.Serializable]
        public class Location
        {
            public string LocationId;
            public LocationUI UIObject;
        }
    }
}
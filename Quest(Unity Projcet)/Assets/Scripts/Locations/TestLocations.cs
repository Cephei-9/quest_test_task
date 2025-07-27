using Naninovel;
using UniRx;
using UnityEngine;

namespace Locations
{
    public class TestLocations : MonoBehaviour
    {
        private void Start()
        {
            LocationService locationService = Engine.GetService<LocationService>();
            locationService.CurrentLocation.Subscribe(locationId => Debug.Log("New location Id: " + locationId));
        }
    }
}
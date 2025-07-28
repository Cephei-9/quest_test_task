using Naninovel;
using UnityEngine;

namespace Locations
{
    public class UIInitialization : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.SetActive(false);
            Engine.OnInitializationFinished += () => gameObject.SetActive(true);
        }
    }
}
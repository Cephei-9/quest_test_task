using System;
using Naninovel;
using UnityEngine;

namespace Locations
{
    public class HideUIElementWhenDialogue : MonoBehaviour
    {
        private ITextPrinterManager _printersManager;

        private void Awake()
        {
            _printersManager = Engine.GetService<ITextPrinterManager>();

            foreach (ITextPrinterActor printerActor in _printersManager.GetAllActors())
            {
                
            }
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }
    }
    
    
}
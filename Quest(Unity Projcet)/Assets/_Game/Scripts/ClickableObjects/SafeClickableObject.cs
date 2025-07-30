using System;
using Locations.NaniNames;
using NaniNames;
using Naninovel;
using TicTacToeGame;
using UI;
using UnityEngine;

namespace ClickableObjects
{
    public class SafeClickableObject : MonoBehaviour
    {
        [SerializeField] private ClickableObject _clickableObject;
        
        private ICustomVariableManager _variableManager;
        private IScriptPlayer _scriptPlayer;

        private bool IsSafeOpened
        {
            get
            {
                string isSafeOpened = _variableManager.GetVariableValue(NaniVariablesNames.SafeOpened);
                return isSafeOpened == true.ToString();
            }
        }

        private void Awake()
        {
            _variableManager = Engine.GetService<ICustomVariableManager>();
            _scriptPlayer = Engine.GetService<IScriptPlayer>();

            _clickableObject.OnClickEvent += OnClickHandler;
        }

        private void OnEnable()
        {
            _clickableObject.SetInteractable(!IsSafeOpened);
        }

        private void OnClickHandler()
        {
            _scriptPlayer.PreloadAndPlayAsync(NaniScriptsNames.ClickToSafeScript);
            _clickableObject.SetInteractable(!IsSafeOpened);
        }
    }
}
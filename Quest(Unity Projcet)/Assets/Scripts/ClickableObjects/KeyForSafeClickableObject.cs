using System;
using Locations.NaniNames;
using Naninovel;
using TicTacToeGame;
using UI;
using UnityEngine;

namespace ClickableObjects
{
    public class KeyForSafeClickableObject : MonoBehaviour
    {
        [SerializeField] private ClickableObject _clickableObject;
        
        private ICustomVariableManager _variableManager;
        private IScriptPlayer _scriptPlayer;
        private TicTacToeService _ticTacToeService;

        private bool IsKeyTaken
        {
            get
            {
                string keyIsTakenValue = _variableManager.GetVariableValue(NaniVariablesNames.KeyForSafeTaken);
                return keyIsTakenValue == true.ToString();
            }
        }

        private void Awake()
        {
            _variableManager = Engine.GetService<ICustomVariableManager>();
            _scriptPlayer = Engine.GetService<IScriptPlayer>();
            _ticTacToeService = Engine.GetService<TicTacToeService>();

            _clickableObject.OnClickEvent += OnClickHandler;
            _ticTacToeService.OnGameFinishedEvent += OnGameFinishedHandler;
        }

        private void OnEnable()
        {
            _clickableObject.SetVisible(!IsKeyTaken);
        }

        private void OnClickHandler()
        {
            if (!IsKeyTaken) 
                _ticTacToeService.StartGame();
        }

        private void OnGameFinishedHandler(TicTacToeGameResult gameResult)
        {
            if (gameResult == TicTacToeGameResult.PlayerWin)
            {
                _scriptPlayer.PreloadAndPlayAsync(NaniScriptsNames.CollectKeyForSafeScript);
                _clickableObject.SetVisible(false);
            }
        }
    }
}
using System;
using System.Threading;
using Naninovel;
using SoundInfrastructure;
using TicTacToeGame;
using UnityEngine;
using UnityEngine.UI;

namespace Locations.TicTarToeUI
{
    // Попап показывающий результат игры в крестики нолики. Он блокирует инпут и ожидает клика игрока.
    // Воспроизводит звук при победе
    
    public class TicTacToeResultPopup : MonoBehaviour
    {
        [SerializeField] private GameObject _playerWinText;
        [SerializeField] private GameObject _aiWinText;
        [SerializeField] private GameObject _drawText;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _backgroundButton;
        [SerializeField] private AudioClip _winSound;

        private SoundService _soundService;
        private UniTaskCompletionSource _completionSource;

        private void Awake()
        {
            _soundService = Engine.GetService<SoundService>();
            
            _confirmButton.onClick.AddListener(OnConfirmClicked);
            _backgroundButton.onClick.AddListener(OnConfirmClicked);
        }

        public async UniTask ShowGameResultAsync(TicTacToeGameResult result, CancellationToken token = default)
        {
            SetActiveText(result);
            gameObject.SetActive(true);

            if (result == TicTacToeGameResult.PlayerWin)
                _soundService.PlaySfx(_winSound);
            
            _completionSource = new UniTaskCompletionSource();

            await _completionSource.Task;

            gameObject.SetActive(false);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void SetActiveText(TicTacToeGameResult result)
        {
            _playerWinText.SetActive(false);
            _aiWinText.SetActive(false);
            _drawText.SetActive(false);

            if (result == TicTacToeGameResult.PlayerWin)
                _playerWinText.SetActive(true);
            else if (result == TicTacToeGameResult.AIWin)
                _aiWinText.SetActive(true);
            else if (result == TicTacToeGameResult.Draw)
                _drawText.SetActive(true);
        }

        private void OnConfirmClicked()
        {
            _completionSource?.TrySetResult();
        }
    }
}
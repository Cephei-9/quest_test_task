using System;
using Naninovel;
using TicTacToeGame;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Locations.TicTarToeUI
{
    // Класс отображающий игру на экране. Он работает с отдельными ячейками и с сервисом игры. По-хорошему
    // его бы разделить на условный контроллер, который бы работал с сервисом, и на view которая бы реализовывала UI
    // тонкости, но я решил в этом проекте не имплементировать MVC идею 
    
    public class TicTacToeUI : MonoBehaviour
    {
        [SerializeField] private GameObject _uiContainer;
        [SerializeField] private Button _exitButton;
        [SerializeField] private TicTacToeCell[] _cells;
        [SerializeField] private TicTacToeResultPopup _resultPopup;

        private TicTacToeService _service;

        private void Awake()
        {
            _exitButton.onClick.AddListener(HandleExitClicked);
            InitCells();
        }

        private void Start()
        {
            _service = Engine.GetService<TicTacToeService>();

            _service.OnBoardChangedEvent += UpdateBoardUI;
            _service.OnGameFinishedEvent += HandleGameFinished;

            _service.IsGameRunning
                .Where(nextValue => nextValue)
                .Subscribe(_ => OnStartGameHandler())
                .AddTo(this);
        }

        private void OnDestroy()
        {
            _service.OnBoardChangedEvent -= UpdateBoardUI;
            _service.OnGameFinishedEvent -= HandleGameFinished;
        }

        private void OnStartGameHandler()
        {
            if (!_service.IsGameRunning.Value)
                return;
            
            ResetBoardUI();
            _resultPopup.Hide();
            _uiContainer.SetActive(true);
        }

        private void InitCells()
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i].OnCellClicked += HandleCellClicked;
            }
        }

        private void HandleCellClicked(Vector2Int position)
        {
            _service.MakeMove(position.x, position.y);
        }

        private void UpdateBoardUI()
        {
            CellState[,] board = _service.GameBoard;

            for (int i = 0; i < _cells.Length; i++)
            {
                Vector2Int pos = _cells[i].CellPosition;
                _cells[i].SetState(board[pos.x, pos.y]);
            }
        }

        private void ResetBoardUI()
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i].SetState(CellState.Empty);
            }
        }

        private async void HandleGameFinished(TicTacToeGameResult result)
        {
            await _resultPopup.ShowGameResultAsync(result, destroyCancellationToken);
            
            if(result == TicTacToeGameResult.PlayerWin)
                _uiContainer.SetActive(false);
            else
                _service.StartGame();
        }

        private void HandleExitClicked()
        {
            ResetBoardUI();
            _service.ForceFinishGame();
            _uiContainer.SetActive(false);
        }
    }
}
using System;
using Naninovel;
using UniRx;
using UnityEngine;

namespace TicTacToeGame
{
    [InitializeAtRuntime]
    public class TicTacToeService : IEngineService
    {
        public readonly ReactiveProperty<bool> IsGameRunning = new();

        public event Action OnStartGameEvent;
        public event Action OnBoardChangedEvent;
        public event Action<TicTacToeGameResult> OnGameFinishedEvent;
        
        public CellState[,] GameBoard { get; } = new CellState[3, 3];

        public TicTacToeService()
        {
            Debug.LogError($"Create Service");
        }

        public UniTask InitializeServiceAsync()
        {
            return UniTask.CompletedTask;
        }

        public void ResetService()
        {
            ForceFinishGame();
        }

        public void DestroyService() { }

        public void StartGame()
        {
            IsGameRunning.Value = true;
            OnStartGameEvent?.Invoke();
        }

        public void MakeMove(int row, int column)
        {
            if(IsGameRunning.Value == false)
                return;

            if(GameBoard[row, column] != CellState.Empty)
                return;

            Debug.Log($"[TicTacToeService] Player move at ({row},{column})");
            
            GameBoard[row, column] = CellState.X;
            
            if (TryFinishGame()) 
                return;

            TicTacToeAI.MakeAIMove(GameBoard);
            
            if (TryFinishGame()) 
                return;

            OnBoardChangedEvent?.Invoke();
        }

        private bool TryFinishGame()
        {
            if (!TicTacToeAI.CheckGameIsFinished(GameBoard, out TicTacToeGameResult gameResult)) 
                return false;

            Debug.Log($"[TicTacToeService] Game finished with result: {gameResult}");
            
            OnBoardChangedEvent?.Invoke();
            OnGameFinishedEvent?.Invoke(gameResult);
            ForceFinishGame();
            
            return true;
        }

        public void ForceFinishGame()
        {
            if (!IsGameRunning.Value)
                return;
            
            Debug.Log("[TicTacToeService] Finishing game and clearing board");
            
            IsGameRunning.Value = false;

            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    GameBoard[i, j] = CellState.Empty;
                }
            }
        }
    }
}
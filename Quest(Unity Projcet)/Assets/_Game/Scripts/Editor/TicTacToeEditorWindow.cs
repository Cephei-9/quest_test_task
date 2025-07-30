using Naninovel;
using TicTacToeGame;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class TicTacToeEditorWindow : EditorWindow
    {
        private TicTacToeService _service;
        private TicTacToeGameResult _lastResult = TicTacToeGameResult.None;

        [MenuItem("Testing/TicTacToe Tester")]
        public static void ShowWindow()
        {
            TicTacToeEditorWindow window = GetWindow<TicTacToeEditorWindow>("TicTacToe Tester");
            window.InitializeService();
        }

        private void OnEnable()
        {
            if (_service == null)
                InitializeService();
        }

        private void InitializeService()
        {
            _service = new TicTacToeService();
            _service.OnBoardChangedEvent += OnBoardChanged;
            _service.OnGameFinishedEvent += OnGameFinished;
        }

        private void OnBoardChanged()
        {
            Repaint();
        }

        private void OnGameFinished(TicTacToeGameResult result)
        {
            _lastResult = result;
            Repaint();
        }

        private void OnGUI()
        {
            GUILayout.Label("TicTacToe Service Tester", EditorStyles.boldLabel);
            
            if (GUILayout.Button("Update Link to Service"))
            {
                InitializeService();
            }
            
            if (GUILayout.Button("Start New Game"))
            {
                Engine.GetService<TicTacToeService>().StartGame();
                _lastResult = TicTacToeGameResult.None;
            }

            if (GUILayout.Button("Clear Board"))
            {
                _service.ForceFinishGame();
                _lastResult = TicTacToeGameResult.None;
            }

            GUILayout.Space(10);
            DrawBoard();
            GUILayout.Space(10);

            if (_lastResult != TicTacToeGameResult.None)
            {
                EditorGUILayout.HelpBox("Game Result: " + _lastResult, MessageType.Info);
            }
        }

        private void DrawBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                EditorGUILayout.BeginHorizontal();

                for (int j = 0; j < 3; j++)
                {
                    CellState cell = _service.GameBoard[i, j];
                    string label = cell == CellState.Empty ? string.Empty : (cell == CellState.X ? "X" : "O");

                    if (GUILayout.Button(label, GUILayout.Width(50), GUILayout.Height(50)))
                    {
                        _service.MakeMove(i, j);
                    }
                }

                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
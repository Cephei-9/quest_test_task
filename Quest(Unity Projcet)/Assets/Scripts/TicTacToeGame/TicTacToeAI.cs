using System.Collections.Generic;
using UnityEngine;

namespace TicTacToeGame
{
    internal class TicTacToeAI
    {
        private static readonly System.Random randomGenerator = new();

        // Мы заранее описываем все возможные выигрышные линии, а после просто проходимся по ним и если на линии
        // все ячейки одинаковые значит победа
        private static readonly Vector2Int[][] winLines = 
        {
            new Vector2Int[] { new(0,0), new(0,1), new(0,2) },
            new Vector2Int[] { new(1,0), new(1,1), new(1,2) },
            new Vector2Int[] { new(2,0), new(2,1), new(2,2) },
            new Vector2Int[] { new(0,0), new(1,0), new(2,0) },
            new Vector2Int[] { new(0,1), new(1,1), new(2,1) },
            new Vector2Int[] { new(0,2), new(1,2), new(2,2) },
            new Vector2Int[] { new(0,0), new(1,1), new(2,2) },
            new Vector2Int[] { new(0,2), new(1,1), new(2,0) }
        };

        public static CellState[,] MakeAIMove(CellState[,] board)
        {
            List<Vector2Int> emptyCells = new();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == CellState.Empty)
                    {
                        emptyCells.Add(new Vector2Int(i, j));
                    }
                }
            }

            int count = emptyCells.Count;

            if(count > 0)
            {
                int index = randomGenerator.Next(count);
                Vector2Int cell = emptyCells[index];
                board[cell.x, cell.y] = CellState.O;
            }

            return board;
        }

        public static bool CheckGameIsFinished(CellState[,] board, out TicTacToeGameResult result)
        {
            foreach(Vector2Int[] line in winLines)
            {
                CellState first = board[line[0].x, line[0].y];
                
                if(first == CellState.Empty)
                    continue;

                bool win = true;
                
                for(int i = 1; i < line.Length; i++)
                {
                    if(board[line[i].x, line[i].y] != first)
                    {
                        win = false;
                        break;
                    }
                }

                if (!win) 
                    continue;
                
                result = first == CellState.X ? TicTacToeGameResult.PlayerWin : TicTacToeGameResult.AIWin;
                return true;
            }

            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if(board[i, j] == CellState.Empty)
                    {
                        result = TicTacToeGameResult.None;
                        return false;
                    }
                }
            }

            result = TicTacToeGameResult.Draw;
            return true;
        }
    }
}
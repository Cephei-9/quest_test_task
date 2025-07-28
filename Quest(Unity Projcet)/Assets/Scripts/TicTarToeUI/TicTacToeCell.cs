using System;
using TicTacToeGame;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Locations.TicTarToeUI
{
    public class TicTacToeCell : MonoBehaviour
    {
        [SerializeField] private Vector2Int _cellPosition;
        [SerializeField] private Button _button;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Sprite _xSprite;
        [SerializeField] private Sprite _oSprite;
        
        private CellState _currentState;

        public event Action<Vector2Int> OnCellClicked;

        public Vector2Int CellPosition => _cellPosition;

        private void Awake()
        {
            _button.onClick.AddListener(HandleClick);
        }

        public void SetState(CellState state)
        {
            _currentState = state;
            _iconImage.color = Color.white;
            
            if(state == CellState.X)
                _iconImage.sprite = _xSprite;
            else if (state == CellState.O)
                _iconImage.sprite = _oSprite;
            else
                _iconImage.color = new Color(1, 1, 1, 0.1f);
        }

        private void HandleClick()
        {
            if(_currentState == CellState.Empty)
                OnCellClicked?.Invoke(_cellPosition);
        }
    }
}
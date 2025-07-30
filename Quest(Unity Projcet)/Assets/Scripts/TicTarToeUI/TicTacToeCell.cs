using System;
using Naninovel;
using SoundInfrastructure;
using TicTacToeGame;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Locations.TicTarToeUI
{
    public class TicTacToeCell : MonoBehaviour
    {
        [SerializeField] private float _clickSoundVolume = 0.4f;
        [SerializeField] private Vector2Int _cellPosition;
        [SerializeField] private Button _button;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Sprite _xSprite;
        [SerializeField] private Sprite _oSprite;
        [SerializeField] private AudioClip _clickSound;
        
        private SoundService _soundService;
        private CellState _currentState;

        public event Action<Vector2Int> OnCellClicked;

        public Vector2Int CellPosition => _cellPosition;

        private void Awake()
        {
            _button.onClick.AddListener(HandleClick);
            _soundService = Engine.GetService<SoundService>();
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
            if (_currentState != CellState.Empty) 
                return;
            
            _soundService.PlaySfx(_clickSound, _clickSoundVolume);
            OnCellClicked?.Invoke(_cellPosition);
        }
    }
}
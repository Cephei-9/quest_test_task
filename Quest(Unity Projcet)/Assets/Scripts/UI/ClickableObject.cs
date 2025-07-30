using System;
using Naninovel;
using SoundInfrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ClickableObject : MonoBehaviour
    {
        [SerializeField] private float _clickSoundVolume = 1;
        [SerializeField] private AudioClip _clickSound;
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        
        private SoundService _soundService;

        public event Action OnClickEvent;

        public Image Image => _image;
        public Button Button => _button;

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void Start()
        {
            _soundService = Engine.GetService<SoundService>();
        }

        public void SetInteractable(bool interactable)
        {
            _button.interactable = interactable;
        }

        public void SetVisible(bool visible)
        {
            _image.enabled = visible;
            _button.enabled = visible;
        }

        private void OnButtonClicked()
        {
            _soundService.PlaySfx(_clickSound, _clickSoundVolume);
            OnClickEvent?.Invoke();
        }
    }
}
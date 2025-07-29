using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ClickableObject : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;

        public event Action OnClickEvent;

        public Image Image => _image;
        public Button Button => _button;

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClicked);
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
            OnClickEvent?.Invoke();
        }
    }
}
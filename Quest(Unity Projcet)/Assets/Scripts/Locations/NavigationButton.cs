using System;
using Naninovel;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Locations
{
    public class NavigationButton : MonoBehaviour
    {
        [SerializeField] private Image _buttonImage;
        
        private DialogueService _dialogueService;
        private IDisposable _subscribe;

        private void Awake()
        {
        }

        private void OnEnable()
        {
            _dialogueService ??= Engine.GetService<DialogueService>();
            _subscribe = _dialogueService.IsDialogueVisible.Subscribe(OnDialogueVisibleChangeHandler);
        }

        private void OnDisable()
        {
            _subscribe?.Dispose();
        }

        private void OnDialogueVisibleChangeHandler(bool visibleStatus)
        {
            _buttonImage.enabled = !visibleStatus;
        }
    }
}
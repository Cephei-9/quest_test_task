using System;
using Locations.NaniNames;
using Naninovel;
using TMPro;
using UI;
using UnityEngine;

namespace Locations
{
    public class NavigationButton : MonoBehaviour
    {
        [SerializeField] private ClickableObject _clickableObject;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Script _targetLocationScript;
        
        private IScriptPlayer _scriptPlayer;
        private DialogueService _dialogueService;
        private LocationService _locationService;
        private IDisposable _subscribe;

        private void Awake()
        {
            SetVisible(false);
        }

        private void Start()
        {
            _scriptPlayer = Engine.GetService<IScriptPlayer>();
            _locationService = Engine.GetService<LocationService>();
            _clickableObject.OnClickEvent += OnClickHandler;
        }

        private void OnEnable()
        {
            _dialogueService ??= Engine.GetService<DialogueService>();
        }

        private void OnDisable()
        {
            _subscribe?.Dispose();
        }

        // В последствии понял что отключать нужно не в момент диалога, а раньше, когда персонаж только появляется,
        // но исправлять не стал
        private void Update()
        {
            SetVisible(!_dialogueService.IsDialogueVisible.Value);
        }

        private void SetVisible(bool visibleStatus)
        {
            _clickableObject.SetVisible(visibleStatus);
            _text.enabled = visibleStatus;
        }

        private void OnClickHandler()
        {
            _scriptPlayer.PreloadAndPlayAsync(_targetLocationScript);
            _locationService.HideLocation();
        }
    }
}
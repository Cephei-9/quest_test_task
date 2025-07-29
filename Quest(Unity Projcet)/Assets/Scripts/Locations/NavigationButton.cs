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
        
        private ICustomVariableManager _variableManager;
        private IScriptPlayer _scriptPlayer;
        private DialogueService _dialogueService;
        private IDisposable _subscribe;
        
        // Нужно написать отдельный QuestService и он в одном месте будет отвечать за состояние квеста, и будет предоставлять
        // нормальный интерфейс. Но я уже на этапе полировки, и я это не уже не буду писать
        private bool QuestIsStarted
        {
            get
            {
                string questStage = _variableManager.GetVariableValue(NaniVariablesNames.QuestStage);
                return !string.IsNullOrWhiteSpace(questStage);
            }
        }

        private void Awake()
        {
            SetVisible(false);
        }

        private void Start()
        {
            _variableManager = Engine.GetService<ICustomVariableManager>();
            _scriptPlayer = Engine.GetService<IScriptPlayer>();
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
            bool visibleStatus = !_dialogueService.IsDialogueVisible.Value && QuestIsStarted;

            SetVisible(visibleStatus);
        }

        private void SetVisible(bool visibleStatus)
        {
            _clickableObject.SetVisible(visibleStatus);
            _text.enabled = visibleStatus;
        }

        private void OnClickHandler()
        {
            _scriptPlayer.PreloadAndPlayAsync(_targetLocationScript);
        }
    }
}
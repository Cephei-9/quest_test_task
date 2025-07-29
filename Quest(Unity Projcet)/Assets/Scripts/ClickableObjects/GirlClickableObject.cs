using System;
using Locations.NaniNames;
using NaniNames;
using Naninovel;
using UI;
using UnityEngine;

namespace ClickableObjects
{
    public class GirlClickableObject : MonoBehaviour
    {
        private const string GirlActorId = "Girl";
        
        [SerializeField] private ClickableObject _clickableObject;
        
        private ICustomVariableManager _variableManager;
        private ICharacterActor _characterActor;
        private IScriptPlayer _scriptPlayer;

        private void Awake()
        {
            _clickableObject.SetVisible(false);
        }

        private bool QuestIsStarted
        {
            get
            {
                string questStage = _variableManager.GetVariableValue(NaniVariablesNames.QuestStage);
                return !string.IsNullOrWhiteSpace(questStage);
            }
        }

        private void Start()
        {
            _variableManager = Engine.GetService<ICustomVariableManager>();
            _scriptPlayer = Engine.GetService<IScriptPlayer>();
            
            ICharacterManager charManager = Engine.GetService<ICharacterManager>();
            _characterActor = charManager.GetActor(GirlActorId);
            
            _clickableObject.OnClickEvent += OnClickHandler;
        }

        private void Update()
        {
            // Да это конечно не очень производительно, по хорошему конечно писать инфраструктуру для того чтобы можно было
            // подписываться на то что актер начал диалог 
            _clickableObject.SetVisible(!_characterActor.Visible && QuestIsStarted);
        }

        private void OnClickHandler()
        {
            _scriptPlayer.PreloadAndPlayAsync(NaniScriptsNames.ClickToGirlScript);
        }
    }
}
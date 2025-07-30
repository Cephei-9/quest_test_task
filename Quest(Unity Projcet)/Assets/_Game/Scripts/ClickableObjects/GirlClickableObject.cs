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
        
        private ICharacterActor _characterActor;
        private IScriptPlayer _scriptPlayer;

        private void Awake()
        {
            _clickableObject.SetVisible(false);
        }

        private void Start()
        {
            _scriptPlayer = Engine.GetService<IScriptPlayer>();
            
            ICharacterManager charManager = Engine.GetService<ICharacterManager>();
            _characterActor = charManager.GetActor(GirlActorId);
            
            _clickableObject.OnClickEvent += OnClickHandler;
        }

        private void Update()
        {
            // Да это конечно не очень производительно, по хорошему конечно писать инфраструктуру для того чтобы можно было
            // подписываться на то что актер начал диалог, но пока так)
            _clickableObject.SetVisible(!_characterActor.Visible);
        }

        private void OnClickHandler()
        {
            _scriptPlayer.PreloadAndPlayAsync(NaniScriptsNames.ClickToGirlScript);
        }
    }
}
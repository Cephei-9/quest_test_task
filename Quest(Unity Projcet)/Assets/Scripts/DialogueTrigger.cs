using Naninovel;
using UnityEngine;

namespace Locations
{
    // Скрипт который вешается на префаб диалогового окна и цепляется к эвентам о показе и сокрытии диалога,
    // и обновляет значения в сервисе диалога
    
    public class DialogueTrigger : MonoBehaviour
    {
        private DialogueService _dialogueService;

        public void OnShowDialogue()
        {
            _dialogueService ??= Engine.GetService<DialogueService>();

            _dialogueService.IsDialogueVisible.Value = true;
        }

        public void OnHideDialogue()
        {
            _dialogueService ??= Engine.GetService<DialogueService>();

            _dialogueService.IsDialogueVisible.Value = false;
        }
    }
}
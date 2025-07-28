using Naninovel;
using UnityEngine;

namespace Locations
{
    public class DialogueTrigger : MonoBehaviour
    {
        private DialogueService _dialogueService;

        private void Awake()
        {
        }

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
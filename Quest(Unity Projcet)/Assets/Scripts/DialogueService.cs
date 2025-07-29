using System;
using System.Linq;
using Naninovel;
using Naninovel.UI;
using UniRx;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Locations
{
    [InitializeAtRuntime]
    public class DialogueService : IEngineService
    {
        public readonly ReactiveProperty<bool> IsDialogueVisible = new();
        
        private readonly ITextPrinterManager _textPrinterManager;

        public DialogueService(ITextPrinterManager textPrinterManager)
        {
            _textPrinterManager = textPrinterManager;
        }

        public UniTask InitializeServiceAsync()
        {
            return UniTask.CompletedTask;
        }

        public void DestroyService()
        {
            _textPrinterManager.OnPrintTextStarted -= OnDialogueStartHandler;
            _textPrinterManager.OnPrintTextFinished -= OnDialogueFinishHandler;
        }

        public void ResetService() { }

        private void OnDialogueStartHandler(PrintTextArgs args)
        {
            IsDialogueVisible.Value = true;
        }
        
        private void OnDialogueFinishHandler(PrintTextArgs args)
        {
            IsDialogueVisible.Value = false;
        }

        private bool IsAnyPrinterIsVisible()
        {
            foreach (ITextPrinterActor textPrinterActor in _textPrinterManager.GetAllActors())
            {
                if (textPrinterActor.Visible)
                    return true;
            }

            return false;
        }
    }
}
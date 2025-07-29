using Naninovel;
using UnityEngine;

namespace Locations
{
    public class NaninovelTestInput : MonoBehaviour
    {
        [SerializeField] private string _hallScriptId = "Hall";
        [SerializeField] private string _testVarName  = "TestVar";

        private IScriptPlayer _scriptPlayer;
        private ICustomVariableManager _variableManager;

        private async void Start()
        {
            if (!Engine.Initialized)
                await UniTask.WaitUntil(() => Engine.Initialized);

            _scriptPlayer = Engine.GetService<IScriptPlayer>();
            _variableManager = Engine.GetService<ICustomVariableManager>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
                PlayHall().Forget();

            if (Input.GetKeyDown(KeyCode.U))
                _variableManager.SetVariableValue(_testVarName, "True");

            if (Input.GetKeyDown(KeyCode.I))
                Debug.Log(_variableManager.GetVariableValue(_testVarName));
        }

        private async UniTaskVoid PlayHall()
        {
            await _scriptPlayer.PreloadAndPlayAsync(_hallScriptId);
        }
    }
}
using Naninovel;
using UnityEngine;

namespace UI
{
    // Назначение этого скрипта в том что он отключает основной Canvas и включает его только когда Naninovel проинициализируется.
    // Мы можем быть уверены что этот скрипт сработает первым потому что я настроил это в script execution order. Думаю
    // для классов инициализации этот инструмент подходит.  
    
    // Это как бы работает, но конечно лучше делать более прозрачную и последовательную инициализацию. 
    
    public class UIInitialization : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.SetActive(false);
            Engine.OnInitializationFinished += () => gameObject.SetActive(true);
        }
    }
}
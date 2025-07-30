using Naninovel;
using UnityEngine;

namespace SoundInfrastructure
{
    // Сервис для работы с звуком. Чтобы из каждой точки проекта мы могли централизованно работать с звуком.
    // Пока мы только удобно включаем SFX, чтобы не привязывать звук к объекту его вызвавшему 
    
    [InitializeAtRuntime]
    public class SoundService : IEngineService
    {
        private MainAudioSource _mainAudioSource;

        public UniTask InitializeServiceAsync()
        {
            _mainAudioSource = Object.FindObjectOfType<MainAudioSource>();
            
            return UniTask.CompletedTask;
        }

        public void ResetService()
        {
        }

        public void DestroyService()
        {
        }

        public void PlaySfx(AudioClip clip, float volume = 1)
        {
            if (_mainAudioSource && clip) 
                _mainAudioSource.PlaySfx(clip, volume);
        }
    }
}
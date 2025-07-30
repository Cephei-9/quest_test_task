using UnityEngine;

namespace SoundInfrastructure
{
    // Просто компонент оборачивающий главный AudioSource, который позволяет найти этот AudioSource,
    // ну и может предоставлять удобный интерфейс для работы с ним 
    
    public class MainAudioSource : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        public AudioSource AudioSource => _audioSource;

        public void PlaySfx(AudioClip clip, float volume)
        {
            _audioSource.PlayOneShot(clip, volume);
        }
    }
}
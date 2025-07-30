using System.Threading;
using Naninovel;
using UnityEngine;

namespace Locations
{
    // Базовый класс для локации который может быть расширен. Этот класс включает и отключает локации по команде, и
    // реализует логику плавного появления и затухания
    
    [RequireComponent(typeof(CanvasGroup))]
    public class LocationUI : MonoBehaviour
    {
        [SerializeField] private float _fadeSpeed = 0.7f;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private CancellationTokenSource _fadeAnimationCts;

        private void Awake()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        public virtual async UniTask Show()
        {
            if (gameObject.activeSelf)
                return;
            
            gameObject.SetActive(true);
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            CancellationToken token = ResetToken();
            
            await FadeTo(1f, token);

            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public virtual async UniTask Hide()
        {
            if (!gameObject.activeSelf)
                return;
            
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            CancellationToken token = ResetToken();
            
            await FadeTo(0f, token);

            gameObject.SetActive(false);
        }

        private CancellationToken ResetToken()
        {
            _fadeAnimationCts?.Cancel();
            _fadeAnimationCts?.Dispose();
            _fadeAnimationCts = new CancellationTokenSource();

            return CancellationTokenSource
                .CreateLinkedTokenSource(_fadeAnimationCts.Token, destroyCancellationToken).Token;
        }

        private async UniTask FadeTo(float targetAlpha, CancellationToken token)
        {
            while (!Mathf.Approximately(_canvasGroup.alpha, targetAlpha))
            {
                float currentAlpha = _canvasGroup.alpha;
                float delta = _fadeSpeed * Time.deltaTime;
                float newAlpha = currentAlpha < targetAlpha
                    ? currentAlpha + delta
                    : currentAlpha - delta;
                newAlpha = Mathf.Clamp(newAlpha, 0, 1);
                
                _canvasGroup.alpha = newAlpha;
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
        }
    }
}

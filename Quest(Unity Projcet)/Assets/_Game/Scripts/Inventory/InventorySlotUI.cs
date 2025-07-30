using System.Collections.Generic;
using System.Threading;
using Naninovel;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private IconBinding[] _iconBindings;
        [SerializeField] private float _bounceAmplitude = 0.2f;
        [SerializeField] private float _bounceDuration = 0.3f;
        
        private readonly Dictionary<string, Sprite> _iconMap = new();

        private CancellationTokenSource _bounceCts = new();

        private void Awake()
        {
            FillIconMap();
        }

        public void SetItem(string itemId, bool playAnimation)
        {
            StopBounceAnimation();

            _iconImage.enabled = true;
            _iconImage.sprite = _iconMap.GetValueOrDefault(itemId);

            if (playAnimation) 
                PlayBounceAnimation(_bounceCts.Token).Forget();
        }

        public void RemoveItem()
        {
            StopBounceAnimation();

            _iconImage.enabled = false;
            _iconImage.sprite = null;
        }

        private void StopBounceAnimation()
        {
            _bounceCts.Cancel();
            _bounceCts.Dispose();
            _bounceCts = new CancellationTokenSource();
        }

        private async UniTaskVoid PlayBounceAnimation(CancellationToken token = default)
        {
            Vector3 originalScale = _iconImage.transform.localScale;
            float elapsed = 0;

            try
            {
                while (elapsed < _bounceDuration)
                {
                    float progress = elapsed / _bounceDuration;
                    float scaleOffset = Mathf.Sin(progress * Mathf.PI) * _bounceAmplitude;
                    _iconImage.transform.localScale = originalScale * (1 + scaleOffset);
                    
                    elapsed += Time.deltaTime;
                    await UniTask.Yield(PlayerLoopTiming.Update, token);
                }
            }
            finally
            {
                _iconImage.transform.localScale = Vector3.one;
            }
        }

        private void FillIconMap()
        {
            int count = _iconBindings.Length;

            for (int i = 0; i < count; i++)
            {
                _iconMap[_iconBindings[i].ItemId] = _iconBindings[i].Icon;
            }
        }

        [System.Serializable]
        private class IconBinding
        {
            public string ItemId;
            public Sprite Icon;
        }
    }
}
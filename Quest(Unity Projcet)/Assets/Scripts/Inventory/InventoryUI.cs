using System.Collections.Generic;
using Naninovel;
using UniRx;
using UnityEngine;

namespace Inventory
{
    // Класс отображающий инвентарь на экране. Он получает данные у сервиса и назначает их отдельным слотам. По хорошему
    // его бы разделить на условный контроллер, который бы работал с сервисом, и на view которая бы реализовывала UI
    // тонкости, но я решил в этом проекте не имплементировать MVC идею 
    
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private List<InventorySlotUI> _slots = new();
        private InventoryService _service;

        private void Awake()
        {
            _service = Engine.GetService<InventoryService>();

            UpdateSlots(_service.ItemsCollection, string.Empty, false);
            
            _service.ItemsCollection.ObserveAdd().Subscribe(OnItemAddedHandler).AddTo(this);
            _service.ItemsCollection.ObserveRemove().Subscribe(OnItemRemovedHandler).AddTo(this);
            _service.ItemsCollection.ObserveReset().Subscribe(_ => OnClearItemListHandler()).AddTo(this);
        }

        private void OnItemAddedHandler(CollectionAddEvent<string> addEvent)
        {
            bool isLoading = _service.IsLoading.Value;
            bool showAnimation = !isLoading;
            
            UpdateSlots(_service.ItemsCollection, addEvent.Value, showAnimation);
        }

        private void OnItemRemovedHandler(CollectionRemoveEvent<string> removeEvent)
        {
            UpdateSlots(_service.ItemsCollection, string.Empty, false);
        }

        private void OnClearItemListHandler()
        {
            UpdateSlots(_service.ItemsCollection, string.Empty, false);
        }

        private void UpdateSlots(IReadOnlyList<string> itemIds, string animatedItemId, bool playAnimation)
        {
            int count = _slots.Count;
            for (int i = 0; i < count; i++)
            {
                if (i < itemIds.Count)
                {
                    bool shouldAnimate = playAnimation && itemIds[i] == animatedItemId;
                    _slots[i].SetItem(itemIds[i], shouldAnimate);
                }
                else
                {
                    _slots[i].RemoveItem();
                }
            }
        }
    }
}
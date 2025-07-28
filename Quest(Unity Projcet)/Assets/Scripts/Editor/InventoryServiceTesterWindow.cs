using Inventory;
using Naninovel;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class InventoryServiceTesterWindow : EditorWindow
    {
        private const string DefaultItemId = "TestItem";

        private InventoryService _service;
        private string _itemId = DefaultItemId;

        [MenuItem("Testing/InventoryTester")]
        public static void ShowWindow()
        {
            InventoryServiceTesterWindow window = GetWindow<InventoryServiceTesterWindow>();
            window.titleContent = new GUIContent("Inventory Tester");
            window.Show();
        }

        public void AddItem()
        {
            _service = Engine.GetService<InventoryService>();
            _service?.AddItem(_itemId);
        }

        public void RemoveItem()
        {
            _service = Engine.GetService<InventoryService>();
            _service?.RemoveItem(_itemId);
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Item Id");
            _itemId = EditorGUILayout.TextField(_itemId);

            if (GUILayout.Button("Add Item"))
            {
                AddItem();
            }

            if (GUILayout.Button("Remove Item"))
            {
                RemoveItem();
            }

            EditorGUILayout.Space();

            if (_service != null)
            {
                EditorGUILayout.LabelField("Current Items:");
                foreach (string id in _service.ItemsCollection)
                {
                    EditorGUILayout.LabelField(id);
                }
            }
        }
    }
}
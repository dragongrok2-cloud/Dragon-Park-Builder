using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DragonPark.Farm;
using System.Collections.Generic;

namespace DragonPark.UI
{
    /// <summary>
    /// Простой UI инвентаря еды.
    /// </summary>
    public class FoodInventoryUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private Transform contentParent;
        [SerializeField] private GameObject itemSlotPrefab; // Prefab: Image + Text (название) + Text (количество) + Button

        [Header("Optional")]
        [SerializeField] private TextMeshProUGUI emptyText;

        private void Start()
        {
            if (inventoryPanel != null)
                inventoryPanel.SetActive(false);

            if (FoodManager.Instance != null)
            {
                FoodManager.Instance.OnInventoryChanged += RefreshUI;
            }

            RefreshUI();
        }

        private void Update()
        {
            // Открыть/закрыть на I
            if (Input.GetKeyDown(KeyCode.I))
            {
                ToggleInventory();
            }
        }

        public void ToggleInventory()
        {
            if (inventoryPanel == null) return;

            bool active = !inventoryPanel.activeSelf;
            inventoryPanel.SetActive(active);

            if (active)
                RefreshUI();
        }

        private void RefreshUI()
        {
            if (contentParent == null) return;

            // Очищаем старые слоты
            foreach (Transform child in contentParent)
            {
                Destroy(child.gameObject);
            }

            if (FoodManager.Instance == null) return;

            var inventory = FoodManager.Instance.GetInventory();

            if (inventory.Count == 0)
            {
                if (emptyText != null)
                    emptyText.gameObject.SetActive(true);
                return;
            }

            if (emptyText != null)
                emptyText.gameObject.SetActive(false);

            foreach (var kvp in inventory)
            {
                CreateSlot(kvp.Key, kvp.Value);
            }
        }

        private void CreateSlot(string foodName, int amount)
        {
            if (itemSlotPrefab == null) return;

            GameObject slot = Instantiate(itemSlotPrefab, contentParent);

            // Пытаемся найти текст
            TextMeshProUGUI[] texts = slot.GetComponentsInChildren<TextMeshProUGUI>();
            if (texts.Length >= 1)
                texts[0].text = foodName;
            if (texts.Length >= 2)
                texts[1].text = $"x{amount}";

            // Кнопка использования (если есть)
            Button useButton = slot.GetComponentInChildren<Button>();
            if (useButton != null)
            {
                string capturedName = foodName;
                useButton.onClick.AddListener(() => OnUseFood(capturedName));
            }
        }

        private void OnUseFood(string foodName)
        {
            if (FoodManager.Instance == null) return;

            if (FoodManager.Instance.UseFood(foodName, out FoodItem food))
            {
                // Здесь можно найти выбранного дракона и покормить его
                var dragon = FindObjectOfType<DragonPark.Dragons.Dragon>();
                if (dragon != null && food != null)
                {
                    dragon.Feed(food.hungerRestore);
                    dragon.happiness = Mathf.Min(100f, dragon.happiness + food.happinessBonus);
                    Debug.Log($"Использована еда: {foodName}");
                }
            }

            RefreshUI();
        }

        private void OnDestroy()
        {
            if (FoodManager.Instance != null)
                FoodManager.Instance.OnInventoryChanged -= RefreshUI;
        }
    }
}

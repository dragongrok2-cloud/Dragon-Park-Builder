using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DragonPark.Park;
using DragonPark.Core;
using System.Collections.Generic;

namespace DragonPark.UI
{
    /// <summary>
    /// Простое меню выбора зданий для размещения.
    /// </summary>
    public class BuildingMenu : MonoBehaviour
    {
        [System.Serializable]
        public class BuildingOption
        {
            public string displayName;
            public Building prefab;
            public int cost;
            public Sprite icon;
        }

        [Header("UI")]
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private Transform buttonsParent;
        [SerializeField] private GameObject buttonPrefab; // Prefab кнопки с Image + Text

        [Header("Building Options")]
        [SerializeField] private List<BuildingOption> availableBuildings = new List<BuildingOption>();

        [Header("References")]
        [SerializeField] private BuildingPlacer buildingPlacer;

        private void Start()
        {
            if (menuPanel != null)
                menuPanel.SetActive(false);

            CreateButtons();
        }

        private void Update()
        {
            // Открыть/закрыть меню на M
            if (Input.GetKeyDown(KeyCode.M))
            {
                ToggleMenu();
            }
        }

        public void ToggleMenu()
        {
            if (menuPanel == null) return;

            bool isActive = !menuPanel.activeSelf;
            menuPanel.SetActive(isActive);
        }

        private void CreateButtons()
        {
            if (buttonsParent == null || buttonPrefab == null) return;

            foreach (var option in availableBuildings)
            {
                GameObject btnObj = Instantiate(buttonPrefab, buttonsParent);
                Button btn = btnObj.GetComponent<Button>();
                TextMeshProUGUI txt = btnObj.GetComponentInChildren<TextMeshProUGUI>();

                if (txt != null)
                    txt.text = $"{option.displayName}\n{option.cost} золота";

                if (btn != null)
                {
                    BuildingOption captured = option; // для замыкания
                    btn.onClick.AddListener(() => OnBuildingSelected(captured));
                }
            }
        }

        private void OnBuildingSelected(BuildingOption option)
        {
            if (option.prefab == null) return;

            if (CurrencyManager.Instance != null && !CurrencyManager.Instance.HasEnoughGold(option.cost))
            {
                Debug.Log("Недостаточно золота для покупки здания!");
                return;
            }

            // Сохраняем стоимость, чтобы списать при успешном размещении
            // Пока просто запускаем размещение
            if (buildingPlacer != null)
            {
                // Нужно будет доработать BuildingPlacer, чтобы принимать префаб и стоимость
                Debug.Log($"Выбрано здание: {option.displayName}. Нажмите B или используйте placer.");
                // Временная заглушка — можно расширить BuildingPlacer
            }

            ToggleMenu();
        }

        public void CloseMenu()
        {
            if (menuPanel != null)
                menuPanel.SetActive(false);
        }
    }
}

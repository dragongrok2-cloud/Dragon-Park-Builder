using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DragonPark.Dragons;

namespace DragonPark.UI
{
    /// <summary>
    /// Простой UI для отображения состояния дракона и кнопок взаимодействия.
    /// </summary>
    public class DragonUI : MonoBehaviour
    {
        [Header("Target Dragon")]
        [SerializeField] private Dragon targetDragon;

        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI stageText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Slider hungerSlider;
        [SerializeField] private Slider happinessSlider;
        [SerializeField] private Slider growthSlider;
        [SerializeField] private Button feedButton;
        [SerializeField] private Button petButton;

        private void Start()
        {
            if (targetDragon == null)
            {
                targetDragon = FindObjectOfType<Dragon>();
            }

            if (targetDragon != null)
            {
                targetDragon.OnFed += OnDragonUpdated;
                targetDragon.OnPetted += OnDragonUpdated;
                targetDragon.OnStageChanged += OnDragonUpdated;
                targetDragon.OnLevelUp += OnDragonUpdated;
            }

            if (feedButton != null)
                feedButton.onClick.AddListener(OnFeedClicked);

            if (petButton != null)
                petButton.onClick.AddListener(OnPetClicked);

            UpdateUI();
        }

        private void Update()
        {
            UpdateUI();
        }

        private void OnDragonUpdated(Dragon dragon)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (targetDragon == null) return;

            if (nameText) nameText.text = targetDragon.dragonName;
            if (stageText) stageText.text = $"Стадия: {GetStageName(targetDragon.currentStage)}";
            if (levelText) levelText.text = $"Уровень: {targetDragon.level}";

            if (hungerSlider) hungerSlider.value = targetDragon.hunger / 100f;
            if (happinessSlider) happinessSlider.value = targetDragon.happiness / 100f;
        }

        private void OnFeedClicked()
        {
            targetDragon?.Feed(25f);
        }

        private void OnPetClicked()
        {
            targetDragon?.Pet();
        }

        private string GetStageName(DragonStage stage)
        {
            return stage switch
            {
                DragonStage.Egg => "Яйцо",
                DragonStage.Baby => "Малыш",
                DragonStage.Young => "Юный",
                DragonStage.Adult => "Взрослый",
                DragonStage.Legendary => "Легендарный",
                _ => stage.ToString()
            };
        }

        private void OnDestroy()
        {
            if (targetDragon != null)
            {
                targetDragon.OnFed -= OnDragonUpdated;
                targetDragon.OnPetted -= OnDragonUpdated;
                targetDragon.OnStageChanged -= OnDragonUpdated;
                targetDragon.OnLevelUp -= OnDragonUpdated;
            }
        }
    }
}

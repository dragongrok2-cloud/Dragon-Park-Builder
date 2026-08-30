using UnityEngine;
using System;

namespace DragonPark.Dragons
{
    /// <summary>
    /// Базовый класс дракона.
    /// Хранит характеристики, стадию роста, голод, счастье и позволяет кормить / гладить.
    /// </summary>
    public class Dragon : MonoBehaviour
    {
        [Header("Identity")]
        public string dragonName = "Маленький дракончик";
        public string element = "Огонь"; // Огонь, Вода, Земля, Воздух, Магия и т.д.

        [Header("Growth")]
        public DragonStage currentStage = DragonStage.Egg;
        [SerializeField] private float growthProgress = 0f;      // 0–100
        [SerializeField] private float growthRequired = 100f;

        [Header("Needs")]
        [Range(0f, 100f)] public float hunger = 50f;            // 0 = очень голоден, 100 = сыт
        [Range(0f, 100f)] public float happiness = 70f;         // 0 = грустный, 100 = очень счастлив

        [Header("Stats")]
        public int level = 1;
        public float experience = 0f;
        public float maxExperience = 100f;

        // События (можно подписываться из UI)
        public event Action<Dragon> OnFed;
        public event Action<Dragon> OnPetted;
        public event Action<Dragon> OnStageChanged;
        public event Action<Dragon> OnLevelUp;

        private void Update()
        {
            // Медленное ухудшение потребностей со временем (можно настроить)
            hunger = Mathf.Max(0f, hunger - Time.deltaTime * 0.5f);
            happiness = Mathf.Max(0f, happiness - Time.deltaTime * 0.2f);
        }

        /// <summary>
        /// Покормить дракона
        /// </summary>
        public void Feed(float foodValue = 25f)
        {
            hunger = Mathf.Min(100f, hunger + foodValue);
            happiness = Mathf.Min(100f, happiness + 5f);

            // Рост немного ускоряется, когда дракон сыт
            if (hunger > 60f)
            {
                AddGrowth(3f);
            }

            OnFed?.Invoke(this);
            Debug.Log($"{dragonName} покормлен! Голод: {hunger:F0}");
        }

        /// <summary>
        /// Погладить дракона
        /// </summary>
        public void Pet()
        {
            happiness = Mathf.Min(100f, happiness + 15f);
            AddGrowth(1.5f);

            OnPetted?.Invoke(this);
            Debug.Log($"{dragonName} доволен! Счастье: {happiness:F0}");
        }

        /// <summary>
        /// Добавить прогресс роста
        /// </summary>
        public void AddGrowth(float amount)
        {
            if (currentStage == DragonStage.Legendary) return;

            growthProgress += amount;

            if (growthProgress >= growthRequired)
            {
                growthProgress = 0f;
                AdvanceStage();
            }
        }

        private void AdvanceStage()
        {
            currentStage = currentStage switch
            {
                DragonStage.Egg => DragonStage.Baby,
                DragonStage.Baby => DragonStage.Young,
                DragonStage.Young => DragonStage.Adult,
                DragonStage.Adult => DragonStage.Legendary,
                _ => currentStage
            };

            // Можно увеличить требования к следующему этапу
            growthRequired *= 1.3f;

            OnStageChanged?.Invoke(this);
            Debug.Log($"{dragonName} вырос! Теперь он: {currentStage}");
        }

        /// <summary>
        /// Добавить опыт (например, после битвы или мини-игры)
        /// </summary>
        public void AddExperience(float exp)
        {
            experience += exp;

            while (experience >= maxExperience)
            {
                experience -= maxExperience;
                level++;
                maxExperience *= 1.2f;
                OnLevelUp?.Invoke(this);
                Debug.Log($"{dragonName} достиг уровня {level}!");
            }
        }

        /// <summary>
        /// Проверка, можно ли летать (только взрослые и выше)
        /// </summary>
        public bool CanFly => currentStage >= DragonStage.Young;

        /// <summary>
        /// Проверка, можно ли участвовать в битвах
        /// </summary>
        public bool CanBattle => currentStage >= DragonStage.Young && hunger > 20f && happiness > 20f;
    }
}

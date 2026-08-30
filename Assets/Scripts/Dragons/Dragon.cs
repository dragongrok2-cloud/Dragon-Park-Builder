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
        public DragonSpecies species; // Ссылка на ScriptableObject вида

        [Header("Growth")]
        public DragonStage currentStage = DragonStage.Egg;
        [SerializeField] private float growthProgress = 0f;
        [SerializeField] private float growthRequired = 100f;

        [Header("Needs")]
        [Range(0f, 100f)] public float hunger = 50f;
        [Range(0f, 100f)] public float happiness = 70f;

        [Header("Stats")]
        public int level = 1;
        public float experience = 0f;
        public float maxExperience = 100f;

        // События
        public event Action<Dragon> OnFed;
        public event Action<Dragon> OnPetted;
        public event Action<Dragon> OnStageChanged;
        public event Action<Dragon> OnLevelUp;

        public string Element => species != null ? species.element : "Неизвестно";

        private void Update()
        {
            hunger = Mathf.Max(0f, hunger - Time.deltaTime * 0.5f);
            happiness = Mathf.Max(0f, happiness - Time.deltaTime * 0.2f);
        }

        public void Feed(float foodValue = 25f)
        {
            hunger = Mathf.Min(100f, hunger + foodValue);
            happiness = Mathf.Min(100f, happiness + 5f);

            if (hunger > 60f)
            {
                AddGrowth(3f);
            }

            OnFed?.Invoke(this);
            Debug.Log($"{dragonName} покормлен! Голод: {hunger:F0}");
        }

        public void Pet()
        {
            happiness = Mathf.Min(100f, happiness + 15f);
            AddGrowth(1.5f);

            OnPetted?.Invoke(this);
            Debug.Log($"{dragonName} доволен! Счастье: {happiness:F0}");
        }

        public void AddGrowth(float amount)
        {
            if (currentStage == DragonStage.Legendary) return;

            float multiplier = species != null ? species.growthMultiplier : 1f;
            growthProgress += amount * multiplier;

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

            growthRequired *= 1.3f;

            OnStageChanged?.Invoke(this);
            Debug.Log($"{dragonName} вырос! Теперь он: {currentStage}");
        }

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

        public bool CanFly => currentStage >= DragonStage.Young;
        public bool CanBattle => currentStage >= DragonStage.Young && hunger > 20f && happiness > 20f;
        public bool CanBreed => currentStage >= DragonStage.Adult && happiness >= 40f;
    }
}

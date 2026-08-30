using UnityEngine;
using System;
using System.Collections;

namespace DragonPark.Dragons
{
    /// <summary>
    /// Система скрещивания драконов.
    /// </summary>
    public class BreedingManager : MonoBehaviour
    {
        public static BreedingManager Instance { get; private set; }

        [Header("Settings")]
        [SerializeField] private float defaultBreedingTime = 60f;

        public event Action<Dragon, Dragon, DragonSpecies> OnBreedingStarted;
        public event Action<DragonSpecies> OnBreedingCompleted; // возвращает вид детёныша

        private bool isBreeding = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Попытаться скрестить двух драконов
        /// </summary>
        public bool TryBreed(Dragon parent1, Dragon parent2)
        {
            if (isBreeding)
            {
                Debug.Log("Уже идёт скрещивание!");
                return false;
            }

            if (parent1 == null || parent2 == null)
            {
                Debug.Log("Нужны оба родителя!");
                return false;
            }

            if (parent1.currentStage < DragonStage.Adult || parent2.currentStage < DragonStage.Adult)
            {
                Debug.Log("Оба дракона должны быть взрослыми!");
                return false;
            }

            if (parent1.happiness < 40f || parent2.happiness < 40f)
            {
                Debug.Log("Драконы должны быть достаточно счастливы!");
                return false;
            }

            // Определяем результат
            DragonSpecies resultSpecies = DetermineOffspring(parent1, parent2);

            if (resultSpecies == null)
            {
                Debug.Log("Не удалось определить потомство.");
                return false;
            }

            float breedingTime = resultSpecies.breedingTime > 0 ? resultSpecies.breedingTime : defaultBreedingTime;

            StartCoroutine(BreedingCoroutine(parent1, parent2, resultSpecies, breedingTime));
            return true;
        }

        private DragonSpecies DetermineOffspring(Dragon parent1, Dragon parent2)
        {
            // Простая логика: если у видов есть possibleOffspring — берём оттуда
            // Иначе возвращаем вид первого родителя (заглушка)

            // В будущем можно сделать более сложную систему комбинаций элементов

            // Пока заглушка — возвращаем null, чтобы пользователь настроил через ScriptableObject
            Debug.Log($"Скрещивание: {parent1.dragonName} + {parent2.dragonName}");
            return null; // TODO: реализовать выбор из possibleOffspring
        }

        private IEnumerator BreedingCoroutine(Dragon parent1, Dragon parent2, DragonSpecies result, float time)
        {
            isBreeding = true;
            OnBreedingStarted?.Invoke(parent1, parent2, result);

            Debug.Log($"Скрещивание началось! Осталось {time} секунд...");

            yield return new WaitForSeconds(time);

            isBreeding = false;
            OnBreedingCompleted?.Invoke(result);

            Debug.Log("Скрещивание завершено! Получен новый дракон.");
        }

        public bool IsBreeding => isBreeding;
    }
}

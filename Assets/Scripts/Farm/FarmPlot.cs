using UnityEngine;
using System;

namespace DragonPark.Farm
{
    /// <summary>
    /// Грядка / участок фермы, на котором растёт еда.
    /// </summary>
    public class FarmPlot : MonoBehaviour
    {
        public enum PlotState
        {
            Empty,
            Growing,
            Ready
        }

        [Header("State")]
        public PlotState currentState = PlotState.Empty;

        [Header("Current Crop")]
        public FoodItem currentFood;
        public float growProgress = 0f;

        public event Action<FarmPlot> OnCropReady;

        private void Update()
        {
            if (currentState == PlotState.Growing && currentFood != null)
            {
                growProgress += Time.deltaTime;

                if (growProgress >= currentFood.growTime)
                {
                    growProgress = currentFood.growTime;
                    currentState = PlotState.Ready;
                    OnCropReady?.Invoke(this);
                    Debug.Log($"Урожай готов: {currentFood.foodName}!");
                }
            }
        }

        /// <summary>
        /// Посадить еду
        /// </summary>
        public bool Plant(FoodItem food)
        {
            if (currentState != PlotState.Empty || food == null)
                return false;

            currentFood = food;
            growProgress = 0f;
            currentState = PlotState.Growing;
            Debug.Log($"Посажено: {food.foodName}");
            return true;
        }

        /// <summary>
        /// Собрать урожай
        /// </summary>
        public FoodItem Harvest()
        {
            if (currentState != PlotState.Ready)
                return null;

            FoodItem harvested = currentFood;
            currentFood = null;
            growProgress = 0f;
            currentState = PlotState.Empty;

            Debug.Log($"Собран урожай: {harvested.foodName}");
            return harvested;
        }

        public float GetProgressNormalized()
        {
            if (currentFood == null || currentFood.growTime <= 0f)
                return 0f;

            return Mathf.Clamp01(growProgress / currentFood.growTime);
        }
    }
}

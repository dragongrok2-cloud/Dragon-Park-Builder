using UnityEngine;

namespace DragonPark.Farm
{
    /// <summary>
    /// Данные о виде еды (можно сделать ScriptableObject позже).
    /// </summary>
    [System.Serializable]
    public class FoodItem
    {
        public string foodName = "Яблоко";
        public int hungerRestore = 20;
        public int happinessBonus = 5;
        public int cost = 10;
        public float growTime = 30f; // секунды
    }
}

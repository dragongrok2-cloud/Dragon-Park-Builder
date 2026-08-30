using UnityEngine;

namespace DragonPark.Park
{
    /// <summary>
    /// Базовый класс здания в парке.
    /// </summary>
    public class Building : MonoBehaviour
    {
        [Header("Building Info")]
        public string buildingName = "Здание";
        public BuildingType buildingType = BuildingType.Decoration;
        public int level = 1;
        public int cost = 100;

        [Header("Grid")]
        public Vector2Int size = new Vector2Int(1, 1); // Размер в клетках сетки

        [Header("Visual")]
        public GameObject model;

        public enum BuildingType
        {
            Habitat,        // Жилище для драконов
            Farm,           // Ферма для еды
            Decoration,     // Украшение
            Training,       // Тренировочная площадка
            Shop,           // Магазин
            Hatchery        // Инкубатор
        }

        public virtual void OnPlaced()
        {
            Debug.Log($"Здание {buildingName} размещено!");
        }

        public virtual void OnRemoved()
        {
            Debug.Log($"Здание {buildingName} удалено.");
        }
    }
}

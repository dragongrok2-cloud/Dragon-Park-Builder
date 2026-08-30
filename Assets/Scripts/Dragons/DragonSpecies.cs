using UnityEngine;

namespace DragonPark.Dragons
{
    /// <summary>
    /// ScriptableObject для вида дракона.
    /// Создавай через: Create → Dragon Park → Dragon Species
    /// </summary>
    [CreateAssetMenu(fileName = "NewDragonSpecies", menuName = "Dragon Park/Dragon Species")]
    public class DragonSpecies : ScriptableObject
    {
        [Header("Basic Info")]
        public string speciesName = "Огненный дракон";
        [TextArea(2, 4)]
        public string description = "Классический огненный дракон.";
        public string element = "Огонь"; // Огонь, Вода, Земля, Воздух, Магия, Свет, Тьма...

        [Header("Rarity")]
        public Rarity rarity = Rarity.Common;

        public enum Rarity
        {
            Common,
            Uncommon,
            Rare,
            Epic,
            Legendary,
            Mythic
        }

        [Header("Visuals")]
        public Sprite eggSprite;
        public Sprite babySprite;
        public Sprite youngSprite;
        public Sprite adultSprite;
        public Sprite legendarySprite;

        [Header("Base Stats")]
        public int baseHealth = 100;
        public int baseAttack = 20;
        public int baseDefense = 10;
        public float baseSpeed = 5f;

        [Header("Growth")]
        public float growthMultiplier = 1f; // Влияет на скорость роста

        [Header("Breeding")]
        public DragonSpecies[] possibleOffspring; // Возможные результаты скрещивания
        public float breedingTime = 60f; // секунды

        public Sprite GetSpriteForStage(DragonStage stage)
        {
            return stage switch
            {
                DragonStage.Egg => eggSprite,
                DragonStage.Baby => babySprite,
                DragonStage.Young => youngSprite,
                DragonStage.Adult => adultSprite,
                DragonStage.Legendary => legendarySprite != null ? legendarySprite : adultSprite,
                _ => adultSprite
            };
        }
    }
}

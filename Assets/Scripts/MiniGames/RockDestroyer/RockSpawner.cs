using UnityEngine;

namespace DragonPark.MiniGames.RockDestroyer
{
    /// <summary>
    /// Спавнит камни в случайных местах.
    /// </summary>
    public class RockSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject rockPrefab;
        [SerializeField] private int rocksToSpawn = 12;
        [SerializeField] private Vector2 spawnAreaMin = new Vector2(-7f, -4f);
        [SerializeField] private Vector2 spawnAreaMax = new Vector2(7f, 4f);

        private void Start()
        {
            SpawnRocks();
        }

        private void SpawnRocks()
        {
            if (rockPrefab == null)
            {
                Debug.LogError("Rock Prefab не назначен!");
                return;
            }

            for (int i = 0; i < rocksToSpawn; i++)
            {
                Vector2 randomPos = new Vector2(
                    Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                    Random.Range(spawnAreaMin.y, spawnAreaMax.y)
                );

                Instantiate(rockPrefab, randomPos, Quaternion.identity, transform);
            }
        }
    }
}

using UnityEngine;

namespace DragonPark.MiniGames.CoinCollector
{
    /// <summary>
    /// Спавнит монеты в случайных местах в пределах заданной области.
    /// </summary>
    public class CoinSpawner : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private GameObject coinPrefab;

        [Header("Spawn Settings")]
        [SerializeField] private int coinsToSpawn = 15;
        [SerializeField] private Vector2 spawnAreaMin = new Vector2(-8f, -4f);
        [SerializeField] private Vector2 spawnAreaMax = new Vector2(8f, 4f);

        private void Start()
        {
            SpawnCoins();
        }

        private void SpawnCoins()
        {
            if (coinPrefab == null)
            {
                Debug.LogError("Coin Prefab не назначен в CoinSpawner!");
                return;
            }

            for (int i = 0; i < coinsToSpawn; i++)
            {
                Vector2 randomPos = new Vector2(
                    Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                    Random.Range(spawnAreaMin.y, spawnAreaMax.y)
                );

                Instantiate(coinPrefab, randomPos, Quaternion.identity, transform);
            }
        }
    }
}

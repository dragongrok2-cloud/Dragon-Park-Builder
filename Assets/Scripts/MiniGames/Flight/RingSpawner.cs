using UnityEngine;

namespace DragonPark.MiniGames.Flight
{
    /// <summary>
    /// Спавнит кольца впереди игрока по мере продвижения.
    /// </summary>
    public class RingSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject ringPrefab;
        [SerializeField] private float spawnInterval = 2.5f;
        [SerializeField] private float spawnDistance = 12f;
        [SerializeField] private float heightVariation = 3f;

        private float timer;
        private Transform player;

        private void Start()
        {
            // Ищем игрока по тегу
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        private void Update()
        {
            if (player == null || ringPrefab == null) return;

            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                timer = 0f;
                SpawnRing();
            }
        }

        private void SpawnRing()
        {
            float randomHeight = Random.Range(-heightVariation, heightVariation);
            Vector3 spawnPos = new Vector3(
                player.position.x + spawnDistance,
                randomHeight,
                0f
            );

            Instantiate(ringPrefab, spawnPos, Quaternion.identity);
        }
    }
}

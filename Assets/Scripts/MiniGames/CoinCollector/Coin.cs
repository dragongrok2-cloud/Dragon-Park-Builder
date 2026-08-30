using UnityEngine;

namespace DragonPark.MiniGames.CoinCollector
{
    /// <summary>
    /// Монета, которую можно собрать.
    /// </summary>
    public class Coin : MonoBehaviour
    {
        [SerializeField] private int value = 1;
        [SerializeField] private float rotationSpeed = 90f;

        public int Value => value;

        private void Update()
        {
            // Красивое вращение монетки
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // Сообщаем менеджеру, что монета собрана
                CoinCollectorManager.Instance?.CollectCoin(this);
                Destroy(gameObject);
            }
        }
    }
}

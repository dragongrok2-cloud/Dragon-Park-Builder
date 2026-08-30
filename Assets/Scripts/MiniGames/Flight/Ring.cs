using UnityEngine;

namespace DragonPark.MiniGames.Flight
{
    /// <summary>
    /// Кольцо, через которое нужно пролететь.
    /// </summary>
    public class Ring : MonoBehaviour
    {
        [SerializeField] private int points = 1;

        private bool collected = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (collected) return;

            if (other.CompareTag("Player"))
            {
                collected = true;
                FlightManager.Instance?.CollectRing(points);

                // Можно добавить эффект исчезновения
                Destroy(gameObject, 0.1f);
            }
        }
    }
}

using UnityEngine;

namespace DragonPark.MiniGames.RockDestroyer
{
    /// <summary>
    /// Камень, который можно разрушить кликом или ударом.
    /// </summary>
    public class Rock : MonoBehaviour
    {
        [SerializeField] private int health = 3;
        [SerializeField] private int points = 5;
        [SerializeField] private GameObject destroyEffect;

        public void TakeDamage(int damage = 1)
        {
            health -= damage;

            // Можно добавить анимацию тряски
            transform.localScale *= 0.9f;

            if (health <= 0)
            {
                DestroyRock();
            }
        }

        private void DestroyRock()
        {
            RockDestroyerManager.Instance?.AddScore(points);

            if (destroyEffect != null)
            {
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }

        private void OnMouseDown()
        {
            // Для прототипа — клик мышкой разрушает
            TakeDamage(1);
        }
    }
}

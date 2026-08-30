using UnityEngine;

namespace DragonPark.MiniGames.CoinCollector
{
    /// <summary>
    /// Простой контроллер игрока для мини-игры "Сбор монет".
    /// Управление: WASD или стрелки.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 5f;

        private Rigidbody2D rb;
        private Vector2 moveInput;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f; // для top-down
            rb.freezeRotation = true;
        }

        private void Update()
        {
            // Считываем ввод
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput = moveInput.normalized;
        }

        private void FixedUpdate()
        {
            rb.velocity = moveInput * moveSpeed;
        }
    }
}

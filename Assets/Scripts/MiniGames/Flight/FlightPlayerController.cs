using UnityEngine;

namespace DragonPark.MiniGames.Flight
{
    /// <summary>
    /// Управление драконом в мини-игре "Полёт".
    /// Простое вертикальное управление + постоянное движение вперёд.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class FlightPlayerController : MonoBehaviour
    {
        [Header("Flight Settings")]
        [SerializeField] private float forwardSpeed = 6f;
        [SerializeField] private float verticalSpeed = 5f;
        [SerializeField] private float maxHeight = 6f;
        [SerializeField] private float minHeight = -4f;

        private Rigidbody2D rb;
        private float verticalInput;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.freezeRotation = true;
        }

        private void Update()
        {
            verticalInput = Input.GetAxisRaw("Vertical"); // W/S или стрелки вверх/вниз
        }

        private void FixedUpdate()
        {
            // Постоянное движение вперёд
            Vector2 velocity = new Vector2(forwardSpeed, verticalInput * verticalSpeed);
            rb.velocity = velocity;

            // Ограничение по высоте
            Vector3 pos = transform.position;
            pos.y = Mathf.Clamp(pos.y, minHeight, maxHeight);
            transform.position = pos;
        }
    }
}

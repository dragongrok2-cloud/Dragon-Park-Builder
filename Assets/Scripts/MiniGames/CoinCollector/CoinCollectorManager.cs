using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro (рекомендуется)

namespace DragonPark.MiniGames.CoinCollector
{
    /// <summary>
    /// Главный менеджер мини-игры "Сбор монет".
    /// </summary>
    public class CoinCollectorManager : MonoBehaviour
    {
        public static CoinCollectorManager Instance { get; private set; }

        [Header("UI")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;

        [Header("Settings")]
        [SerializeField] private float gameDuration = 30f;
        [SerializeField] private int coinsToWin = 10;

        private int currentScore = 0;
        private float timeLeft;
        private bool isGameActive = true;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            timeLeft = gameDuration;
            UpdateUI();
            
            if (winPanel) winPanel.SetActive(false);
            if (losePanel) losePanel.SetActive(false);
        }

        private void Update()
        {
            if (!isGameActive) return;

            timeLeft -= Time.deltaTime;
            UpdateUI();

            if (timeLeft <= 0f)
            {
                timeLeft = 0f;
                EndGame(false);
            }
        }

        public void CollectCoin(Coin coin)
        {
            if (!isGameActive) return;

            currentScore += coin.Value;
            UpdateUI();

            if (currentScore >= coinsToWin)
            {
                EndGame(true);
            }
        }

        private void UpdateUI()
        {
            if (scoreText)
                scoreText.text = $"Монеты: {currentScore} / {coinsToWin}";

            if (timerText)
                timerText.text = $"Время: {Mathf.CeilToInt(timeLeft)}";
        }

        private void EndGame(bool won)
        {
            isGameActive = false;

            if (won && winPanel)
                winPanel.SetActive(true);
            else if (!won && losePanel)
                losePanel.SetActive(true);

            // Можно добавить паузу времени или отключение управления
            Time.timeScale = 0f;
        }

        // Кнопка "Играть снова"
        public void RestartGame()
        {
            Time.timeScale = 1f;
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}

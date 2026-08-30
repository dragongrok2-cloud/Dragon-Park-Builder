using UnityEngine;
using TMPro;

namespace DragonPark.MiniGames.Flight
{
    /// <summary>
    /// Менеджер мини-игры "Полёт через кольца".
    /// </summary>
    public class FlightManager : MonoBehaviour
    {
        public static FlightManager Instance { get; private set; }

        [Header("UI")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;

        [Header("Settings")]
        [SerializeField] private float gameDuration = 45f;
        [SerializeField] private int ringsToWin = 12;

        private int currentScore = 0;
        private float timeLeft;
        private bool isGameActive = true;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
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
                EndGame(currentScore >= ringsToWin);
            }
        }

        public void CollectRing(int points)
        {
            if (!isGameActive) return;

            currentScore += points;
            UpdateUI();

            if (currentScore >= ringsToWin)
            {
                EndGame(true);
            }
        }

        private void UpdateUI()
        {
            if (scoreText)
                scoreText.text = $"Кольца: {currentScore} / {ringsToWin}";

            if (timerText)
                timerText.text = $"Время: {Mathf.CeilToInt(timeLeft)}";
        }

        private void EndGame(bool won)
        {
            isGameActive = false;
            Time.timeScale = 0f;

            if (won && winPanel) winPanel.SetActive(true);
            else if (!won && losePanel) losePanel.SetActive(true);
        }

        public void RestartGame()
        {
            Time.timeScale = 1f;
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}

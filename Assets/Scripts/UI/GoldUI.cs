using UnityEngine;
using TMPro;
using DragonPark.Core;

namespace DragonPark.UI
{
    /// <summary>
    /// Простой UI для отображения золота.
    /// </summary>
    public class GoldUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI goldText;

        private void Start()
        {
            if (CurrencyManager.Instance != null)
            {
                CurrencyManager.Instance.OnGoldChanged += UpdateGold;
                UpdateGold(CurrencyManager.Instance.Gold);
            }
        }

        private void UpdateGold(int amount)
        {
            if (goldText != null)
                goldText.text = $"Золото: {amount}";
        }

        private void OnDestroy()
        {
            if (CurrencyManager.Instance != null)
                CurrencyManager.Instance.OnGoldChanged -= UpdateGold;
        }
    }
}

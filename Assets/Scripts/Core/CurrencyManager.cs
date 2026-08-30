using UnityEngine;
using System;

namespace DragonPark.Core
{
    /// <summary>
    /// Простой менеджер валюты (золото).
    /// </summary>
    public class CurrencyManager : MonoBehaviour
    {
        public static CurrencyManager Instance { get; private set; }

        [Header("Starting Values")]
        [SerializeField] private int startingGold = 500;

        public int Gold { get; private set; }

        public event Action<int> OnGoldChanged;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                Gold = startingGold;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public bool HasEnoughGold(int amount)
        {
            return Gold >= amount;
        }

        public bool SpendGold(int amount)
        {
            if (!HasEnoughGold(amount))
            {
                Debug.Log("Недостаточно золота!");
                return false;
            }

            Gold -= amount;
            OnGoldChanged?.Invoke(Gold);
            Debug.Log($"Потрачено {amount} золота. Осталось: {Gold}");
            return true;
        }

        public void AddGold(int amount)
        {
            if (amount <= 0) return;

            Gold += amount;
            OnGoldChanged?.Invoke(Gold);
            Debug.Log($"Получено {amount} золота. Всего: {Gold}");
        }
    }
}

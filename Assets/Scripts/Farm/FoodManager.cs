using UnityEngine;
using System.Collections.Generic;
using System;

namespace DragonPark.Farm
{
    /// <summary>
    /// Менеджер еды: инвентарь и покупка.
    /// </summary>
    public class FoodManager : MonoBehaviour
    {
        public static FoodManager Instance { get; private set; }

        [Header("Available Foods")]
        public List<FoodItem> availableFoods = new List<FoodItem>();

        // Простой инвентарь: название еды → количество
        private Dictionary<string, int> inventory = new Dictionary<string, int>();

        public event Action OnInventoryChanged;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitDefaultFoods();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitDefaultFoods()
        {
            if (availableFoods.Count == 0)
            {
                availableFoods.Add(new FoodItem { foodName = "Яблоко", hungerRestore = 15, happinessBonus = 3, cost = 8, growTime = 20f });
                availableFoods.Add(new FoodItem { foodName = "Мясо", hungerRestore = 35, happinessBonus = 8, cost = 25, growTime = 45f });
                availableFoods.Add(new FoodItem { foodName = "Магический фрукт", hungerRestore = 50, happinessBonus = 20, cost = 60, growTime = 90f });
            }
        }

        public bool BuyFood(FoodItem food, int amount = 1)
        {
            if (food == null || amount <= 0) return false;

            int totalCost = food.cost * amount;

            if (CurrencyManager.Instance == null || !CurrencyManager.Instance.SpendGold(totalCost))
            {
                return false;
            }

            AddToInventory(food.foodName, amount);
            return true;
        }

        public void AddToInventory(string foodName, int amount)
        {
            if (inventory.ContainsKey(foodName))
                inventory[foodName] += amount;
            else
                inventory[foodName] = amount;

            OnInventoryChanged?.Invoke();
        }

        public bool UseFood(string foodName, out FoodItem foodData)
        {
            foodData = availableFoods.Find(f => f.foodName == foodName);

            if (foodData == null || !inventory.ContainsKey(foodName) || inventory[foodName] <= 0)
                return false;

            inventory[foodName]--;
            if (inventory[foodName] <= 0)
                inventory.Remove(foodName);

            OnInventoryChanged?.Invoke();
            return true;
        }

        public int GetAmount(string foodName)
        {
            return inventory.ContainsKey(foodName) ? inventory[foodName] : 0;
        }

        public Dictionary<string, int> GetInventory()
        {
            return new Dictionary<string, int>(inventory);
        }
    }
}

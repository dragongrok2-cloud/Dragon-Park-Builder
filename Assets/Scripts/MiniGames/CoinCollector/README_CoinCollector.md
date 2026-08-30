# Мини-игра: Сбор монет (Coin Collector)

Простой прототип мини-игры для **Dragon Park Builder**.

## Как запустить в Unity

1. Открой проект в **Unity 2022.3 LTS** или новее (рекомендуется).
2. Создай новую 2D-сцену: `File → New Scene → 2D`.
3. Сохрани сцену как `Assets/Scenes/MiniGames/CoinCollector.unity`.

### Настройка объектов

#### 1. Игрок (Player)
- Создай пустой GameObject → назови `Player`
- Добавь компоненты:
  - `Sprite Renderer` (можно поставить любой квадрат/круг пока)
  - `Rigidbody2D` (Gravity Scale = 0)
  - `Circle Collider 2D` или `Box Collider 2D` (Is Trigger = **false**)
  - Скрипт `PlayerController`
- Поставь тег **Player**

#### 2. Монета (Coin Prefab)
- Создай GameObject → `Coin`
- Добавь:
  - `Sprite Renderer` (жёлтый круг/монетка)
  - `Circle Collider 2D` → **Is Trigger = true**
  - Скрипт `Coin`
- Перетащи в папку `Assets/Prefabs/MiniGames/` и сделай Prefab

#### 3. Менеджер игры
- Создай пустой GameObject → `GameManager`
- Добавь скрипт `CoinCollectorManager`
- Создай UI (Canvas):
  - Text (TMP) для счёта → привяжи к `scoreText`
  - Text (TMP) для таймера → привяжи к `timerText`
  - Две панели (Win / Lose) с кнопкой "Играть снова", которая вызывает `RestartGame()`

#### 4. Спавнер монет
- Создай пустой GameObject → `CoinSpawner`
- Добавь скрипт `CoinSpawner`
- Перетащи Prefab монеты в поле `Coin Prefab`

### Управление
- **WASD** или **Стрелки** — движение

### Цель
Собрать нужное количество монет до истечения времени!

---

Это только первый прототип. Потом добавим красивых драконов, частицы, звуки и интеграцию в основной парк.

# Мини-игра: Полёт через кольца (Flight)

Второй прототип мини-игры для **Dragon Park Builder**.

## Суть
Дракон постоянно летит вперёд. Игрок управляет только высотой (W/S или стрелки вверх/вниз) и должен пролетать через кольца.

## Как настроить в Unity

1. Создай новую 2D-сцену → сохрани как `Assets/Scenes/MiniGames/Flight.unity`

2. **Игрок**
   - GameObject с тегом `Player`
   - Sprite Renderer
   - Rigidbody2D (Gravity Scale = 0)
   - Collider2D
   - Скрипт `FlightPlayerController`

3. **Кольцо (Prefab)**
   - GameObject с Collider2D (Is Trigger = true)
   - Скрипт `Ring`
   - Сделай Prefab

4. **Менеджер**
   - Пустой объект + `FlightManager`
   - Привяжи UI (счёт, таймер, панели победы/поражения)

5. **Спавнер**
   - Пустой объект + `RingSpawner`
   - Перетащи Prefab кольца

Управление: **W / S** или **стрелки вверх / вниз**

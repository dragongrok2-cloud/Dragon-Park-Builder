# Система скрещивания и виды драконов

## DragonSpecies (ScriptableObject)

Создавай новые виды через меню:
**Create → Dragon Park → Dragon Species**

В каждом виде можно указать:
- Название, описание, элемент
- Редкость
- Спрайты для всех стадий
- Базовые характеристики
- Возможное потомство (possibleOffspring)
- Время скрещивания

## BreedingManager

- Метод `TryBreed(Dragon parent1, Dragon parent2)`
- Требования: оба дракона взрослые + счастье ≥ 40
- События: OnBreedingStarted, OnBreedingCompleted

## Как использовать

1. Создай несколько DragonSpecies assets
2. Назначь species на объект Dragon
3. Вызови `BreedingManager.Instance.TryBreed(dragon1, dragon2)`

Позже можно добавить UI выбора родителей и красивую анимацию яйца.

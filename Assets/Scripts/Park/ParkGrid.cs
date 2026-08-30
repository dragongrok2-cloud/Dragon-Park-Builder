using UnityEngine;
using System.Collections.Generic;

namespace DragonPark.Park
{
    /// <summary>
    /// Простая сетка парка для размещения зданий.
    /// </summary>
    public class ParkGrid : MonoBehaviour
    {
        [Header("Grid Settings")]
        public int width = 20;
        public int height = 20;
        public float cellSize = 1f;

        private Building[,] grid;

        private void Awake()
        {
            grid = new Building[width, height];
        }

        public bool CanPlace(Vector2Int position, Vector2Int size)
        {
            if (position.x < 0 || position.y < 0 ||
                position.x + size.x > width || position.y + size.y > height)
            {
                return false;
            }

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    if (grid[position.x + x, position.y + y] != null)
                        return false;
                }
            }

            return true;
        }

        public bool PlaceBuilding(Building building, Vector2Int position)
        {
            if (!CanPlace(position, building.size))
                return false;

            for (int x = 0; x < building.size.x; x++)
            {
                for (int y = 0; y < building.size.y; y++)
                {
                    grid[position.x + x, position.y + y] = building;
                }
            }

            // Позиционируем объект в мире
            Vector3 worldPos = GridToWorld(position, building.size);
            building.transform.position = worldPos;
            building.OnPlaced();

            return true;
        }

        public void RemoveBuilding(Vector2Int position)
        {
            Building building = grid[position.x, position.y];
            if (building == null) return;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (grid[x, y] == building)
                        grid[x, y] = null;
                }
            }

            building.OnRemoved();
            Destroy(building.gameObject);
        }

        public Vector3 GridToWorld(Vector2Int gridPos, Vector2Int size)
        {
            float x = (gridPos.x + size.x * 0.5f) * cellSize;
            float z = (gridPos.y + size.y * 0.5f) * cellSize;
            return new Vector3(x, 0f, z);
        }

        public Vector2Int WorldToGrid(Vector3 worldPos)
        {
            int x = Mathf.FloorToInt(worldPos.x / cellSize);
            int y = Mathf.FloorToInt(worldPos.z / cellSize);
            return new Vector2Int(x, y);
        }

        // Для отладки — рисуем сетку в редакторе
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.gray;
            for (int x = 0; x <= width; x++)
            {
                Vector3 start = new Vector3(x * cellSize, 0, 0);
                Vector3 end = new Vector3(x * cellSize, 0, height * cellSize);
                Gizmos.DrawLine(start, end);
            }
            for (int y = 0; y <= height; y++)
            {
                Vector3 start = new Vector3(0, 0, y * cellSize);
                Vector3 end = new Vector3(width * cellSize, 0, y * cellSize);
                Gizmos.DrawLine(start, end);
            }
        }
    }
}

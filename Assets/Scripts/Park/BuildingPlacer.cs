using UnityEngine;

namespace DragonPark.Park
{
    /// <summary>
    /// Позволяет размещать здания мышкой (для прототипа).
    /// </summary>
    public class BuildingPlacer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ParkGrid parkGrid;
        [SerializeField] private Camera mainCamera;

        [Header("Placement")]
        [SerializeField] private Building buildingPrefab; // Временно один префаб
        [SerializeField] private LayerMask groundLayer;

        private Building currentPreview;
        private bool isPlacing = false;

        private void Start()
        {
            if (mainCamera == null)
                mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                StartPlacing();
            }

            if (isPlacing)
            {
                UpdatePreview();

                if (Input.GetMouseButtonDown(0))
                {
                    TryPlace();
                }

                if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
                {
                    CancelPlacing();
                }
            }
        }

        public void StartPlacing()
        {
            if (buildingPrefab == null || parkGrid == null) return;

            isPlacing = true;
            currentPreview = Instantiate(buildingPrefab);
            // Можно сделать полупрозрачным
            SetPreviewMaterial(currentPreview, true);
        }

        private void UpdatePreview()
        {
            if (currentPreview == null) return;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
            {
                Vector2Int gridPos = parkGrid.WorldToGrid(hit.point);
                Vector3 worldPos = parkGrid.GridToWorld(gridPos, currentPreview.size);
                currentPreview.transform.position = worldPos;

                bool canPlace = parkGrid.CanPlace(gridPos, currentPreview.size);
                SetPreviewColor(currentPreview, canPlace ? Color.green : Color.red);
            }
        }

        private void TryPlace()
        {
            if (currentPreview == null) return;

            Vector2Int gridPos = parkGrid.WorldToGrid(currentPreview.transform.position);

            if (parkGrid.PlaceBuilding(currentPreview, gridPos))
            {
                SetPreviewMaterial(currentPreview, false);
                currentPreview = null;
                isPlacing = false;
            }
            else
            {
                Debug.Log("Нельзя разместить здание здесь!");
            }
        }

        private void CancelPlacing()
        {
            if (currentPreview != null)
            {
                Destroy(currentPreview.gameObject);
                currentPreview = null;
            }
            isPlacing = false;
        }

        private void SetPreviewMaterial(Building building, bool isPreview)
        {
            // Простая заглушка — можно улучшить позже
        }

        private void SetPreviewColor(Building building, Color color)
        {
            var renderer = building.GetComponentInChildren<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }
        }
    }
}

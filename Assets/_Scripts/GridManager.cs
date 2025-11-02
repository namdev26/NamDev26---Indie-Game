using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int gridWidth = 7;
    [SerializeField] private int gridHeight = 7;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private float spacing = 0.1f;
    [SerializeField] private GameObject tilePrefab;

    [Header("Grid Origin (tile 0,0 nằm tại đây)")]
    [SerializeField] private Vector3 gridOrigin = Vector3.zero;

    private Transform gridParent;

    private void Start()
    {
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        if (tilePrefab == null)
        {
            Debug.LogError("⚠️ Tile Prefab chưa được gán trong GridManager!");
            return;
        }

        if (gridParent != null)
            DestroyImmediate(gridParent.gameObject);

        gridParent = new GameObject("GridParent").transform;
        gridParent.SetParent(transform);
        gridParent.position = gridOrigin;

        float totalCellSize = cellSize + spacing;

        // 🔹 Không căn giữa — bắt đầu từ (0,0)
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 worldPos = new Vector3(
                    gridOrigin.x + x * totalCellSize,
                    gridOrigin.y,
                    gridOrigin.z + y * totalCellSize
                );

                GameObject tile = Instantiate(tilePrefab, worldPos, Quaternion.identity, gridParent);
                tile.name = $"Tile_{x}_{y}";
            }
        }
    }

    public void SetGridPosition(Vector3 newOrigin)
    {
        gridOrigin = newOrigin;
        if (gridParent != null)
            gridParent.position = gridOrigin;
    }

    // ==== Getter ====
    public float GetCellSize() => cellSize;
    public float GetSpacing() => spacing;
    public int GetGridWidth() => gridWidth;
    public int GetGridHeight() => gridHeight;
    public Vector3 GetGridOrigin() => gridOrigin;
}

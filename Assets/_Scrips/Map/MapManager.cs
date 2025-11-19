using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Map Settings")]
    public int width = 20;
    public int height = 20;
    public float cellSize = 1.1f;
    public Vector3 origin = Vector3.zero;

    [Header("Prefabs")]
    public GameObject grassPrefab;
    public GameObject soilPrefab;

    private TileData[,] grid;

    public enum EditMode
    {
        None,
        SoilMode
    }

    public EditMode currentMode = EditMode.None;

    void Start()
    {
        GenerateMap();
        Debug.Log("=== Map Generated ===");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("CLICK DETECTED - Mode: " + currentMode);
            HandleTileClick();
        }
    }

    void GenerateMap()
    {
        grid = new TileData[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 pos = new Vector3(
                    origin.x + x * cellSize,
                    origin.y,
                    origin.z + z * cellSize
                );

                GameObject tileObj = Instantiate(grassPrefab, pos, Quaternion.identity, transform);

                TileData td = tileObj.GetComponent<TileData>();
                td.x = x;
                td.z = z;

                grid[x, z] = td;

                Debug.Log($"Tile Created [{x},{z}] at {pos}");
            }
        }
    }

    void HandleTileClick()
    {
        if (currentMode == EditMode.None)
        {
            Debug.Log("Click ignored: NO MODE SELECTED");
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f);

        if (Physics.Raycast(ray, out RaycastHit hit, 200f))
        {
            Debug.Log("Raycast HIT: " + hit.collider.name + " at world pos: " + hit.point);

            TileData tile = hit.collider.GetComponent<TileData>();

            if (tile == null)
            {
                Debug.Log("❌ Hit object has NO TileData");
                return;
            }

            Debug.Log($"TileData FOUND: tile({tile.x},{tile.z}), occupied={tile.occupied}");

            switch (currentMode)
            {
                case EditMode.SoilMode:
                    ConvertGrassToSoil(tile);
                    break;
            }
        }
        else
        {
            Debug.Log("❌ Raycast hit NOTHING");
        }
    }

    void ConvertGrassToSoil(TileData tile)
    {
        Debug.Log($"--- Converting Tile [{tile.x},{tile.z}] to SOIL ---");

        if (tile.occupied)
        {
            Debug.Log("❌ Tile already occupied");
            return;
        }

        GameObject soil = Instantiate(soilPrefab, tile.transform.position, Quaternion.identity);
        tile.occupied = true;
        tile.occupant = soil;

        Debug.Log($"✔ Tile [{tile.x},{tile.z}] converted SUCCESSFULLY");

        Destroy(tile.gameObject);
    }

    // BUTTON CALL
    public void ActivateSoilMode()
    {
        currentMode = EditMode.SoilMode;
        Debug.Log("=== Soil Mode Enabled ===");
    }
}

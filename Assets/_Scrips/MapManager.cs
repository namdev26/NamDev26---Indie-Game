using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Map Settings")]
    public int width = 20;
    public int height = 20;
    public float cellSize = 1f;
    public Vector3 origin = new Vector3(0, 0.223f, 0);

    public bool digMode = false;

    public Grid gridLayout;

    [Header("Prefabs")]
    public GameObject grassPrefab;
    public GameObject soilPrefab;

    private TileData[,] grid;

    void Start()
    {
        GenerateMap();
    }

    private void Update()
    {
        if (digMode && Input.GetMouseButtonDown(0))
        {
            TryPlaceSoil();
        }
    }

    void GenerateMap()
    {
        grid = new TileData[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3Int cell = new Vector3Int(x, 0, z);
                Vector3 pos = gridLayout.CellToWorld(cell);
                pos.y = origin.y;

                GameObject tileObj = Instantiate(grassPrefab, pos, Quaternion.identity, transform);
                tileObj.name = $"Tile_{x}_{z}";
                grid[x, z] = tileObj.GetComponent<TileData>();
            }
        }
    }

    public void ActivateDigMode()
    {
        digMode = !digMode; // TOGGLE
        Debug.Log("Dig mode: " + digMode);
    }

    void TryPlaceSoil()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, origin);

        if (plane.Raycast(ray, out float dist))
        {
            Vector3 hit = ray.GetPoint(dist);
            hit.y = origin.y;

            Vector3Int cell = gridLayout.WorldToCell(hit);
            int x = cell.x;
            int z = cell.z;

            if (x < 0 || x >= width || z < 0 || z >= height)
                return;

            TileData tile = grid[x, z];
            if (tile == null) return;

            if (tile.occupied) return;

            GameObject oldGrass = tile.gameObject;

            GameObject soil = Instantiate(soilPrefab, oldGrass.transform.position, Quaternion.identity);

            tile.occupied = true;
            tile.occupant = soil;

            Destroy(oldGrass);
            Debug.Log($"Soil placed at [{x},{z}]");

        }
    }
}

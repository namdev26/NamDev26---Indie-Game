using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Map Size")]
    public int width = 30;
    public int height = 30;

    [Header("Colors")]
    public Color grassColor = new Color(0.8f, 1f, 0.8f);
    public Color soilDryColor = new Color(0.7f, 0.55f, 0.3f);   // soil moisture = 0
    public Color soilWetColor = new Color(0.45f, 0.25f, 0.1f);  // soil moisture = 1
    public Color hoverColor = new Color(1f, 1f, 0.7f);

    [Header("Settings")]
    public float cellSize = 1f;
    public Vector3 origin = Vector3.zero;
    public Material terrainMaterial;

    public Vector2Int hoverTile = new Vector2Int(-1, -1);

    public enum EditMode { None, SoilMode }
    public EditMode currentMode = EditMode.None;

    private MapMesh mesh;

    public TileData[,] tiles;

    void Start()
    {
        // init tile data
        tiles = new TileData[width, height];
        for (int x = 0; x < width; x++)
            for (int z = 0; z < height; z++)
                tiles[x, z] = new TileData();

        mesh = gameObject.AddComponent<MapMesh>();
        mesh.Init(this);
    }

    void Update()
    {
        DetectHoverTile(); // luôn cho thấy hover

        if (Input.GetMouseButtonDown(0)) // click ô
            OnTileClicked();
    }

    private void OnTileClicked()
    {
        if (hoverTile.x == -1) return;

        int x = hoverTile.x;
        int z = hoverTile.y;

        switch (currentTool)
        {
            case ToolMode.RemoveSoil:
                RemoveSoil(x, z);
                break;

            case ToolMode.Plant:
                TryPlant(x, z);
                break;

            case ToolMode.CreateSoil:   // 👈 thêm
                CreateSoil(x, z);
                break;

            case ToolMode.None:
            default:
                break;
        }
    }

    private void DetectHoverTile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, 200f))
        {
            ClearHover();
            return;
        }

        Vector3 p = hit.point;

        int x = Mathf.FloorToInt((p.x - origin.x) / cellSize);
        int z = Mathf.FloorToInt((p.z - origin.z) / cellSize);

        if (x < 0 || x >= width || z < 0 || z >= height)
        {
            ClearHover();
            return;
        }

        hoverTile = new Vector2Int(x, z);
        mesh.UpdateHoverTile();
    }

    private void ClearHover()
    {
        hoverTile = new Vector2Int(-1, -1);
        mesh.UpdateHoverTile();
    }

    private void ApplySoil()
    {
        if (hoverTile.x == -1) return;

        tiles[hoverTile.x, hoverTile.y].isSoil = true;
        tiles[hoverTile.x, hoverTile.y].moisture = 1f; // mới đào là ẩm 100%

        mesh.UpdateTileColor(hoverTile.x, hoverTile.y);
    }

    public void RemoveSoil(int x, int z)
    {
        tiles[x, z].isSoil = false;
        tiles[x, z].moisture = 0;
        tiles[x, z].hasPlant = false;

        mesh.UpdateTileColor(x, z);
    }

    public void MoveSoil(Vector2Int from, Vector2Int to)
    {
        TileData a = tiles[from.x, from.y];
        TileData b = tiles[to.x, to.y];

        b.isSoil = a.isSoil;
        b.moisture = a.moisture;
        b.hasPlant = a.hasPlant;

        a.isSoil = false;
        a.moisture = 0;
        a.hasPlant = false;

        mesh.UpdateTileColor(from.x, from.y);
        mesh.UpdateTileColor(to.x, to.y);
    }

    public bool TryPlant(int x, int z)
    {
        if (!tiles[x, z].isSoil) return false;

        tiles[x, z].hasPlant = true;

        return true;
    }

    public enum ToolMode
    {
        None,
        RemoveSoil,
        Plant,
        CreateSoil
    }

    public ToolMode currentTool = ToolMode.None;
    public void CreateSoil(int x, int z)
    {
        tiles[x, z].isSoil = true;
        tiles[x, z].moisture = 1f;   // mới tạo luôn ẩm 100%
        tiles[x, z].hasPlant = false;

        mesh.UpdateTileColor(x, z);
    }

}

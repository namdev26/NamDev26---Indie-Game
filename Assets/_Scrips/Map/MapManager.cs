using UnityEngine;
using UnityEngine.AI;

public class MapManager : MonoBehaviour
{
    public int width = 30;
    public int height = 30;

    public Color grassColor = new Color(0.8f, 1f, 0.8f);
    public Color soilColor = new Color(0.6f, 0.4f, 0.2f);
    public Color hoverColor = new Color(1f, 1f, 0.7f);

    public Vector2Int hoverTile = new Vector2Int(-1, -1);

    public float cellSize = 1f;
    public Vector3 origin = Vector3.zero;

    public Material terrainMaterial;

    public enum TileType { Grass, Soil }
    public TileType[,] map;

    private MapMesh mesh;

    public enum EditMode { None, SoilMode }
    public EditMode currentMode = EditMode.None;

    void Start()
    {
        // init map
        map = new TileType[width, height];
        for (int x = 0; x < width; x++)
            for (int z = 0; z < height; z++)
                map[x, z] = TileType.Grass;

        mesh = gameObject.AddComponent<MapMesh>();
        mesh.Init(this);
    }

    void Update()
    {
        if (currentMode == EditMode.SoilMode)
            DetectHoverTile();
        else
            ClearHover();

        if (Input.GetMouseButtonDown(0))
            ApplySoil();
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

        map[hoverTile.x, hoverTile.y] = TileType.Soil;

        mesh.UpdateTileColor(hoverTile.x, hoverTile.y);
    }

    public void ToggleSoilMode()
    {
        currentMode =
            currentMode == EditMode.SoilMode ?
            EditMode.None :
            EditMode.SoilMode;

        if (currentMode == EditMode.None)
            ClearHover();
    }
}

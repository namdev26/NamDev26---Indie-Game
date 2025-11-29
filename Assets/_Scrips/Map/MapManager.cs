using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private MapConfig config;
    [SerializeField] private Vector3 origin = Vector3.zero;

    public MapConfig Config => config;
    public Vector3 Origin => origin;
    public TileMap TileMap { get; private set; }
    public Vector2Int HoverTile { get; private set; } = new Vector2Int(-1, -1);

    private MapMesh mesh;
    private Camera mainCamera;
    private ITileModifier currentTool;

    public event System.Action<int, int> OnTileChanged;

    private void Start()
    {
        mainCamera = Camera.main;
        TileMap = new TileMap(config.width, config.height);

        mesh = gameObject.AddComponent<MapMesh>();
        mesh.Init(this);
    }

    private void Update()
    {
        UpdateHoverTile();

        if (Input.GetMouseButtonDown(0) && HoverTile.x >= 0 && currentTool != null)
        {
            currentTool.Execute(TileMap, HoverTile.x, HoverTile.y);
            NotifyTileChanged(HoverTile.x, HoverTile.y);
        }

        if (FarmUI.Instance != null && FarmUI.Instance.uiOpen)
            return;
    }

    public void SetTool(ITileModifier tool) => currentTool = tool;

    public void MoveSoil(Vector2Int from, Vector2Int to)
    {
        if (!TileMap.IsValidPosition(from.x, from.y) ||
            !TileMap.IsValidPosition(to.x, to.y)) return;

        TileMap.GetTile(to.x, to.y).CopyFrom(TileMap.GetTile(from.x, from.y));
        TileMap.GetTile(from.x, from.y).Clear();

        NotifyTileChanged(from.x, from.y);
        NotifyTileChanged(to.x, to.y);
    }

    private void NotifyTileChanged(int x, int z)
    {
        mesh.UpdateTileColor(x, z);
        OnTileChanged?.Invoke(x, z);
    }

    private void UpdateHoverTile()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, 200f))
        {
            SetHover(-1, -1);
            return;
        }

        int x = Mathf.FloorToInt((hit.point.x - origin.x) / config.tileSize);
        int z = Mathf.FloorToInt((hit.point.z - origin.z) / config.tileSize);

        if (!TileMap.IsValidPosition(x, z))
        {
            SetHover(-1, -1);
            return;
        }

        SetHover(x, z);
    }

    private void SetHover(int x, int z)
    {
        HoverTile = new Vector2Int(x, z);
        mesh.UpdateHoverTile();
    }
}
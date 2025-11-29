using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private MapConfig config;
    [SerializeField] private Vector3 origin = Vector3.zero;

    public MapConfig Config => config;
    public Vector3 Origin => origin;
    public TileMap TileMap { get; private set; }
    public Vector2Int HoverTile { get; private set; } = new Vector2Int(-1, -1);

    private Camera mainCamera;
    private ITileModifier currentTool;

    private GridSpawner spawner;

    public event System.Action<int, int> OnTileChanged;

    private void Start()
{
    mainCamera = Camera.main;

    TileMap = new TileMap(config.width, config.height);

    spawner = GetComponent<GridSpawner>();
    spawner.Init(this);
}



    private void Update()
    {
        // Hover raycast
        UpdateHoverTile();

        // Nếu UI mở → ngưng click map
        if (FarmUI.Instance != null && FarmUI.Instance.uiOpen)
            return;

        // Click tile
        if (Input.GetMouseButtonDown(0) && HoverTile.x >= 0 && currentTool != null)
        {
            currentTool.Execute(TileMap, HoverTile.x, HoverTile.y);
            NotifyTileChanged(HoverTile.x, HoverTile.y);
        }
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
        var tile = TileMap.GetTile(x, z);

        // CHUYỂN DATAMODEL → PREFAB
        spawner.SetTiletype(x, z, tile.type);

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

        int x = Mathf.FloorToInt((hit.point.x - origin.x + config.tileSize) / config.tileSize);
        int z = Mathf.FloorToInt((hit.point.z - origin.z + config.tileSize) / config.tileSize) - 1;


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
    }
}

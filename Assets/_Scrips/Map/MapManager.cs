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
    private BaseTool currentTool;

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
        UpdateHoverTile();

        if (FarmUI.Instance != null && FarmUI.Instance.uiOpen)
            return;

        if (Input.GetMouseButtonDown(0) && HoverTile.x >= 0 && currentTool != null)
        {
            Vector3 wp = TileToWorld(HoverTile.x, HoverTile.y);
            currentTool.OnPointerDown(wp);
        }

        if (Input.GetMouseButton(0) && HoverTile.x >= 0 && currentTool != null)
        {
            Vector3 wp = TileToWorld(HoverTile.x, HoverTile.y);
            currentTool.OnPointerHold(wp);
        }
    }

    public void SetTool(BaseTool tool)
    {
        currentTool?.OnToolDeselected();
        currentTool = tool;
        currentTool?.OnToolSelected();
    }

    public Vector3 TileToWorld(int x, int z)
    {
        return new Vector3(
            origin.x + x * config.tileSize,
            0,
            origin.z + z * config.tileSize
        );
    }

    public void NotifyTileChanged(int x, int z)
    {
        var tile = TileMap.GetTile(x, z);
        spawner.SetTiletype(x, z, tile.type);
        OnTileChanged?.Invoke(x, z);
    }

    private void UpdateHoverTile()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, 200f))
        {
            HoverTile = new Vector2Int(-1, -1);
            return;
        }

        int x = Mathf.FloorToInt((hit.point.x - origin.x + config.tileSize) / config.tileSize);
        int z = Mathf.FloorToInt((hit.point.z - origin.z + config.tileSize) / config.tileSize) - 1;

        if (!TileMap.IsValidPosition(x, z))
        {
            HoverTile = new Vector2Int(-1, -1);
            return;
        }

        HoverTile = new Vector2Int(x, z);
    }
}

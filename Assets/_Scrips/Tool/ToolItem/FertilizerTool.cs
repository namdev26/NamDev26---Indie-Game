using UnityEngine;

public class FertilizerTool : BaseTool
{
    private MapManager map;
    private FertilizerType type;

    public FertilizerTool(MapManager mapManager, FertilizerType fertilizerType)
    {
        map = mapManager;
        type = fertilizerType;
    }

    public override string ToolName => "Fertilizer";

    public override void OnPointerDown(Vector3 worldPos)
    {
        var tilePos = map.HoverTile;
        var tile = map.TileMap.GetTile(tilePos.x, tilePos.y);

        if (tile.type != TileType.Soil)
            return;

        switch (type)
        {
            case FertilizerType.Speed:
                tile.fertilizerSpeed = 1f;
                break;

            case FertilizerType.Yield:
                tile.fertilizerYield = 1f;
                break;
        }

        Vector3 center = map.TileToWorld(tilePos.x, tilePos.y);
        center.x -= map.Config.tileSize * 0.5f;
        center.z += map.Config.tileSize * 0.5f;

        FertilizerVisual.Instance.SpawnDots(tilePos, center, type);

        map.NotifyTileChanged(tilePos.x, tilePos.y);
    }

}

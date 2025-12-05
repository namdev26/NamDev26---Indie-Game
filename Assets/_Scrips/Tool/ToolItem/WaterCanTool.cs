using UnityEngine;

public class WaterCanTool : BaseTool
{
    private MapManager map;

    public WaterCanTool(MapManager map)
    {
        this.map = map;
    }

    public override void OnPointerDown(Vector3 worldPos)
    {
        var tilePos = map.HoverTile;
        if (tilePos.x < 0) return;

        var tileData = map.TileMap.GetTile(tilePos.x, tilePos.y);

        if (tileData.type != TileType.Soil) return;

        tileData.moisture = 1f;

        map.NotifyTileChanged(tilePos.x, tilePos.y); // cập nhật màu
    }
}

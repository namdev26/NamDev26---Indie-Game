using UnityEngine;

public class ShovelTool : BaseTool
{
    private MapManager map;

    public ShovelTool(MapManager map)
    {
        this.map = map;
    }

    public override string ToolName => "Shovel";

    public override void OnPointerDown(Vector3 worldPos)
    {
        var tilePos = map.HoverTile;
        if (tilePos.x < 0) return;

        var tile = map.TileMap.GetTile(tilePos.x, tilePos.y);
        if (tile == null) return;

        // N?u ?ang có plant trong tile ? có th? check rule sau
        tile.Clear();

        map.NotifyTileChanged(tilePos.x, tilePos.y);
    }
}

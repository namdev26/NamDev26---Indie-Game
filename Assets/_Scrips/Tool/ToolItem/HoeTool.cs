
using UnityEngine;

public class HoeTool : BaseTool
{
    private MapManager map;

    public HoeTool(MapManager map)
    {
        this.map = map;
    }

    public override string ToolName => "Hoe";

    public override void OnPointerDown(Vector3 worldPos)
    {
        var tilePos = map.HoverTile;

        if (tilePos.x < 0) return;

        var tile = map.TileMap.GetTile(tilePos.x, tilePos.y);
        tile.SetSoil();

        map.NotifyTileChanged(tilePos.x, tilePos.y);
    }
}

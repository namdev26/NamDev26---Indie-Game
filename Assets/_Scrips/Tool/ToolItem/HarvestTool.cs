using UnityEngine;
using System.Collections.Generic;

public class HarvestTool : BaseTool
{
    private readonly MapManager map;
    private readonly PlantManager plantManager;

    // tránh x? lý l?p m?t ô khi kéo chu?t qua
    private HashSet<Vector2Int> harvestedTiles = new HashSet<Vector2Int>();

    public HarvestTool(MapManager mapManager, PlantManager plantMgr)
    {
        map = mapManager;
        plantManager = plantMgr;
    }

    public override string ToolName => "Harvest";

    public override void OnToolSelected()
    {
        harvestedTiles.Clear();
    }

    public override void OnPointerDown(Vector3 worldPos)
    {
        TryHarvest(map.HoverTile);
    }

    public override void OnPointerHold(Vector3 worldPos)
    {
        TryHarvest(map.HoverTile);
    }

    private void TryHarvest(Vector2Int tilePos)
    {
        if (tilePos.x < 0) return;
        if (harvestedTiles.Contains(tilePos)) return;

        harvestedTiles.Add(tilePos);

        // ki?m tra có plant không
        var plant = plantManager.GetPlantAt(tilePos);
        if (plant == null) return;

        // ki?m tra ?ã chín ch?a
        if (!plant.IsGrown()) return;

        // th?c hi?n thu ho?ch
        plantManager.HarvestAt(tilePos.x, tilePos.y);

        // c?p nh?t visual
        map.NotifyTileChanged(tilePos.x, tilePos.y);
    }
}

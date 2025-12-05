using UnityEngine;
using System.Collections.Generic;

public class HarvestTool : BaseTool
{
    private readonly MapManager map;
    private readonly PlantManager plantManager;

    // tr�nh x? l� l?p m?t � khi k�o chu?t qua
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

        var plant = plantManager.GetPlantAt(tilePos);
        if (plant == null) return;

        if (!plant.IsGrown()) return;

        plantManager.HarvestAt(tilePos.x, tilePos.y);

        map.NotifyTileChanged(tilePos.x, tilePos.y);
    }
}
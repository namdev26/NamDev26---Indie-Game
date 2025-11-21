using UnityEngine;

public class PlantTool : ITileModifier
{
    public void Execute(ITileMap map, int x, int z)
    {
        if (!map.IsValidPosition(x, z)) return;
        var tile = map.GetTile(x, z);
        if (tile.isSoil) tile.hasPlant = true;
    }
}
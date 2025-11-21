using UnityEngine;

public class CreateSoilTool : ITileModifier
{
    public void Execute(ITileMap map, int x, int z)
    {
        if (!map.IsValidPosition(x, z)) return;
        map.GetTile(x, z).SetSoil();
    }
}

public class RemoveSoilTool : ITileModifier
{
    public void Execute(ITileMap map, int x, int z)
    {
        if (!map.IsValidPosition(x, z)) return;
        map.GetTile(x, z).Clear();
    }
}
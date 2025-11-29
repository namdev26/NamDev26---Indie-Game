using UnityEngine;

public class ShovelTool : ITileModifier
{
    public void Execute(ITileMap map, int x, int z)
    {
        var tile = map.GetTile(x, z);

        if (tile == null) return;

        tile.Clear();
    }
}

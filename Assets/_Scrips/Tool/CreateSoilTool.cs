public class CreateSoilTool : ITileModifier
{
    public void Execute(ITileMap map, int x, int z)
    {
        var tile = map.GetTile(x, z);
        tile.isSoil = true;
    }
}

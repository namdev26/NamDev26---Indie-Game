public interface ITileMap
{
    int Width { get; }
    int Height { get; }
    TileData GetTile(int x, int z);
    bool IsValidPosition(int x, int z);
}
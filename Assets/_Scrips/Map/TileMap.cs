using UnityEngine;

public class TileMap : ITileMap
{
    private readonly TileData[,] tiles;
    private readonly GameObject[,] tileObjects;

    public int Width { get; }
    public int Height { get; }

    public TileMap(int width, int height)
    {
        Width = width;
        Height = height;

        tiles = new TileData[width, height];
        tileObjects = new GameObject[width, height];

        for (int x = 0; x < width; x++)
            for (int z = 0; z < height; z++)
                tiles[x, z] = new TileData();
    }

    public void SetTileObject(int x, int z, GameObject obj)
    {
        tileObjects[x, z] = obj;
    }

    public GameObject GetTileObject(int x, int z)
    {
        return tileObjects[x, z];
    }

    public TileData GetTile(int x, int z) => tiles[x, z];

    public bool IsValidPosition(int x, int z) =>
        x >= 0 && x < Width && z >= 0 && z < Height;
}

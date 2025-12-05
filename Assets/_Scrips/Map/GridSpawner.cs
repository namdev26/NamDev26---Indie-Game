using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public GameObject grassPrefab;
    public GameObject soilPrefab;

    private MapManager map;
    private GameObject[,] tileObjects;

    public void Init(MapManager mapManager)
    {
        map = mapManager;

        int w = map.Config.width;
        int h = map.Config.height;

        tileObjects = new GameObject[w, h];

        for (int x = 0; x < w; x++)
        {
            for (int z = 0; z < h; z++)
            {
                SpawnGrass(x, z);
            }
        }
    }

    void SpawnGrass(int x, int z)
    {
        Vector3 pos = new Vector3(
            map.Origin.x + x * map.Config.tileSize,
            0f,
            map.Origin.z + z * map.Config.tileSize
        );

        var go = Instantiate(grassPrefab, pos, Quaternion.identity, transform);
        tileObjects[x, z] = go;

        map.TileMap.SetTileObject(x, z, go);

        var tileData = map.TileMap.GetTile(x, z);
        tileData.type = TileType.Grass;

        var renderer = go.GetComponent<TileRenderer>();
        if (renderer != null)
            renderer.Init(tileData);
    }


    public void SetTiletype(int x, int z, TileType type)
    {
        GameObject prefab = type == TileType.Soil ? soilPrefab : grassPrefab;
        ReplaceTile(x, z, prefab, type);
    }

    private void ReplaceTile(int x, int z, GameObject prefab, TileType type)
    {
        // Xóa tile cũ
        if (tileObjects[x, z] != null)
            Destroy(tileObjects[x, z]);

        Vector3 pos = new Vector3(
            map.Origin.x + x * map.Config.tileSize,
            0f,
            map.Origin.z + z * map.Config.tileSize
        );

        var go = Instantiate(prefab, pos, Quaternion.identity, transform);
        tileObjects[x, z] = go;

        map.TileMap.SetTileObject(x, z, go);

        var tileData = map.TileMap.GetTile(x, z);
        tileData.type = type;

        var renderer = go.GetComponent<TileRenderer>();
        if (renderer != null)
            renderer.Init(tileData);
    }

}

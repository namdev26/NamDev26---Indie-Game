using UnityEngine;

public class GrassMapGenerator : MonoBehaviour
{
    [Header("Map Settings")]
    public int width = 20;
    public int height = 20;
    public float tileSize = 1f;

    [Header("Tile Prefab")]
    public GameObject grassTilePrefab;

    private GameObject[,] spawnedTiles;

    void Start()
    {
        GenerateGrassMap();
    }

    public void GenerateGrassMap()
    {
        if (grassTilePrefab == null)
        {
            Debug.LogError("GrassTilePrefab chưa được gán!");
            return;
        }

        spawnedTiles = new GameObject[width, height];

        Vector3 origin = transform.position;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(
                    origin.x + x * tileSize,
                    origin.y,
                    origin.z + y * tileSize
                );

                GameObject tile = Instantiate(grassTilePrefab, pos, Quaternion.identity, transform);
                spawnedTiles[x, y] = tile;

                // đảm bảo tile luôn scale chuẩn
                tile.transform.localScale = Vector3.one;
            }
        }

        Debug.Log("Map grass đã tạo xong!");
    }
}

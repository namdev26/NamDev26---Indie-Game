//using UnityEngine;

//public class MapMesh : MonoBehaviour
//{
//    private MapManager map;
//    private MapConfig config;
//    private TileMap tileMap;

//    private MeshFilter mf;
//    private MeshRenderer mr;
//    private MeshCollider col;
//    private Mesh mesh;

//    private Vector3[] vertices;
//    private int[] triangles;
//    private Color[] colors;

//    public void Init(MapManager m)
//    {
//        map = m;
//        config = m.Config;
//        tileMap = m.TileMap;

//        mf = gameObject.AddComponent<MeshFilter>();
//        mr = gameObject.AddComponent<MeshRenderer>();
//        col = gameObject.AddComponent<MeshCollider>();

//        mesh = new Mesh();
//        mf.sharedMesh = mesh;
//        mr.sharedMaterial = config.terrainMaterial;

//        BuildMesh();
//    }

//    private void BuildMesh()
//    {
//        int w = tileMap.Width;
//        int h = tileMap.Height;

//        vertices = new Vector3[w * h * 4];
//        triangles = new int[w * h * 6];
//        colors = new Color[w * h * 4];

//        int v = 0, t = 0;

//        for (int x = 0; x < w; x++)
//        {
//            for (int z = 0; z < h; z++)
//            {
//                float px = map.Origin.x + x * config.tileSize;
//                float pz = map.Origin.z + z * config.tileSize;

//                vertices[v + 0] = new Vector3(px, 0, pz);
//                vertices[v + 1] = new Vector3(px + config.tileSize, 0, pz);
//                vertices[v + 2] = new Vector3(px, 0, pz + config.tileSize);
//                vertices[v + 3] = new Vector3(px + config.tileSize, 0, pz + config.tileSize);

//                colors[v + 0] = config.grassColor;
//                colors[v + 1] = config.grassColor;
//                colors[v + 2] = config.grassColor;
//                colors[v + 3] = config.grassColor;

//                triangles[t + 0] = v + 0;
//                triangles[t + 1] = v + 2;
//                triangles[t + 2] = v + 1;
//                triangles[t + 3] = v + 1;
//                triangles[t + 4] = v + 2;
//                triangles[t + 5] = v + 3;

//                v += 4;
//                t += 6;
//            }
//        }

//        mesh.Clear();
//        mesh.vertices = vertices;
//        mesh.triangles = triangles;
//        mesh.colors = colors;
//        mesh.RecalculateNormals();
//        mesh.RecalculateBounds();
//        col.sharedMesh = mesh;
//    }

//    public void UpdateTileColor(int x, int z)
//    {
//        int index = (x * tileMap.Height + z) * 4;
//        TileData tile = tileMap.GetTile(x, z);

//        Color c = tile.isSoil
//            ? Color.Lerp(config.soilDryColor, config.soilWetColor, tile.moisture)
//            : config.grassColor;

//        colors[index + 0] = c;
//        colors[index + 1] = c;
//        colors[index + 2] = c;
//        colors[index + 3] = c;

//        mesh.colors = colors;
//    }

//    public void UpdateHoverTile()
//    {
//        // Reset tất cả tile về màu gốc
//        for (int x = 0; x < tileMap.Width; x++)
//            for (int z = 0; z < tileMap.Height; z++)
//                SetTileColor(x, z, GetTileBaseColor(x, z));

//        // Áp dụng hover color
//        Vector2Int hover = map.HoverTile;
//        if (hover.x >= 0)
//            SetTileColor(hover.x, hover.y, config.hoverColor);

//        mesh.colors = colors;
//    }

//    private Color GetTileBaseColor(int x, int z)
//    {
//        TileData tile = tileMap.GetTile(x, z);
//        return tile.isSoil
//            ? Color.Lerp(config.soilDryColor, config.soilWetColor, tile.moisture)
//            : config.grassColor;
//    }

//    private void SetTileColor(int x, int z, Color c)
//    {
//        int index = (x * tileMap.Height + z) * 4;
//        colors[index + 0] = c;
//        colors[index + 1] = c;
//        colors[index + 2] = c;
//        colors[index + 3] = c;
//    }
//}
using UnityEngine;

public class MapMesh : MonoBehaviour
{
    private MapManager map;

    private MeshFilter mf;
    private MeshRenderer mr;
    private MeshCollider col;

    private Mesh mesh;

    private Vector3[] vertices;
    private int[] triangles;
    private Color[] colors;

    public void Init(MapManager m)
    {
        map = m;

        mf = gameObject.AddComponent<MeshFilter>();
        mr = gameObject.AddComponent<MeshRenderer>();
        col = gameObject.AddComponent<MeshCollider>();

        mesh = new Mesh();
        mf.sharedMesh = mesh;
        mr.sharedMaterial = map.terrainMaterial;

        BuildMesh();
    }

    private void BuildMesh()
    {
        int w = map.width;
        int h = map.height;

        vertices = new Vector3[w * h * 4];
        triangles = new int[w * h * 6];
        colors = new Color[w * h * 4];

        int v = 0;
        int t = 0;

        for (int x = 0; x < w; x++)
        {
            for (int z = 0; z < h; z++)
            {
                float px = map.origin.x + x * map.cellSize;
                float pz = map.origin.z + z * map.cellSize;

                vertices[v + 0] = new Vector3(px, 0, pz);
                vertices[v + 1] = new Vector3(px + map.cellSize, 0, pz);
                vertices[v + 2] = new Vector3(px, 0, pz + map.cellSize);
                vertices[v + 3] = new Vector3(px + map.cellSize, 0, pz + map.cellSize);

                Color c = map.grassColor;
                colors[v + 0] = c;
                colors[v + 1] = c;
                colors[v + 2] = c;
                colors[v + 3] = c;

                triangles[t + 0] = v + 0;
                triangles[t + 1] = v + 2;
                triangles[t + 2] = v + 1;
                triangles[t + 3] = v + 1;
                triangles[t + 4] = v + 2;
                triangles[t + 5] = v + 3;

                v += 4;
                t += 6;
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.RecalculateBounds();

        col.sharedMesh = mesh;
    }

    // update 1 tile màu đất
    public void UpdateTileColor(int x, int z)
    {
        int index = (x * map.height + z) * 4;

        Color c = map.soilColor;

        colors[index + 0] = c;
        colors[index + 1] = c;
        colors[index + 2] = c;
        colors[index + 3] = c;

        mesh.colors = colors;
    }

    // update hover highlight
    public void UpdateHoverTile()
    {
        // reset toàn bộ màu
        for (int i = 0; i < colors.Length; i++)
            colors[i] = map.grassColor;

        // apply soil
        for (int x = 0; x < map.width; x++)
            for (int z = 0; z < map.height; z++)
                if (map.map[x, z] == MapManager.TileType.Soil)
                    UpdateTileColor(x, z);

        // apply hover
        if (map.hoverTile.x != -1)
        {
            int x = map.hoverTile.x;
            int z = map.hoverTile.y;

            int index = (x * map.height + z) * 4;
            colors[index + 0] = map.hoverColor;
            colors[index + 1] = map.hoverColor;
            colors[index + 2] = map.hoverColor;
            colors[index + 3] = map.hoverColor;
        }

        mesh.colors = colors;
    }
}

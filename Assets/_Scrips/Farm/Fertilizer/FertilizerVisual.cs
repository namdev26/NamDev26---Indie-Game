using UnityEngine;
using System.Collections.Generic;

public class FertilizerVisual : MonoBehaviour
{
    public static FertilizerVisual Instance;

    [Header("Prefabs cho từng loại phân")]
    public GameObject speedDotPrefab;
    public GameObject yieldDotPrefab;

    [Header("Số hạt tối đa sau khi bón")]
    public int dotCount = 5;

    // Lưu danh sách hạt theo tile
    private Dictionary<Vector2Int, List<GameObject>> dotsOnTile
        = new Dictionary<Vector2Int, List<GameObject>>();

    private void Awake()
    {
        Instance = this;
    }

    // SPAWN DOTS TRÊN TILE
    public void SpawnDots(Vector2Int tilePos, Vector3 tileCenter, FertilizerType type)
    {
        ClearDots(tilePos);

        GameObject prefab = null;

        switch (type)
        {
            case FertilizerType.Speed:
                prefab = speedDotPrefab;
                break;

            case FertilizerType.Yield:
                prefab = yieldDotPrefab;
                break;
        }

        if (prefab == null) return;

        List<GameObject> list = new List<GameObject>();

        for (int i = 0; i < dotCount; i++)
        {
            Vector3 offset = new Vector3(
                Random.Range(-0.4f, 0.4f),
                0.015f,
                Random.Range(-0.4f, 0.4f)
            );

            GameObject dot = Instantiate(prefab, tileCenter + offset, Quaternion.identity);

            // random scale tạo cảm giác tự nhiên
            dot.transform.localScale *= Random.Range(0.6f, 1f);

            list.Add(dot);
        }

        dotsOnTile[tilePos] = list;
    }

    // CLEAR DOTS KHI MẤT PHÂN
    public void ClearDots(Vector2Int tilePos)
    {
        if (!dotsOnTile.ContainsKey(tilePos))
            return;

        foreach (var dot in dotsOnTile[tilePos])
        {
            if (dot != null)
                Destroy(dot);
        }

        dotsOnTile.Remove(tilePos);
    }
}

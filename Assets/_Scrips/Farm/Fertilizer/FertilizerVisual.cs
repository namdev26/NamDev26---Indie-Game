using UnityEngine;
using System.Collections.Generic;

public class FertilizerVisual : MonoBehaviour
{
    public static FertilizerVisual Instance;

    [Header("Prefabs cho từng loại phân")]
    public GameObject speedDotPrefab;
    public GameObject yieldDotPrefab;

    [Header("Số hạt mỗi loại")]
    public int dotCount = 5;

    // Lưu theo tile → theo type → danh sách object
    private Dictionary<Vector2Int, Dictionary<FertilizerType, List<GameObject>>> dotsOnTile
        = new Dictionary<Vector2Int, Dictionary<FertilizerType, List<GameObject>>>();

    private void Awake()
    {
        Instance = this;
    }

    // =====================================================================
    // SPAWN DOTS CHO LOẠI PHÂN CỤ THỂ
    // =====================================================================
    public void SpawnDots(Vector2Int tilePos, Vector3 center, FertilizerType type)
    {
        // Nếu tile chưa có entry → tạo dictionary con
        if (!dotsOnTile.ContainsKey(tilePos))
            dotsOnTile[tilePos] = new Dictionary<FertilizerType, List<GameObject>>();

        // Nếu tile đã có hạt của riêng loại này → xoá trước (để không bị double)
        ClearDots(tilePos, type);

        GameObject prefab = GetPrefab(type);
        if (prefab == null) return;

        List<GameObject> list = new List<GameObject>();

        for (int i = 0; i < dotCount; i++)
        {
            Vector3 offset = new Vector3(
                Random.Range(-0.25f, 0.25f),
                0.02f,
                Random.Range(-0.25f, 0.25f)
            );

            GameObject dot = Instantiate(prefab, center + offset, Quaternion.identity);
            dot.transform.localScale *= Random.Range(0.7f, 1.2f);

            list.Add(dot);
        }

        dotsOnTile[tilePos][type] = list;
    }

    // =====================================================================
    // CLEAR DOTS CHO MỘT LOẠI CỤ THỂ
    // =====================================================================
    public void ClearDots(Vector2Int tilePos, FertilizerType type)
    {
        if (!dotsOnTile.ContainsKey(tilePos)) return;
        if (!dotsOnTile[tilePos].ContainsKey(type)) return;

        foreach (var dot in dotsOnTile[tilePos][type])
            if (dot != null) Destroy(dot);

        dotsOnTile[tilePos].Remove(type);
    }

    // CLEAR TẤT CẢ LOẠI PHÂN TRÊN TILE
    public void ClearAll(Vector2Int tilePos)
    {
        if (!dotsOnTile.ContainsKey(tilePos)) return;

        foreach (var kv in dotsOnTile[tilePos])
            foreach (var dot in kv.Value)
                if (dot != null) Destroy(dot);

        dotsOnTile.Remove(tilePos);
    }

    private GameObject GetPrefab(FertilizerType type)
    {
        switch (type)
        {
            case FertilizerType.Speed: return speedDotPrefab;
            case FertilizerType.Yield: return yieldDotPrefab;
        }
        return null;
    }
}

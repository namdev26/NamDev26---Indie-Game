using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class HarvestDrop
{
    public ShopItemData item;
    public int quantity;
    [Header("Product (if harvest item is Seed, this product will be created instead)")]
    public ShopItemData productItem; // Sản phẩm tương ứng khi thu hoạch
}

[CreateAssetMenu(fileName = "NewPlant", menuName = "Game/PlantData")]
public class PlantData : ScriptableObject
{
    public string plantName;

    public HarvestDrop harvest;

    public List<PlantStageData> stages = new List<PlantStageData>();

    public int harvestAmount = 1;

    public PlantStageData GetStage(int index)
    {
        if (index < 0 || index >= stages.Count)
            return null;

        return stages[index];
    }

    public float GetGrowthTime(int index)
    {
        var s = GetStage(index);
        return s != null ? s.growthTime : 0f;
    }

    public GameObject GetPrefab(int index)
    {
        var s = GetStage(index);
        return s != null ? s.prefab : null;
    }
}

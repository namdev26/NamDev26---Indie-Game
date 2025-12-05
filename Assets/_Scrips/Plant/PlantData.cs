using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewPlant", menuName = "Game/PlantData")]
public class PlantData : ScriptableObject
{
    public string plantName;

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

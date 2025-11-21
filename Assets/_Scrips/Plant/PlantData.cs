using JetBrains.Annotations;
using UnityEngine;

public enum PlantStage { Small, Medium, Large }

[CreateAssetMenu(fileName = "NewPlant", menuName = "Game/PlantData")]
public class PlantData : ScriptableObject
{
    public string plantName;
    public GameObject[] stagePrefabs = new GameObject[3];
    public float growthTimeS = 5f; 
    public float growthTimeM = 8f;
    public int harvestAmount = 1;

    public float GetGrowthTime(PlantStage stage) => stage switch
    {
        PlantStage.Small => growthTimeS,
        PlantStage.Medium => growthTimeM,
        _ => 0f
    };

    public GameObject GetObjPrefab(PlantStage stage) => stagePrefabs[(int)stage];
}

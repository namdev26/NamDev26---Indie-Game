using UnityEngine;

public class PlantInstance
{
    public PlantData Data { get; }
    public PlantStage Stage { get; private set; }
    public float GrowthTimer { get; private set; }
    public Vector2Int Position { get; }

    public bool CanHarvest => Stage == PlantStage.Large;
    public bool CanGrow => Stage != PlantStage.Large;

    public PlantInstance(PlantData data, Vector2Int position)
    {
        Data = data;
        Position = position;
        Stage = PlantStage.Small;
        GrowthTimer = 0f;
    }

    public bool TryGrow(float deltaTime)
    {
        if (!CanGrow) return false;

        GrowthTimer += deltaTime;
        float required = Data.GetGrowthTime(Stage);

        if (GrowthTimer >= required)
        {
            GrowthTimer = 0f;
            Stage++;
            return true; // ?ã chuy?n giai ?o?n
        }
        return false;
    }

    public float GetGrowthProgress()
    {
        if (!CanGrow) return 1f;
        return GrowthTimer / Data.GetGrowthTime(Stage);
    }
}
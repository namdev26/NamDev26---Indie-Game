using UnityEngine;

public class PlantInstance
{
    public PlantData Data { get; }
    public int StageIndex { get; private set; }
    public float GrowthTimer { get; private set; }
    public Vector2Int Position { get; }

    public bool CanHarvest => StageIndex >= Data.stages.Count - 1;
    public bool CanGrow => StageIndex < Data.stages.Count - 1;

    // Giữ logic cũ
    public bool IsGrown() => StageIndex == Data.stages.Count - 1;

    public PlantInstance(PlantData data, Vector2Int position)
    {
        Data = data;
        Position = position;
        StageIndex = 0;
        GrowthTimer = 0f;
    }

    public bool TryGrow(float deltaTime)
    {
        if (!CanGrow) return false;

        GrowthTimer += deltaTime;
        float required = Data.GetGrowthTime(StageIndex);

        if (required <= 0f) return false;

        if (GrowthTimer >= required)
        {
            GrowthTimer = 0f;
            StageIndex++;

            if (StageIndex >= Data.stages.Count)
                StageIndex = Data.stages.Count - 1;

            return true;
        }

        return false;
    }

    public float GetGrowthProgress()
    {
        if (!CanGrow) return 1f;

        float required = Data.GetGrowthTime(StageIndex);
        if (required <= 0f) return 1f;

        return GrowthTimer / required;
    }

    public int GetHarvestStage()
    {
        return StageIndex;
    }
}

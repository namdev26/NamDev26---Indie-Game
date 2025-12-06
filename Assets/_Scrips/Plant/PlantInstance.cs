using UnityEngine;

public class PlantInstance
{
    public PlantData Data { get; }
    public int StageIndex { get; private set; }
    public float GrowthTimer { get; private set; }
    public Vector2Int Position { get; }

    public int harvestItemId;
    public int harvestQuantity;

    public bool CanHarvest => StageIndex >= Data.stages.Count - 1;
    public bool CanGrow => StageIndex < Data.stages.Count - 1;

    public bool IsGrown() => StageIndex == Data.stages.Count - 1;

    public PlantInstance(PlantData data, Vector2Int position)
    {
        Data = data;
        Position = position;
        StageIndex = 0;
        GrowthTimer = 0f;
    }

    public float GetGrowthSpeed()
    {
        TileData tile = MapManager.Instance.TileMap.GetTile(Position.x, Position.y);

        float moistureBonus = tile.moisture * 0.5f;        // từ hệ thống nước
        float fertilizerBonus = tile.fertilizerSpeed * 1f; // SPEED fertilizer

        return 1f + moistureBonus + fertilizerBonus;
    }


    public bool TryGrow(float deltaTime)
    {
        if (!CanGrow) return false;

        // ⭐ Áp dụng tăng tốc độ
        float speed = GetGrowthSpeed();

        GrowthTimer += deltaTime * speed;

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

    public int GetFinalHarvestQuantity()
    {
        TileData tile = MapManager.Instance.TileMap.GetTile(Position.x, Position.y);

        int baseQty = Data.harvest.quantity;
        int bonusQty = Mathf.RoundToInt(tile.fertilizerYield * 2f); // +2 sản phẩm

        return baseQty + bonusQty;
    }
}

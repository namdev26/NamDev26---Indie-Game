using UnityEngine;

[System.Serializable]
public class TileData
{
    public TileType type = TileType.Grass;

    public float moisture;
    public bool hasPlant;

    public void SetSoil(float moistureValue = 1f)
    {
        type = TileType.Soil;
        moisture = moistureValue;
        hasPlant = false;
    }

    public bool HasPlant()
    {
        return hasPlant;
    }

    public void Clear()
    {
        type = TileType.Grass;
        moisture = 0f;
        hasPlant = false;
    }

    public void CopyFrom(TileData other)
    {
        type = other.type;
        moisture = other.moisture;
        hasPlant = other.hasPlant;
    }
}

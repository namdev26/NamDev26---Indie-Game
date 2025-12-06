using UnityEngine;

[System.Serializable]
public class TileData
{
    public TileType type = TileType.Grass;

    public float moisture; // độ ẩm đất
    public bool hasPlant; // có cây trồng hay không

    public float fertilizerSpeed;   // 0 = không có, 1 = Speed Fertilizer max
    public float fertilizerYield;   // 0 = không có, 1 = Yield Fertilizer max


    public void SetSoil(float moistureValue = 1f)
    {
        type = TileType.Soil;

        // đất mới được cày sẽ có độ ẩm ban đầu
        moisture = moistureValue;
        hasPlant = false;

        // đất mới auto không có phân bón
        fertilizerSpeed = 0f;
        fertilizerYield = 0f;
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

        fertilizerSpeed = 0f;
        fertilizerYield = 0f;
    }

    public void CopyFrom(TileData other)
    {
        type = other.type;
        moisture = other.moisture;
        hasPlant = other.hasPlant;

        fertilizerSpeed = other.fertilizerSpeed;
        fertilizerYield = other.fertilizerYield;
    }
}

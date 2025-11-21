using UnityEngine;

[System.Serializable]
public class TileData
{
    public bool isSoil;
    public float moisture;
    public bool hasPlant;

    public void SetSoil(float moistureValue = 1f)
    {
        isSoil = true;
        moisture = moistureValue;
        hasPlant = false;
    }

    public void Clear()
    {
        isSoil = false;
        moisture = 0f;
        hasPlant = false;
    }

    public void CopyFrom(TileData other)
    {
        isSoil = other.isSoil;
        moisture = other.moisture;
        hasPlant = other.hasPlant;
    }
}
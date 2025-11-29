using UnityEngine;

public enum TileType
{
    Grass,
    Soil,
    Water,       // để mở rộng tương lai
    Path,        // đường đi
    Rock         // đá chắn
}


public class Tile : MonoBehaviour
{
    public TileType type;
}

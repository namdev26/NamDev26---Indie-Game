using UnityEngine;

[CreateAssetMenu(menuName = "Game/Farm/Seed Item")]
public class SeedItem : ScriptableObject
{
    public string seedName;
    public Sprite icon;
    public int price;
    public string description;

    [Header("Plant output")]
    public PlantData plantData;
}

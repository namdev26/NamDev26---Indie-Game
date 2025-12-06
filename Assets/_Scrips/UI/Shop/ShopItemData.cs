using UnityEngine;

public enum ItemType
{
    Seed,
    Crop,
    Tool,
    Consumable,
    Fertilizer
}


[CreateAssetMenu(menuName = "Game/Farm/Shop Item")]
public class ShopItemData : ScriptableObject
{
    public string id;
    public string itemName;
    public Sprite icon;
    public int price;
    public string description;

    public ItemType itemType;

    public SeedItem seedItem;
    public ProductItem productItem;
    public ToolData toolData;

    public FertilizerType fertilizerType = FertilizerType.None;
}

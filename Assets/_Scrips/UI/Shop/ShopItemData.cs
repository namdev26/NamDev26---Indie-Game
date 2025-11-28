using UnityEngine;

public enum ItemType
{
    Seed,
    Tool,
    Consumable,
    Other
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
}

using UnityEngine;

[CreateAssetMenu(menuName = "Game/Farm/Shop Item")]
public class ShopItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int price;
    public string description;

    public SeedItem seedItem;
}

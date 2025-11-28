using System;

[Serializable]
public class InventoryItem
{
    public ShopItemData itemData;
    public int quantity;

    public InventoryItem(ShopItemData itemData, int quantity)
    {
        this.itemData = itemData;
        this.quantity = quantity;
    }
}

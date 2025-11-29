using System;

[Serializable]
public class InventoryItem
{
    public ShopItemData itemData;
    public int quantity;

    // NEW: dành cho Tool
    public int durability;

    public InventoryItem(ShopItemData itemData, int quantity)
    {
        this.itemData = itemData;
        this.quantity = quantity;

        // nếu item là Tool thì khởi tạo durability
        if (itemData.itemType == ItemType.Tool)
        {
            durability = itemData.toolData.maxDurability;
        }
    }
}

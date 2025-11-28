using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public Action OnInventoryChanged;

    private void Awake()
    {
        Instance = this;
    }

    public List<InventoryItem> items = new List<InventoryItem>();

    public void AddItem(ShopItemData itemData, int quantity)
    {
        var found = items.Find(i => i.itemData.id == itemData.id);

        if (found != null)
            found.quantity += quantity;
        else
            items.Add(new InventoryItem(itemData, quantity));

        Debug.Log($"Added {quantity} x {itemData.itemName}.");

        OnInventoryChanged?.Invoke();
    }

    public bool UseItem(InventoryItem invItem)
    {
        if (invItem == null) return false;

        bool result = false;

        switch (invItem.itemData.itemType)
        {
            case ItemType.Seed:
                result = UseSeed(invItem);
                break;

            case ItemType.Consumable:
                result = Consume(invItem);
                break;

            case ItemType.Tool:
                Debug.Log("Tool selected – not consumed.");
                result = true;
                break;

            default:
                result = Consume(invItem);
                break;
        }

        OnInventoryChanged?.Invoke();

        return result;
    }

    private bool UseSeed(InventoryItem invItem)
    {
        if (invItem.itemData.seedItem == null)
        {
            Debug.LogError("Seed item does not contain SeedItem data!");
            return false;
        }

        invItem.quantity--;

        if (invItem.quantity <= 0)
            items.Remove(invItem);

        return true;
    }

    private bool Consume(InventoryItem invItem)
    {
        invItem.quantity--;

        if (invItem.quantity <= 0)
            items.Remove(invItem);

        return true;
    }

    public bool HasItem(ShopItemData data)
    {
        foreach (var item in items)
            if (item.itemData == data && item.quantity > 0)
                return true;

        return false;
    }
}

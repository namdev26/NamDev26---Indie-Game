public class PlantTool : ITileModifier
{
    private readonly PlantManager plantManager;
    private readonly InventoryItem inventoryItem; // lấy từ HotbarSlot
    private readonly PlantData plantData;

    public PlantTool(PlantManager manager, PlantData data, InventoryItem invItem)
    {
        plantManager = manager;
        plantData = data;
        inventoryItem = invItem;
    }

    public void Execute(ITileMap map, int x, int z)
    {
        bool planted = plantManager.TryPlant(plantData, x, z);

        if (!planted) return;

        Inventory.Instance.UseItem(inventoryItem);

        HotbarManager.Instance.RefreshHotbarUI();

        if (inventoryItem.quantity <= 0)
            HotbarManager.Instance.DeselectTool();
    }
}

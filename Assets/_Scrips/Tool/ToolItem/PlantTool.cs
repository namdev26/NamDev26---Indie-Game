using UnityEngine;

public class PlantTool : BaseTool
{
    private readonly PlantManager plantManager;
    private readonly InventoryItem inventoryItem;
    private readonly PlantData plantData;
    private readonly MapManager map;

    public PlantTool(MapManager mapManager, PlantManager manager, PlantData data, InventoryItem invItem)
    {
        map = mapManager;
        plantManager = manager;
        plantData = data;
        inventoryItem = invItem;
    }

    public override string ToolName => $"Plant: {plantData.name}";

    public override void OnPointerDown(Vector3 worldPos)
    {
        var tilePos = map.HoverTile;
        if (tilePos.x < 0) return;

        // trồng cây
        bool planted = plantManager.TryPlant(plantData, tilePos.x, tilePos.y);
        if (!planted) return;

        // dùng inventory
        Inventory.Instance.UseItem(inventoryItem);

        // cập nhật UI hotbar
        HotbarManager.Instance.RefreshHotbarUI();

        // tự tắt tool nếu hết hạt
        if (inventoryItem.quantity <= 0)
            HotbarManager.Instance.DeselectTool();

        // cập nhật visual tile
        map.NotifyTileChanged(tilePos.x, tilePos.y);
    }
}

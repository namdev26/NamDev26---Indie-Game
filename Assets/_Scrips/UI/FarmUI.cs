using UnityEngine;

public class FarmUI : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private MapManager map;
    [SerializeField] private PlantManager plantManager;
    [SerializeField] private OrthoIsoCameraController cameraController;

    public static FarmUI Instance;
    public bool uiOpen = false;

    private void Awake()
    {
        Instance = this;
    }

    private void SetUIState(bool state)
    {
        uiOpen = state;

        if (cameraController != null)
            cameraController.uiOpen = state;

        // Không được dùng tool khi UI đang mở
        if (state)
            map.SetTool(null);
    }

    // ===== UI BUTTONS =====
    public void SelectButtonShop()
    {
        bool newState = !shopPanel.activeSelf;
        shopPanel.SetActive(newState);
        SetUIState(newState);
    }

    public void SelectButtonInventory()
    {
        bool newState = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(newState);
        SetUIState(newState);
    }

    public void SelectNone()
    {
        if (uiOpen) return;

        map.SetTool(null);
        Debug.Log("Tool: None");
    }

    // ===========================================================
    //                 SELECT ITEM FROM INVENTORY
    // ===========================================================
    public void SelectToolFromItem(InventoryItem item)
    {
        if (uiOpen) return;
        if (item == null || item.itemData == null) return;

        ShopItemData data = item.itemData;

        // =======================================================
        //                         SEED
        // =======================================================
        if (data.itemType == ItemType.Seed)
        {
            PlantData plantData = data.seedItem.plantData;
            map.SetTool(new PlantTool(map, plantManager, plantData, item));

            Debug.Log("Tool: Plant " + plantData.name);
            return;
        }

        // =======================================================
        //                         TOOL
        // =======================================================
        if (data.itemType == ItemType.Tool)
        {
            switch (data.toolData.toolType)
            {
                case ToolType.Hoe:
                    map.SetTool(new HoeTool(map));
                    Debug.Log("Tool: Hoe");
                    break;

                case ToolType.Shovel:
                    map.SetTool(new ShovelTool(map));
                    Debug.Log("Tool: Shovel");
                    break;

                case ToolType.Sickle:
                    map.SetTool(new HarvestTool(map, plantManager));
                    Debug.Log("Tool: Scythe");
                    break;

                case ToolType.WaterCan:
                    map.SetTool(new WaterCanTool(map));
                    Debug.Log("Tool: Watering Can");
                    break;

                default:
                    Debug.LogWarning("Unknown tool type!");
                    break;
            }

            return;
        }

        if (data.itemType == ItemType.Fertilizer)
        {
            FertilizerType fertType = data.fertilizerType;

            if (fertType == FertilizerType.None)
            {
                Debug.LogError("Fertilizer item has FertilizerType.None!");
                return;
            }

            // Equip fertilizer tool
            map.SetTool(new FertilizerTool(map, fertType));

            Debug.Log("Tool: Fertilizer (" + fertType + ")");
            return;
        }

        Debug.Log("Selected non-tool item.");
    }

    // ===== SELECT FERTILIZER TOOL =====
    public void SelectFertilizerTool(FertilizerType type)
    {
        if (uiOpen) return;

        var tool = new FertilizerTool(map, type);
        map.SetTool(tool);

        Debug.Log("Tool: Fertilizer (" + type + ")");
    }

}

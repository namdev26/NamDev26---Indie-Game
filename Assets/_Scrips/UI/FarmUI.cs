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

    // ===== SELECT ITEM FROM HOTBAR / INVENTORY =====
    public void SelectToolFromItem(InventoryItem item)
    {
        if (uiOpen) return;
        if (item == null || item.itemData == null) return;

        ShopItemData data = item.itemData;

        // ==== SEED ====
        if (data.itemType == ItemType.Seed)
        {
            PlantData plantData = data.seedItem.plantData;

            map.SetTool(new PlantTool(map, plantManager, plantData, item));

            Debug.Log("Tool: Plant " + plantData.name);
            return;
        }

        // ==== TOOL ====
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

                default:
                    Debug.LogWarning("Unknown tool type!");
                    break;
            }

            return;
        }

        Debug.Log("Selected non-tool item.");
    }
}

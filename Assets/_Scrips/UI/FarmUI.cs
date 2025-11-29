using UnityEngine;

public class FarmUI : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private MapManager map;
    [SerializeField] private PlantManager plantManager;
    [SerializeField] private OrthoIsoCameraController cameraController;

    //private readonly CreateSoilTool createSoilTool = new CreateSoilTool();
    //private readonly RemoveSoilTool removeSoilTool = new RemoveSoilTool();
    private HarvestTool harvestTool;

    public static FarmUI Instance;
    public bool uiOpen = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        harvestTool = new HarvestTool(plantManager);
    }

    private void SetUIState(bool state)
    {
        uiOpen = state;

        if (cameraController != null)
            cameraController.uiOpen = state;

        if (state)
            map.SetTool(null);
    }

    // ===== SOIL TOOLS =====
    //public void SelectCreateSoil()
    //{
    //    if (uiOpen) return;
    //    map.SetTool(createSoilTool);
    //    Debug.Log("Tool: Create Soil");
    //}

    //public void SelectRemoveSoil()
    //{
    //    if (uiOpen) return;
    //    //map.SetTool(removeSoilTool);
    //    Debug.Log("Tool: Remove Soil");
    //}

    // ===== UI PANELS =====
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

    // ===== HARVEST =====
    //public void SelectHarvest()
    //{
    //    if (uiOpen) return;
    //    map.SetTool(harvestTool);
    //    Debug.Log("Tool: Harvest");
    //}

    public void SelectNone()
    {
        if (uiOpen) return;
        map.SetTool(null);
        Debug.Log("Tool: None");
    }

    // ===== SELECT SEED ITEM =====
    public void SelectToolFromItem(InventoryItem item)
    {
        if (uiOpen) return;
        if (item == null || item.itemData == null) return;

        ShopItemData data = item.itemData;

        // ==== SEED ====
        if (data.itemType == ItemType.Seed)
        {
            PlantData plantData = data.seedItem.plantData;
            map.SetTool(new PlantTool(plantManager, plantData, item));
            Debug.Log("Tool: Plant " + plantData.name);
            return;
        }

        // ==== TOOL ====
        if (data.itemType == ItemType.Tool)
        {
            switch (data.toolData.toolType)
            {
                case ToolType.Hoe:
                    map.SetTool(new CreateSoilTool());
                    Debug.Log("Tool: Hoe");
                    break;

                //case ToolType.Shovel:
                //    map.SetTool(new RemoveSoilTool());
                //    Debug.Log("Tool: Shovel");
                //    break;

                //case ToolType.Scythe:
                //    map.SetTool(new HarvestTool(plantManager));
                //    Debug.Log("Tool: Scythe");
                //    break;

                default:
                    Debug.LogWarning("Unknown tool type!");
                    break;
            }

            return;
        }

        Debug.Log("Selected non-tool item.");
    }
}

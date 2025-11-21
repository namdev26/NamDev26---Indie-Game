using UnityEngine;

public class FarmUI : MonoBehaviour
{
    [SerializeField] private MapManager map;
    [SerializeField] private PlantManager plantManager;

    [Header("Plant Types")]
    //[SerializeField] private PlantData tomatoData;
    [SerializeField] private PlantData carrotData;
    //[SerializeField] private PlantData cabbageData;

    private readonly CreateSoilTool createSoilTool = new CreateSoilTool();
    private readonly RemoveSoilTool removeSoilTool = new RemoveSoilTool();

    private HarvestTool harvestTool;

    private void Start()
    {
        harvestTool = new HarvestTool(plantManager);
    }

    // ===== SOIL TOOLS =====
    public void SelectCreateSoil()
    {
        map.SetTool(createSoilTool);
        Debug.Log("Tool: Create Soil");
    }

    public void SelectRemoveSoil()
    {
        map.SetTool(removeSoilTool);
        Debug.Log("Tool: Remove Soil");
    }

    // ===== PLANT TOOLS =====
    //public void SelectPlantTomato()
    //{
    //    map.SetTool(new PlantTool(plantManager, tomatoData));
    //    Debug.Log("Tool: Plant Tomato");
    //}

    public void SelectPlantCarrot()
    {
        map.SetTool(new PlantTool(plantManager, carrotData));
        Debug.Log("Tool: Plant Carrot");
    }

    //public void SelectPlantCabbage()
    //{
    //    map.SetTool(new PlantTool(plantManager, cabbageData));
    //    Debug.Log("Tool: Plant Cabbage");
    //}

    // ===== HARVEST TOOL =====
    public void SelectHarvest()
    {
        map.SetTool(harvestTool);
        Debug.Log("Tool: Harvest");
    }

    // ===== NONE =====
    public void SelectNone()
    {
        map.SetTool(null);
        Debug.Log("Tool: None");
    }
}
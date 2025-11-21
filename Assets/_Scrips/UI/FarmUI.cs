using UnityEngine;

public class FarmUI : MonoBehaviour
{
    [SerializeField] private MapManager map;

    private readonly CreateSoilTool createSoilTool = new CreateSoilTool();
    private readonly RemoveSoilTool removeSoilTool = new RemoveSoilTool();
    private readonly PlantTool plantTool = new PlantTool();

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

    public void SelectPlant()
    {
        map.SetTool(plantTool);
        Debug.Log("Tool: Plant");
    }

    public void SelectNone()
    {
        map.SetTool(null);
        Debug.Log("Tool: None");
    }
}
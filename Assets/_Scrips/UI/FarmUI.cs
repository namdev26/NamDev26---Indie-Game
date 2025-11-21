using UnityEngine;

public class FarmUI : MonoBehaviour
{
    public MapManager map;

    public void SelectRemoveSoil()
    {
        map.currentTool = MapManager.ToolMode.RemoveSoil;
        Debug.Log("Tool: Remove Soil");
    }

    public void SelectPlant()
    {
        map.currentTool = MapManager.ToolMode.Plant;
        Debug.Log("Tool: Plant");
    }

    public void SelectNone()
    {
        map.currentTool = MapManager.ToolMode.None;
        Debug.Log("Tool: None");
    }
    public void SelectCreateSoil()
    {
        map.currentTool = MapManager.ToolMode.CreateSoil;
        Debug.Log("Tool: Create Soil");
    }

}

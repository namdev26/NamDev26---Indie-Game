using UnityEngine;

public class HarvestTool : ITileModifier
{
    private readonly PlantManager plantManager;

    public HarvestTool(PlantManager manager)
    {
        plantManager = manager;
    }

    public void Execute(ITileMap map, int x, int z)
    {
        int amount = plantManager.TryHarvest(x, z);
        if (amount > 0)
            Debug.Log($"Harvested {amount} items!");
    }
}
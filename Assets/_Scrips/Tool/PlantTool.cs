public class PlantTool : ITileModifier
{
    private readonly PlantManager plantManager;
    private readonly PlantData plantData;

    public PlantTool(PlantManager manager, PlantData data)
    {
        plantManager = manager;
        plantData = data;
    }

    public void Execute(ITileMap map, int x, int z)
    {
        plantManager.TryPlant(plantData, x, z);
    }
}
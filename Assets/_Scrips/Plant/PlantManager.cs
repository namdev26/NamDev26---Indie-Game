using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    [SerializeField] private MapManager map;
    [SerializeField] private GameObject plantPrefab; 

    private Dictionary<Vector2Int, PlantInstance> plants = new Dictionary<Vector2Int, PlantInstance>();
    private Dictionary<Vector2Int, GameObject> plantObjects = new Dictionary<Vector2Int, GameObject>();

    public event System.Action<PlantInstance> OnPlantGrown;
    public event System.Action<Vector2Int, int> OnPlantHarvested;

    private void Update()
    {
        UpdateAllPlants();
    }

    private void UpdateAllPlants()
    {
        foreach (var plant in plants.Values)
        {
            if (plant.TryGrow(Time.deltaTime))
            {
                UpdatePlantVisual(plant);
                OnPlantGrown?.Invoke(plant);
            }
        }
    }

    public bool TryPlant(PlantData data, int x, int z)
    {
        var pos = new Vector2Int(x, z);
        var tile = map.TileMap.GetTile(x, z);

        if (!map.TileMap.IsValidPosition(x, z)) return false;
        if (!tile.isSoil) return false;
        if (plants.ContainsKey(pos)) return false;

        var plant = new PlantInstance(data, pos);
        plants[pos] = plant;
        tile.hasPlant = true;

        CreatePlantVisual(plant);
        return true;
    }

    public int TryHarvest(int x, int z)
    {
        var pos = new Vector2Int(x, z);

        if (!plants.TryGetValue(pos, out var plant)) return 0;
        if (!plant.CanHarvest) return 0;

        int amount = plant.Data.harvestAmount;

        RemovePlant(pos);
        OnPlantHarvested?.Invoke(pos, amount);

        return amount;
    }

    public void RemovePlant(Vector2Int pos)
    {
        if (!plants.ContainsKey(pos)) return;

        plants.Remove(pos);
        map.TileMap.GetTile(pos.x, pos.y).hasPlant = false;

        if (plantObjects.TryGetValue(pos, out var obj))
        {
            Destroy(obj);
            plantObjects.Remove(pos);
        }
    }

    public PlantInstance GetPlant(int x, int z)
    {
        plants.TryGetValue(new Vector2Int(x, z), out var plant);
        return plant;
    }

    private void CreatePlantVisual(PlantInstance plant)
    {
        var config = map.Config;

        float px = map.Origin.x + plant.Position.x * config.tileSize + config.tileSize * 0.5f;
        float pz = map.Origin.z + plant.Position.y * config.tileSize + config.tileSize * 0.5f;

        var obj = Instantiate(plantPrefab, new Vector3(px, 0, pz), Quaternion.identity);

        var stagePrefab = plant.Data.GetObjPrefab(plant.Stage);
        var stageObj = Instantiate(stagePrefab, obj.transform);

        stageObj.transform.localPosition = Vector3.zero;
        stageObj.transform.localRotation = Quaternion.identity;

        plantObjects[plant.Position] = obj;
    }



    private void UpdatePlantVisual(PlantInstance plant)
    {
        if (!plantObjects.TryGetValue(plant.Position, out var obj))
            return;

        Destroy(obj);

        var config = map.Config;

        float px = map.Origin.x + plant.Position.x * config.tileSize + config.tileSize * 0.5f;
        float pz = map.Origin.z + plant.Position.y * config.tileSize + config.tileSize * 0.5f;

        var newObj = Instantiate(
            plant.Data.stagePrefabs[(int)plant.Stage],
            new Vector3(px, 0, pz),
            Quaternion.identity
        );

        plantObjects[plant.Position] = newObj;
    }

}
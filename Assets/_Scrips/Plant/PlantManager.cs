﻿using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    [SerializeField] private MapManager map;
    [SerializeField] private GameObject plantRootPrefab;

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
        if (tile.type != TileType.Soil) return false;

        if (plants.ContainsKey(pos)) return false;

        var plant = new PlantInstance(data, pos);
        plants[pos] = plant;
        tile.hasPlant = true;

        CreatePlantVisual(plant);
        return true;
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

        float half = config.tileSize * 0.5f;

        float px = map.Origin.x + plant.Position.x * config.tileSize - half;
        float pz = map.Origin.z + plant.Position.y * config.tileSize + half;

        var rootObj = Instantiate(plantRootPrefab, new Vector3(px, 0, pz), Quaternion.identity);

        var stageData = plant.Data.GetStage(plant.StageIndex);
        if (stageData == null) return;

        var stageObj = Instantiate(stageData.prefab, rootObj.transform);
        stageObj.transform.localPosition = Vector3.zero;

        plantObjects[plant.Position] = rootObj;
    }


    private void UpdatePlantVisual(PlantInstance plant)
    {
        if (!plantObjects.TryGetValue(plant.Position, out var rootObj))
            return;

        foreach (Transform child in rootObj.transform)
            Destroy(child.gameObject);

        var stageData = plant.Data.GetStage(plant.StageIndex);
        if (stageData == null) return;

        var newStage = Instantiate(stageData.prefab, rootObj.transform);
        newStage.transform.localPosition = Vector3.zero;
        newStage.transform.localRotation = Quaternion.identity;
    }

    public PlantInstance GetPlantAt(Vector2Int pos)
    {
        if (plants.TryGetValue(pos, out var plant))
            return plant;

        return null;
    }

    public bool HarvestAt(int x, int z)
    {
        Vector2Int pos = new Vector2Int(x, z);

        if (!plants.TryGetValue(pos, out PlantInstance plant))
            return false;

        if (!plant.IsGrown())
            return false;

        var tile = map.TileMap.GetTile(pos.x, pos.y);
        var drop = plant.Data.harvest;

        // === 1. Tính số lượng thu hoạch có áp dụng Yield Fertilizer ===
        int finalQty = plant.GetFinalHarvestQuantity();

        // === 2. Thêm vào kho đồ ===
        if (drop != null && drop.item != null)
            Inventory.Instance.AddItem(drop.item, finalQty);

        // === 3. Event thu hoạch
        OnPlantHarvested?.Invoke(pos, finalQty);

        // === 4. Xóa dữ liệu cây ===
        plants.Remove(pos);
        tile.hasPlant = false;

        // === 5. Reset phân bón sau vụ này ===
        tile.fertilizerYield = 0f;
        tile.fertilizerSpeed = 0f;

        // === 6. Xóa visual ===
        if (plantObjects.TryGetValue(pos, out GameObject obj))
        {
            Destroy(obj);
            plantObjects.Remove(pos);
        }

        FertilizerVisual.Instance.ClearAll(pos);

        // === 7. Cập nhật tile ===
        map.NotifyTileChanged(x, z);

        return true;
    }
}
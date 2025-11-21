using UnityEngine;

[CreateAssetMenu(fileName = "MapConfig", menuName = "Game/MapConfig")]
public class MapConfig : ScriptableObject
{
    [Header("Size")]
    public int width = 30;
    public int height = 30;
    public float tileSize = 1f;

    [Header("Colors")]
    public Color grassColor = new Color(0.8f, 1f, 0.8f);
    public Color soilDryColor = new Color(0.7f, 0.55f, 0.3f);
    public Color soilWetColor = new Color(0.45f, 0.25f, 0.1f);
    public Color hoverColor = new Color(1f, 1f, 0.7f);

    [Header("Material")]
    public Material terrainMaterial;
}
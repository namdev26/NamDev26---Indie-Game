using UnityEngine;

public class TileRenderer : MonoBehaviour
{
    private TileData data;
    private MeshRenderer meshRenderer;
    private Material mat;

    public Color dryColor = new Color(0.55f, 0.40f, 0.30f);
    public Color wetColor = new Color(0.35f, 0.25f, 0.18f);

    public void Init(TileData tileData)
    {
        data = tileData;

        if (meshRenderer == null)
            meshRenderer = GetComponentInChildren<MeshRenderer>();

        if (meshRenderer != null)
            mat = meshRenderer.material;

        Refresh();
    }

    public void Refresh()
    {
        if (data == null || mat == null)
            return;

        UpdateColor();
    }

    private void UpdateColor()
    {
        if (data.type != TileType.Soil)
        {
            mat.SetColor("_BaseColor", Color.white);
            return;
        }
        // fertilizer speed color tint
        if (data.fertilizerSpeed > 0f)
        {
            mat.SetColor("_BaseColor", Color.blue);
        }

        // fertilizer yield color tint
        if (data.fertilizerYield > 0f)
        {
            mat.SetColor("_BaseColor", Color.black);
        }

        Color c = Color.Lerp(dryColor, wetColor, data.moisture);
        mat.SetColor("_BaseColor", c);
    }
}

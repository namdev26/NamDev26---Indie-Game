using UnityEngine;

public class GridSnapper : MonoBehaviour
{
    public Grid grid;
    public GameObject gridVisual;

    public Vector3 GetSnappedPosition(Vector3 worldPos)
    {
        Vector3Int cell = grid.WorldToCell(worldPos);
        return grid.GetCellCenterWorld(cell);
    }

    public void ShowGrid()
    {
        if (gridVisual != null)
            gridVisual.SetActive(true);
    }

    //public void HideGrid()
    //{
    //    if (gridVisual != null)
    //        gridVisual.SetActive(false);
    //}
}

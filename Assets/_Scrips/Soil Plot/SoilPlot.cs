using UnityEngine;

public class SoilPlot : MonoBehaviour
{
    public bool isUnlocked = true;

    [Header("Refs")]
    public GameObject realMesh;
    public GameObject ghostMesh;

    [HideInInspector] public bool isDragging = false;
    [HideInInspector] public Vector3 originalPosition;

    private void Start()
    {
        ghostMesh.SetActive(false);
    }
}

using UnityEngine;

public class DragManager : MonoBehaviour
{
    public static DragManager Instance;

    private void Awake() => Instance = this;

    [Header("Settings")]
    public float longPressTime = 0.2f;
    public float soilY = 0f;

    [Header("Refs")]
    public Camera cam;
    public GridSnapper gridSnapper;

    private SoilPlot currentPlot;
    private float pressStart;
    private bool isPressing;

    void Update()
    {
        HandleMouseDown();
        HandleLongPress();
        HandleDragging();
        HandleMouseUp();
    }

    void HandleMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SoilPlot clicked = RaycastSoilPlot();
            if (clicked != null)
            {
                currentPlot = clicked;
                currentPlot.originalPosition = clicked.transform.position;
                pressStart = Time.time;
                isPressing = true;
            }
        }
    }

    void HandleLongPress()
    {
        if (isPressing && currentPlot != null && !currentPlot.isDragging)
        {
            if (Time.time - pressStart >= longPressTime)
            {
                BeginDrag();
            }
        }
    }

    void HandleDragging()
    {
        if (currentPlot != null && currentPlot.isDragging)
        {
            DragUpdate();
        }
    }

    void HandleMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (currentPlot != null)
            {
                if (currentPlot.isDragging)
                    FinishDrag();

                isPressing = false;
                currentPlot = null;
            }
        }
    }

    void BeginDrag()
    {
        currentPlot.isDragging = true;
        isPressing = false;

        gridSnapper.ShowGrid();

        currentPlot.realMesh.SetActive(false);
        currentPlot.ghostMesh.SetActive(true);
    }

    private void DragUpdate()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        Plane ground = new Plane(Vector3.up, new Vector3(0, soilY, 0));

        if (ground.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance - 0.5f);

            Vector3 snapPos = gridSnapper.GetSnappedPosition(hitPoint);
            snapPos.y = soilY;
            currentPlot.ghostMesh.transform.position = snapPos;
        }
    }

    void FinishDrag()
    {
        //gridSnapper.HideGrid();

        currentPlot.realMesh.transform.position = currentPlot.ghostMesh.transform.position;

        currentPlot.realMesh.SetActive(true);
        currentPlot.ghostMesh.SetActive(false);

        currentPlot.isDragging = false;
    }

    SoilPlot RaycastSoilPlot()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 200f))
        {
            return hit.collider.GetComponentInParent<SoilPlot>();
        }
        return null;
    }
}

using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform gridParent;
    [SerializeField] private GameObject slotPrefab;

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        foreach (Transform child in gridParent)
            Destroy(child.gameObject);

        foreach (var item in Inventory.Instance.items)
        {
            GameObject slotGO = Instantiate(slotPrefab, gridParent);
            InventorySlot slot = slotGO.GetComponent<InventorySlot>();
            slot.Setup(item);
        }
    }
}

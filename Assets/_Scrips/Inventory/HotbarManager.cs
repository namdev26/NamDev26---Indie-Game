using UnityEngine;

public class HotbarManager : MonoBehaviour
{
    public static HotbarManager Instance;

    [SerializeField] private HotbarSlot[] slots;

    private HotbarSlot selectedSlot;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Inventory.Instance.OnInventoryChanged += RefreshHotbarUI;
    }

    public void AssignToSlot(int index, InventoryItem item)
    {
        if (index < 0 || index >= slots.Length) return;
        slots[index].SetItem(item);
    }

    public void DeselectTool()
    {
        if (selectedSlot != null)
            selectedSlot.ForceDeselect();

        selectedSlot = null;

        FarmUI.Instance.SelectNone();
    }

    public void SelectToolFromHotbar(HotbarSlot slot)
    {
        selectedSlot = slot;

        FarmUI.Instance.SelectToolFromItem(slot.GetItem());
    }

    public void RefreshHotbarUI()
    {
        foreach (var slot in slots)
        {
            var item = slot.GetItem();

            if (item == null)
            {
                slot.ClearSlot();
                continue;
            }

            if (!Inventory.Instance.items.Contains(item))
            {
                slot.ClearSlot();
                continue;
            }

            slot.SetItem(item);
        }
    }

    public bool AssignToNextAvailableSlot(InventoryItem item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            var slotItem = slots[i].GetItem();

            if (slotItem != null && slotItem.itemData.id == item.itemData.id)
            {
                Debug.Log("Item đã có trong hotbar → bỏ qua.");
                return false;
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].GetItem() == null)
            {
                Debug.Log($"Gán item vào slot {i}");
                slots[i].SetItem(item);
                return true;
            }
        }

        Debug.LogWarning("Hotbar full!");
        return false;
    }

}

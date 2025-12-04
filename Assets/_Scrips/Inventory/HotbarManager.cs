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
        // Nếu click vào slot đã được chọn → bỏ chọn
        if (selectedSlot == slot)
        {
            DeselectTool();
            return;
        }

        // Bỏ chọn slot cũ nếu có
        if (selectedSlot != null)
        {
            selectedSlot.ForceDeselect();
        }

        // Chọn slot mới
        selectedSlot = slot;
        slot.SetSelected(true);
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
                // Nếu slot này đang được chọn và bị clear → bỏ chọn
                if (selectedSlot == slot)
                {
                    selectedSlot = null;
                }
                continue;
            }

            if (!Inventory.Instance.items.Contains(item))
            {
                slot.ClearSlot();
                // Nếu slot này đang được chọn và bị clear → bỏ chọn
                if (selectedSlot == slot)
                {
                    selectedSlot = null;
                    FarmUI.Instance.SelectNone();
                }
                continue;
            }

            // Lưu trạng thái selected trước khi SetItem (SetItem sẽ reset selected)
            bool wasSelected = (selectedSlot == slot);
            slot.SetItem(item);
            
            // Khôi phục trạng thái selected nếu slot này đang được chọn
            if (wasSelected)
            {
                slot.SetSelected(true);
            }
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

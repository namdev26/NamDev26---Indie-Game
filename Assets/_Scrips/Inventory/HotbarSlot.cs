using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HotbarSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Button clickButton;
    [SerializeField] private Outline outline;

    private InventoryItem currentItem;

    private bool isSelected = false;

    private void Start()
    {
        //ClearSlot();
    }

    public void SetItem(InventoryItem item)
    {
        currentItem = item;

        if (item == null)
        {
            ClearSlot();
            return;
        }

        icon.enabled = true;
        icon.sprite = item.itemData.icon;
        quantityText.text = "x" + item.quantity;

        clickButton.onClick.RemoveAllListeners();
        clickButton.onClick.AddListener(OnSlotClicked);

        // Reset trạng thái selected khi item thay đổi
        isSelected = false;
        outline.enabled = false;
    }

    private void OnSlotClicked()
    {
        if (currentItem == null) return;

        // Nếu slot này đã được chọn → bỏ chọn
        if (isSelected)
        {
            HotbarManager.Instance.DeselectTool();
        }
        else
        {
            // Chọn slot này (HotbarManager sẽ tự động bỏ chọn slot cũ)
            HotbarManager.Instance.SelectToolFromHotbar(this);
        }
    }

    public void UpdateUI()
    {
        outline.enabled = isSelected;
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        UpdateUI();
    }

    public void ForceDeselect()
    {
        isSelected = false;
        outline.enabled = false;
    }

    public void ClearSlot()
    {
        currentItem = null;
        icon.sprite = null;
        icon.enabled = false;
        quantityText.text = "";
        outline.enabled = false;

        clickButton.onClick.RemoveAllListeners();
    }

    public InventoryItem GetItem()
    {
        return currentItem;
    }
}

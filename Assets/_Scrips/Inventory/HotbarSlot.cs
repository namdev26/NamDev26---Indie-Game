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

        outline.enabled = false;
    }

    private void OnSlotClicked()
    {
        if (currentItem == null) return;

        isSelected = !isSelected;

        if (isSelected)
            HotbarManager.Instance.SelectToolFromHotbar(this);
        else
            HotbarManager.Instance.DeselectTool();

        UpdateUI();
    }

    public void UpdateUI()
    {
        outline.enabled = isSelected;
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

using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Button useButton;
    [SerializeField] private Button pinButton;

    private InventoryItem currentItem;

    public void Setup(InventoryItem item)
    {
        currentItem = item;

        icon.sprite = item.itemData.icon;
        nameText.text = item.itemData.itemName;
        quantityText.text = "x" + item.quantity;

        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(OnUseClicked);

        pinButton.onClick.RemoveAllListeners();
        pinButton.onClick.AddListener(() =>
        {
            Debug.Log("?ã ghim: " + item.itemData.itemName);
            HotbarManager.Instance.AssignToNextAvailableSlot(item);
        });
    }

    private void OnUseClicked()
    {
        bool success = Inventory.Instance.UseItem(currentItem);

        if (success)
        {
            Debug.Log("?ã s? d?ng: " + currentItem.itemData.itemName);
            FindAnyObjectByType<InventoryUI>().Refresh();
        }
    }
}

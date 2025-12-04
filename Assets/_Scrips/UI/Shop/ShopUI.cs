using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [Header("Shop Item")]
    [SerializeField] private ShopItemData[] shopItems;
    [SerializeField] private Transform itemsGridParent;
    [SerializeField] private GameObject shopItemPrefab;

    [Header("Detail Panel")]
    [SerializeField] private Image detailIcon;
    [SerializeField] private TMP_Text detailName;
    [SerializeField] private TMP_Text detailDescription;
    [SerializeField] private TMP_Text detailPrice;
    [SerializeField] private Button buyButton;

    //[Header("Inventory")]
    //[SerializeField] private Inventory playerInventory;

    private ShopItemData currentItem;

    private void Start()
    {
        LoadItems();
    }

    private void LoadItems()
    {
        foreach (var itemData in shopItems)
        {
            GameObject itemGO = Instantiate(shopItemPrefab, itemsGridParent);

            var slot = itemGO.GetComponent<ShopItemSlot>();
            slot.Setup(itemData, this);
        }
    }

    public void ShowItemDetails(ShopItemData itemData)
    {
        currentItem = itemData;

        detailIcon.sprite = itemData.icon;
        detailName.text = itemData.itemName;
        detailDescription.text = itemData.description;
        detailPrice.text = itemData.price.ToString();
    }

    public void OnBuy1Button() 
    {
        if (currentItem == null) return;
        
        int totalPrice = currentItem.price * 1;
        
        // Kiểm tra đủ tiền
        if (!PlayerWallet.Instance.Spend(CurrencyType.Coin, totalPrice))
        {
            Debug.LogWarning($"Không đủ tiền! Cần {totalPrice} coin.");
            return;
        }
        
        // Thêm item vào inventory
        Inventory.Instance.AddItem(currentItem, 1);

        Debug.Log($"Đã mua 1 {currentItem.itemName} với giá {totalPrice} coin");
    }

    public void OnBuy10Button() 
    {
        if (currentItem == null) return;
        
        int totalPrice = currentItem.price * 10;
        
        // Kiểm tra đủ tiền
        if (!PlayerWallet.Instance.Spend(CurrencyType.Coin, totalPrice))
        {
            Debug.LogWarning($"Không đủ tiền! Cần {totalPrice} coin.");
            return;
        }
        
        // Thêm item vào inventory
        Inventory.Instance.AddItem(currentItem, 10);
        
        Debug.Log($"Đã mua 10 {currentItem.itemName} với giá {totalPrice} coin");
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    /// <summary>
    /// [SerializeField] private TMP_Text itemNameText;
    /// </summary>
    [SerializeField] private TMP_Text priceText;

    private ShopItemData shopItemData;
    private ShopUI shopUI;

    public void Setup(ShopItemData data, ShopUI shopUI)
    {
        this.shopItemData = data;
        this.shopUI = shopUI;
     
        icon.sprite = data.icon;
        //itemNameText.text = data.itemName;
        priceText.text = data.price.ToString();
    }

    public void OnClickShopItem()
    {
        shopUI.ShowItemDetails(shopItemData);
    }
}

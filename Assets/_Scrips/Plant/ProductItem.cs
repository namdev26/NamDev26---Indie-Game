using UnityEngine;

[CreateAssetMenu(menuName = "Game/Farm/Product Item")]
public class ProductItem : ScriptableObject
{
    public string productName;
    public Sprite icon;
    public int price;
    public string description;
}

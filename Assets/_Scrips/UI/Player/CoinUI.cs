using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TMP_Text valueText;

    private void Start()
    {
        UpdateUI();

        PlayerWallet.Instance.OnCurrencyChanged += (type, val) =>
        {
            if (type == CurrencyType.Coin)
                UpdateUI();
        };
    }

    void UpdateUI()
    {
        int coin = PlayerWallet.Instance.Get(CurrencyType.Coin);
        valueText.text = coin.ToString();
    }
}

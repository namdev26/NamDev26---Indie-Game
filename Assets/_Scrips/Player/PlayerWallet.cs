using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public static PlayerWallet Instance;

    public event Action<CurrencyType, int> OnCurrencyChanged;

    private readonly Dictionary<CurrencyType, int> values = new();

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        DontDestroyOnLoad(gameObject);

        Load();
    }

    private void Start()
    {
        //PlayerWallet.Instance.Add(CurrencyType.Coin, 999999);
    }

    // lấy tiền
    public int Get(CurrencyType type)
    {
        return values.TryGetValue(type, out int val) ? val : 0;
    }

    // Thêm tiền
    public void Add(CurrencyType type, int amount)
    {
        if (!values.ContainsKey(type)) values[type] = 0;

        values[type] += amount;

        Save();
        OnCurrencyChanged?.Invoke(type, values[type]);
    }

    // Trừ tiền – trả về true/false
    public bool Spend(CurrencyType type, int amount)
    {
        if (Get(type) < amount)
            return false;

        values[type] -= amount;

        Save();
        OnCurrencyChanged?.Invoke(type, values[type]);
        return true;
    }

    // Lưu 
    private void Save()
    {
        foreach (var kv in values)
            PlayerPrefs.SetInt("currency_" + kv.Key, kv.Value);
    }

    private void Load()
    {
        foreach (CurrencyType type in Enum.GetValues(typeof(CurrencyType)))
            values[type] = PlayerPrefs.GetInt("currency_" + type, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyWeapon : MonoBehaviour
{
    public GameObject shopManager;
    public int gunIndex;
    ShopManager sm;
    Image m_Image;
    public Sprite[] guns;
    public int[] price;
    public int currUpgrade;
    public TMP_Text moneyCounter;

    private void Start()
    {
        m_Image = GetComponent<Image>();
        sm = shopManager.GetComponent<ShopManager>();

        currUpgrade = sm.weaponProgress[gunIndex];
        moneyCounter.text = price[currUpgrade].ToString();

        m_Image.sprite = guns[currUpgrade];
    }

    public void PurchaseItem()
    {
        if (sm.GetCurrAmountOfMoney() >= price[currUpgrade] || currUpgrade > guns.Length)
        {
            sm.DecreaseMoney(price[currUpgrade]);

            currUpgrade++;
            m_Image.sprite = guns[currUpgrade];
            moneyCounter.text = price[currUpgrade].ToString();
            sm.weaponProgress[gunIndex] = currUpgrade;
        }
    }
}

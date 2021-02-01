using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectUI : MonoBehaviour
{
    Image m_image;

    public Sprite[] guns;

    [Space]
    static int currGun;

    private void Start()
    {
        m_image = GetComponent<Image>();
        currGun = 0;
    }

    public static int GetSelectedGun()
    {
        return currGun;
    }

    public void NextGun()
    {
        currGun++;
        if (currGun == guns.Length)
        {
            currGun = 0;
        }
        changeSprite(currGun);
    }

    public void PrevGun()
    {
        currGun--;
        Debug.Log(currGun);
        if (currGun < 0)
            currGun = (guns.Length - 1);
        changeSprite(currGun);
    }

    void changeSprite(int i)
    {
        m_image.sprite = guns[i];
    }
}

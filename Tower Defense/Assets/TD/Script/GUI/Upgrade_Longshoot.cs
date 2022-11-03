using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_Longshoot : ShopItemUpgrade
{
    void Start()
    {
        UpdateStatus();
    }

    void UpdateStatus()
    {
        if (GlobalValue.UpgradeLongShoot >= maxUpgrade)
        {
            coinTxt.text = "MAX";
            upgradeButton.interactable = false;
            SetDots(maxUpgrade);
        }
        else
        {
            SetDots(GlobalValue.UpgradeLongShoot);
        }
    }

    void SetDots(int numberCount)
    {
        for(int i = 0; i < upgradeDots.Length; i++)
        {
            if (i < numberCount)
            {
                upgradeDots[i].sprite = dotImageOn;
            }
            else
            {
                upgradeDots[i].sprite = dotImageOff;
            }
        }
    }

    public void Upgrade()
    {
        if (GlobalValue.SavedCoins >= coinPrice)
        {
            SoundManager.PlaySfx(SoundManager.Instance.soundUpgrade);

            GlobalValue.SavedCoins -= coinPrice;
            GlobalValue.UpgradeLongShoot++;

            UpdateStatus();
        }
        else
        {
            Debug.LogError("NOT ENOUGH COIN");
        }
    }
}

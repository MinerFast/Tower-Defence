using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_Longshoot : ShopItemUpgrade
{


    // Start is called before the first frame update
    void Start()
    {
        UpdateStatus();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void SetDots(int number)
    {
        for(int i = 0; i < upgradeDots.Length; i++)
        {
            if (i < number)
                upgradeDots[i].sprite = dotImageOn;
            else
                upgradeDots[i].sprite = dotImageOff;
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
            Debug.LogError("NOT ENOUGH COIN");
    }
}

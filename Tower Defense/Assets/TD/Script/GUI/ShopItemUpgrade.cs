using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUpgrade : MonoBehaviour
{
    public string itemName = "ITEM NAME";
    public string infor = "information for item";
    public enum ItemType { LongShoot, Archers, Fortress }
    public ItemType itemType;
    public int maxUpgrade = 5;
    public Image[] upgradeDots;
    public Sprite dotImageOn, dotImageOff;
    public Text nameTxt, inforTxt;
    [ReadOnly] public int coinPrice = 1;
    public Text coinTxt;

    public Button upgradeButton;

    [Header("Long Shoot")]
    public float forcePerUpgrade = 0.1f;

    [Header("Strong Wall")]
    public float StrongPerUpgrade = 0.2f;

    void Start()
    {
        if (GameMode.Instance)
        {
            switch (itemType)
            {
                case ItemType.Archers:
                    coinPrice = GameMode.Instance.upgradeArcherPrice;
                    break;
                case ItemType.LongShoot:
                    coinPrice = GameMode.Instance.upgradeLongShootrice;
                    break;
                case ItemType.Fortress:
                    coinPrice = GameMode.Instance.upgradeFortressPrice;
                    break;
                default:
                    break;
            }
        }
        nameTxt.text = itemName;
        inforTxt.text = infor;
        coinTxt.text = coinPrice + "";

        UpdateStatus();
    }

    void UpdateStatus()
    {
        int currentUpgrade = 0;
        switch (itemType)
        {
            case ItemType.Archers:
                currentUpgrade = GlobalValue.UpgradeArcher;
                break;
            case ItemType.LongShoot:
                currentUpgrade = GlobalValue.UpgradeLongShoot;
                break;
            case ItemType.Fortress:
                currentUpgrade = GlobalValue.UpgradeStrongWall;
                break;
            default:
                break;
        }
        if (currentUpgrade >= maxUpgrade)
        {
            coinTxt.text = "MAX";
            upgradeButton.interactable = false;
            upgradeButton.GetComponent<Image>().enabled = false;
            SetDots(maxUpgrade);
        }
        else
        {
            SetDots(currentUpgrade);
        }
    }

    void SetDots(int number)
    {
        for (int i = 0; i < upgradeDots.Length; i++)
        {
            if (i < number)
                upgradeDots[i].sprite = dotImageOn;
            else
                upgradeDots[i].sprite = dotImageOff;

            if (i >= maxUpgrade)
                upgradeDots[i].gameObject.SetActive(false);
        }
    }

    public void Upgrade()
    {
        if (GlobalValue.SavedCoins >= coinPrice)
        {
            SoundManager.PlaySfx(SoundManager.Instance.soundUpgrade);
            GlobalValue.SavedCoins -= coinPrice;

            switch (itemType)
            {
                case ItemType.Archers:
                    GlobalValue.UpgradeArcher++;
                    break;
                case ItemType.LongShoot:
                    GlobalValue.UpgradeLongShoot++;
                    GlobalValue.LongShootExtra += forcePerUpgrade;
                    break;
                case ItemType.Fortress:
                    GlobalValue.UpgradeStrongWall++;
                    GlobalValue.StrongWallExtra += StrongPerUpgrade;
                    break;
                default:
                    break;
            }
            
            UpdateStatus();
        }
        else
        {
            SoundManager.PlaySfx(SoundManager.Instance.soundNotEnoughCoin);
            if (AdsManager.Instance && AdsManager.Instance.isRewardedAdReady())
                NotEnoughCoins.Instance.ShowUp();
        }
    }
}

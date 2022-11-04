using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class ShopItemReward : MonoBehaviour
{
    public string itemNameRus;
    public string itemNameEng;
    public string currentRus;
    public string currentEng;
    private string current;
    public enum ItemType { DoubleArrow, TripleArrow, Posion, Freeze }
    public ItemType itemType;

    public int rewardedUnit = 1;

    public Text nameTxt;
    public Text rewardedAmountTxt;
    public Text currentAmountTxt;

    [ReadOnly] public int coinPrice = 1;
    public Text coinTxt;


    void OnEnable()
    {
        UpdateAmount();
    }

    void Start()
    {
        if (GameMode.Instance)
        {
            switch (itemType)
            {
                case ItemType.DoubleArrow:
                    coinPrice = GameMode.Instance.doubleArrowPrice;
                    break;
                case ItemType.TripleArrow:
                    coinPrice = GameMode.Instance.tripleArrowPrice;
                    break;
                case ItemType.Posion:
                    coinPrice = GameMode.Instance.poisonArrowPrice;
                    break;
                case ItemType.Freeze:
                    coinPrice = GameMode.Instance.freezeArrowPrice;
                    break;
                default:
                    break;
            }
        }

        UpdateAmount();

        rewardedAmountTxt.text = "x" + rewardedUnit;
        coinTxt.text = coinPrice.ToString();
        if (PlayerPrefs.GetString("Language") == "rus")
        {
            nameTxt.text = itemNameRus;
            nameTxt.fontStyle = FontStyle.Bold;
        }
        else
        {
            nameTxt.text = itemNameEng;
            nameTxt.fontStyle = FontStyle.Normal;
        }
    }

    public void UseCoin()
    {
        var coins = GlobalValue.SavedCoins;
        if (coins >= coinPrice)
        {
            coins -= coinPrice;
            GlobalValue.SavedCoins = coins;

            DoReward();
        }
        else
        {
            SoundManager.PlaySfx(SoundManager.Instance.soundNotEnoughCoin);
            if (AdsManager.Instance && AdsManager.Instance.isRewardedAdReady())
                NotEnoughCoins.Instance.ShowUp();
        }
    }


    private void DoReward()
    {
        switch (itemType)
        {
            case ItemType.DoubleArrow:
                GlobalValue.ItemDoubleArrow += rewardedUnit;
                break;
            case ItemType.TripleArrow:
                GlobalValue.ItemTripleArrow += rewardedUnit;
                break;
            case ItemType.Posion:
                GlobalValue.ItemPoison += rewardedUnit;
                break;
            case ItemType.Freeze:
                GlobalValue.ItemFreeze += rewardedUnit;
                break;
            default:
                break;
        }

        UpdateAmount();
        SoundManager.PlaySfx(SoundManager.Instance.soundPurchased);

    }

    private void UpdateAmount()
    {
        if (PlayerPrefs.GetString("Language") == "rus")
        {
            current = currentRus;
            currentAmountTxt.fontStyle = FontStyle.Bold;
        }
        else
        {
            current = currentEng;
            currentAmountTxt.fontStyle = FontStyle.Normal;
            //current: 
        }
        switch (itemType)
        {
            case ItemType.DoubleArrow:
                currentAmountTxt.text = current + GlobalValue.ItemDoubleArrow;
                break;
            case ItemType.TripleArrow:
                currentAmountTxt.text = current + GlobalValue.ItemTripleArrow;
                break;
            case ItemType.Posion:
                currentAmountTxt.text = current + GlobalValue.ItemPoison;
                break;
            case ItemType.Freeze:
                currentAmountTxt.text = current + GlobalValue.ItemFreeze;
                break;
            default:
                break;
        }
    }
}

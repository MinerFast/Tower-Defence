using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiftVideoAd : MonoBehaviour
{
    public Text rewardedTxt;
    public GameObject button;
    bool allowShow = true;
    // Start is called before the first frame update
    void Start()
    {
        if (GameMode.Instance)
        {
            rewardedTxt.text = AdsManager.Instance.rewardedWatchAd + "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        button.SetActive(allowShow && AdsManager.Instance && AdsManager.Instance.isRewardedAdReady());
    }

    public void WatchVideoAd()
    {
        SoundManager.Click();
        allowShow = false;
        AdsManager.AdResult += AdsManager_AdResult;
        AdsManager.Instance.ShowRewardedAds();
        Invoke("AllowShow", 2);
    }

    private void AdsManager_AdResult(bool isSuccess, int rewarded)
    {
        AdsManager.AdResult -= AdsManager_AdResult;
        if (isSuccess)
        {
            GlobalValue.SavedCoins += AdsManager.Instance.rewardedWatchAd;
            SoundManager.PlaySfx(SoundManager.Instance.soundPurchased);
        }
    }

    void AllowShow()
    {
        allowShow = true;
    }
}

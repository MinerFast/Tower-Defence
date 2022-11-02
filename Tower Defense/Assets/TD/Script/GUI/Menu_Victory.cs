using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

/// <summary>
/// Handle Level Complete UI of Menu object
/// </summary>
public class Menu_Victory : MonoBehaviour {
	public GameObject Menu;
	public GameObject Restart;
	public GameObject Next;
    //public GameObject ComicBut;
	public GameObject Star1;
	public GameObject Star2;
	public GameObject Star3;

	

	//public Text Score;
	//public Text Best;

	//private int scoreRunning;
	//private int score = 0;
	//private bool finishCounting = false;

	void Awake(){
		Menu.SetActive (false);
		Restart.SetActive (false);
		Next.SetActive (false);
		Star1.SetActive (false);
		Star2.SetActive (false);
		Star3.SetActive (false);
        //ComicBut.SetActive(false);

    }

    IEnumerator Start()
    {
        SoundManager.PlaySfx(SoundManager.Instance.soundVictoryPanel);
        Star1.SetActive(false);
        Star2.SetActive(false);
        Star3.SetActive(false);
        

        var theFortress = FindObjectOfType<TheFortrest>();
        if ((theFortress.currentHealth / theFortress.maxHealth) > 0)
        {
            yield return new WaitForSeconds(0.6f);
            Star1.SetActive(true);
            SoundManager.PlaySfx(SoundManager.Instance.soundStar1);
            GameManager.Instance.levelStarGot = 1;
        }

        if ((theFortress.currentHealth / theFortress.maxHealth) > 0.5f)
        {
            yield return new WaitForSeconds(0.6f);
            Star2.SetActive(true);
            SoundManager.PlaySfx(SoundManager.Instance.soundStar2);
            GameManager.Instance.levelStarGot = 2;
        }

        if ((theFortress.currentHealth / theFortress.maxHealth) > 0.8f)
        {
            yield return new WaitForSeconds(0.6f);
            Star3.SetActive(true);
            SoundManager.PlaySfx(SoundManager.Instance.soundStar3);
            GameManager.Instance.levelStarGot = 3;
        }
        //}
        yield return new WaitForSeconds(0.5f);
        //when finish counting, enable those button for user choose
        Menu.SetActive(true);
        Restart.SetActive(true);
        Next.SetActive(true); ;
        //				if (!GameManager.instance.isFinishWorld)
        //					Next.SetActive (true);
        //if (GameManager.instance.isFinishWorld && GlobalValue.worldPlaying == 3)
        //    Next.SetActive(false);

        //if (GlobalValue.PracticeModeEnable && !GlobalValue.isTutorial)
        //    Next.SetActive(false);

        //if (GameManager.instance.isFinishGame)
        //{
        //    Menu.SetActive(false);
        //    Restart.SetActive(false); ;
        //    Next.SetActive(false);
        //    ComicBut.SetActive(true);
        //}
    }
    //bool showNormalVideo = false;

    //public void ShowEndComic()
    //{
    //    if (GlobalValue.nextLvType == global::NextLevel.RewardAd)
    //    {
    //        ShowRewardedAd();
    //        return;
    //    }
    //    else if (GlobalValue.nextLvType == global::NextLevel.Normal)
    //    {
    //        showNormalVideo = false;
    //        ShowNormalVideo();
    //        return;
    //    }
    //    else
    //        GameManager.instance.ShowEndScene();

    //    //GameManager.instance.ShowEndScene();
    //}

    //private void ShowRewardedAd()
    //{
    //    if (!GlobalValue.allowClickUnityAdAgain)
    //        return;

    //    if (Advertisement.IsReady("rewardedVideo"))
    //    {
    //        var options = new ShowOptions { resultCallback = HandleShowResult };
    //        Advertisement.Show("rewardedVideo", options);
    //        GlobalValue.allowClickUnityAdAgain = false;
    //    }
    //}

    //public void ShowNormalVideo()
    //{
    //    if (GlobalValue.RemoveAds)
    //    {
    //        Debug.LogWarning("NORMAL AD IS REMOVED");
    //        GameManager.instance.ShowEndScene();
    //        return;
    //    }

    //    if (!GlobalValue.allowClickUnityAdAgain)
    //        return;

    //    if (Advertisement.IsReady())
    //    {
    //        var options = new ShowOptions { resultCallback = HandleShowResult };
    //        Advertisement.Show(options);
    //        GlobalValue.allowClickUnityAdAgain = false;
    //    }
    //}

    //private void HandleShowResult(ShowResult result)
    //{
    //    GlobalValue.allowClickUnityAdAgain = true;
    //    GameManager.instance.ShowEndScene();
    //}
}

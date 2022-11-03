using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResetData : MonoBehaviour
{
    private SoundManager soundManager;
    public bool ResetRemoveAd = false;

    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }
    #region Reset
    public void Reset()
    {

        SoundManager.PlaySfx(soundManager.soundClick);

        GlobalValue.isNewGame = false;
        GlobalValue.LevelPass = 0;

    }

    public void ResetGame()
    {
        SoundManager.Click();

        bool isRemoveAd = GlobalValue.RemoveAds;

        PlayerPrefs.DeleteAll();
        GlobalValue.RemoveAds = ResetRemoveAd ? false : isRemoveAd;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHomeScene : MonoBehaviour
{

    public static MainMenuHomeScene Instance;

    public GameObject MapUI;
    public GameObject ShopUI;
    public GameObject Loading;
    public GameObject GetMoreCoin;
    public GameObject Settings;

    public string facebookLink;
    public string twitterLink = "https://twitter.com/";

    public Text[] coinTxt;

    [Header("Sound and Music")]
    public Image soundImage;
    public Image musicImage;
    public Sprite soundImageOn, soundImageOff, musicImageOn, musicImageOff;

    #region MonoBehaviour
    void Awake()
    {
        Instance = this;
        if (Loading != null)
        {
            Loading.SetActive(false);
        }

        if (MapUI != null)
        {
            MapUI.SetActive(false);
        }

        if (GetMoreCoin)
        {
            GetMoreCoin.SetActive(false);
        }

        if (Settings)
        {
            Settings.SetActive(false);
        }

        if (ShopUI)
        {
            ShopUI.SetActive(false);
        }

    }
    void Update()
    {
        CheckSoundMusic();

        foreach (var ct in coinTxt)
        {
            ct.text = GlobalValue.SavedCoins + "";
        }
    }
    IEnumerator Start()
    {
        CheckSoundMusic();
     

        if (GlobalValue.isFirstOpenMainMenu)
        {
            GlobalValue.isFirstOpenMainMenu = false;
            SoundManager.Instance.PauseMusic(true);
            SoundManager.PlaySfx(SoundManager.Instance.beginSoundInMainMenu);
            yield return new WaitForSeconds(SoundManager.Instance.beginSoundInMainMenu.length);
            SoundManager.Instance.PauseMusic(false);
            SoundManager.PlayMusic(SoundManager.Instance.musicsGame);
        }
    }
    #endregion

    public void LoadScene(string name)
    {
        if (Loading != null)
            Loading.SetActive(true);

        StartCoroutine(LoadAsynchronously(name));
    }

    #region OpenRegion
    public void OpenGetMoreCoin(bool open)
    {
        SoundManager.Instance.ClickBut();
        GetMoreCoin.SetActive(open);
    }

    public void OpenMap(bool open)
    {
        SoundManager.Click();
        StartCoroutine(OpenMapCo(open));
    }

    IEnumerator OpenMapCo(bool open)
    {
        yield return null;

        BlackScreenUI.instance.Show(0.2f);
        MapUI.SetActive(open);
        BlackScreenUI.instance.Hide(0.2f);
    }

    public void Facebook()
    {
        SoundManager.Click();
        Application.OpenURL(facebookLink);
    }

    public void Twitter()
    {
        SoundManager.Click();
        Application.OpenURL(twitterLink);
    }

    public void ExitGame()
    {
        SoundManager.Click();
        Application.Quit();
    }

    public void Setting(bool open)
    {
        SoundManager.Click();
        Settings.SetActive(open);
    }
    public void OpenShop(bool open)
    {
        SoundManager.Click();
        ShopUI.SetActive(open);
    }

    public void Tutorial()
    {
        SoundManager.Click();
        SceneManager.LoadScene("Tutorial");
    }
    #endregion

    #region Music 
    public void TurnSound()
    {
        GlobalValue.isSound = !GlobalValue.isSound;
        soundImage.sprite = GlobalValue.isSound ? soundImageOn : soundImageOff;

        SoundManager.SoundVolume = GlobalValue.isSound ? 1 : 0;
    }

    public void TurnMusic()
    {
        GlobalValue.isMusic = !GlobalValue.isMusic;
        musicImage.sprite = GlobalValue.isMusic ? musicImageOn : musicImageOff;

        SoundManager.MusicVolume = GlobalValue.isMusic ? SoundManager.Instance.musicsGameVolume : 0;
    }
    private void CheckSoundMusic()
    {
        soundImage.sprite = GlobalValue.isSound ? soundImageOn : soundImageOff;
        musicImage.sprite = GlobalValue.isMusic ? musicImageOn : musicImageOff;
        SoundManager.SoundVolume = GlobalValue.isSound ? 1 : 0;
        SoundManager.MusicVolume = GlobalValue.isMusic ? SoundManager.Instance.musicsGameVolume : 0;
    }
    #endregion



    public Slider slider;
    public Text progressText;
    IEnumerator LoadAsynchronously(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            progressText.text = (int)progress * 100f + "%";
            yield return null;
        }
    }

    public void ResetData()
    {
        if (GameMode.Instance)
            GameMode.Instance.ResetDATA();
    }
}

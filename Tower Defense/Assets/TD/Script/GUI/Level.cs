/// <summary>
/// The UI Level, check the current level
/// </summary>
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    string levelSceneName;
    public int world = 1;
    public int level = 1;
    public bool isUnlock = false;
    public Text numberTxt;
    public GameObject imgLock, imgOpen, imgPass;

    public GameObject starGroup;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public bool loadSceneManual = false;
    public string loadSceneName = "story1";

    #region MonoBehaviour
    void Start()
    {

        if (level > GlobalValue.finishGameAtLevel)
        {
            gameObject.SetActive(false);
            return;
        }

        numberTxt.text = level + "";
        levelSceneName = "Lv" + level;

        var openLevel = isUnlock ? true : GlobalValue.LevelPass + 1 >= level;
        var stars = GlobalValue.LevelStar(level);

        star1.SetActive(openLevel && stars >= 1);
        star2.SetActive(openLevel && stars >= 2);
        star3.SetActive(openLevel && stars >= 3);

        imgLock.SetActive(false);
        imgOpen.SetActive(false);
        imgPass.SetActive(false);
        starGroup.SetActive(false);

        if (openLevel)
        {
            if (GlobalValue.LevelPass + 1 == level)
            {
                imgOpen.SetActive(true);
                FindObjectOfType<MapControllerUI>().SetCurrentWorld(world);
            }
            else
            {
                imgPass.SetActive(true);
                starGroup.SetActive(true);
            }

        }
        else
        {
            imgLock.SetActive(true);
        }
        GetComponent<Button>().interactable = openLevel;
    }
    #endregion
    #region Play
    public void Play(string _levelSceneName = null)
    {

        SoundManager.Click();

        GlobalValue.levelPlaying = level;

        MainMenuHomeScene.Instance.LoadScene(_levelSceneName);
    }
    public void Play()
    {
        GlobalValue.levelPlaying = level;

        SoundManager.Click();

        MainMenuHomeScene.Instance.LoadScene(levelSceneName);

    }

    #endregion
}

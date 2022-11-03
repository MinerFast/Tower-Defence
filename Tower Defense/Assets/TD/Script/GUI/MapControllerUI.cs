using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapControllerUI : MonoBehaviour
{

    public RectTransform BlockLevel;

    public int howManyBlocks = 3;
    int currentPos = 0;

    public float step = 720f;
    private float newPosX = 0;

    public Image[] Dots;

    public AudioClip music;

    #region MonoBehaviour
    void Start()
    {
        SetDots();
    }

    void OnEnable()
    {
        SoundManager.PlayMusic(music);
    }

    void OnDisable()
    {
        SoundManager.PlayMusic(SoundManager.Instance.musicsGame);
    }
    #endregion
    void SetDots()
    {
        foreach (var obj in Dots)
        {
            obj.color = new Color(1, 1, 1, 0.5f);
            obj.rectTransform.sizeDelta = new Vector2(28, 28);
        }

        Dots[currentPos].color = Color.yellow;
        Dots[currentPos].rectTransform.sizeDelta = new Vector2(38, 38);
    }


    public void SetCurrentWorld(int world)
    {
        var magicCount = 1;
        currentPos += (world - magicCount);

        newPosX -= step * (world - magicCount);
        newPosX = Mathf.Clamp(newPosX, -step * (howManyBlocks - magicCount), 0);

        SetMapPosition();

        SetDots();
    }

    private Vector2 MapPos()
    {
        return new Vector2(newPosX, BlockLevel.anchoredPosition.y);
    }
    public void SetMapPosition()
    {
        BlockLevel.anchoredPosition = MapPos();
    }

    private bool allowPressButton = true;
    public void Next()
    {
        if (allowPressButton)
        {
            StartCoroutine(NextCo());
        }
    }

    IEnumerator NextCo()
    {
        var magicCount = 0.15f;
        allowPressButton = false;

        SoundManager.Click();

        if (newPosX != (-step * (howManyBlocks - 1)))
        {
            currentPos++;

            newPosX -= step;
            newPosX = Mathf.Clamp(newPosX, -step * (howManyBlocks - 1), 0);

        }
        else
        {
            allowPressButton = true;
            yield break;
        }

        BlackScreenUI.instance.Show(magicCount);

        yield return new WaitForSeconds(magicCount);
        SetMapPosition();
        BlackScreenUI.instance.Hide(magicCount);

        SetDots();


        allowPressButton = true;

    }

    public void Pre()
    {
        if (allowPressButton)
        {
            StartCoroutine(PreCo());
        }
    }

    IEnumerator PreCo()
    {
        allowPressButton = false;
        SoundManager.Click();
        if (newPosX != 0)
        {
            currentPos--;

            newPosX += step;
            newPosX = Mathf.Clamp(newPosX, -step * (howManyBlocks - 1), 0);
        }
        else
        {
            allowPressButton = true;
            yield break;
        }

        BlackScreenUI.instance.Show(0.15f);

        yield return new WaitForSeconds(0.15f);
        SetMapPosition();
        BlackScreenUI.instance.Hide(0.15f);

        SetDots();


        allowPressButton = true;

    }

    public void UnlockAllLevels()
    {
        GlobalValue.LevelPass = (GlobalValue.LevelPass + 1000);

        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

        SoundManager.Click();
    }
}

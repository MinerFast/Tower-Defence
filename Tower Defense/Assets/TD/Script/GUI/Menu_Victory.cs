using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

/// <summary>
/// Handle Level Complete UI of Menu object
/// </summary>
public class Menu_Victory : MonoBehaviour
{

    public GameObject menu;
    public GameObject restart;
    public GameObject next;

    public GameObject starOne;
    public GameObject starTwo;
    public GameObject starThree;

    void Awake()
    {
        menu.SetActive(false);
        restart.SetActive(false);
        next.SetActive(false);

        starOne.SetActive(false);
        starTwo.SetActive(false);
        starThree.SetActive(false);

    }

    IEnumerator Start()
    {
        var magicCount = 0.6f;
        SoundManager.PlaySfx(SoundManager.Instance.soundVictoryPanel);

        starOne.SetActive(false);
        starTwo.SetActive(false);
        starThree.SetActive(false);


        var theFortress = FindObjectOfType<TheFortrest>();
        if ((theFortress.currentHealth / theFortress.maxHealth) > 0)
        {
            yield return new WaitForSeconds(magicCount);
            starOne.SetActive(true);
            SoundManager.PlaySfx(SoundManager.Instance.soundStar1);
            GameManager.Instance.levelStarGot = 1;
        }

        if ((theFortress.currentHealth / theFortress.maxHealth) > 0.5f)
        {
            yield return new WaitForSeconds(magicCount);
            starTwo.SetActive(true);
            SoundManager.PlaySfx(SoundManager.Instance.soundStar2);
            GameManager.Instance.levelStarGot = 2;
        }

        if ((theFortress.currentHealth / theFortress.maxHealth) > 0.8f)
        {
            yield return new WaitForSeconds(magicCount);
            starThree.SetActive(true);
            SoundManager.PlaySfx(SoundManager.Instance.soundStar3);
            GameManager.Instance.levelStarGot = 3;
        }
        yield return new WaitForSeconds(0.5f);

        menu.SetActive(true);
        restart.SetActive(true);
        next.SetActive(true); ;
    }
}

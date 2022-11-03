using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlashScene : MonoBehaviour
{

    public GameObject LoadingObj;
    public Slider slider;
    public Text progressText;
    public string sceneLoad = "scene name";
    public float delay = 2;

    void Awake()
    {
        StartCoroutine(LoadAsynchronously(sceneLoad));
    }
    IEnumerator LoadAsynchronously(string name)
    {
        LoadingObj.SetActive(false);
        yield return new WaitForSeconds(delay);

        LoadingObj.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            progressText.text = (int)progress * 100f + "%";
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreenUI : MonoBehaviour
{

    public static BlackScreenUI instance;

    private CanvasGroup canvas;

    private Image image;

    private float countAplpha = 0;
    void Start()
    {
        instance = this;
        canvas = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
    }

    public void Show(float timerForCoroutine, Color _color)
    {
        image.color = _color;

        canvas.alpha = countAplpha;

        StartCoroutine(MMFade.FadeCanvasGroup(GetComponent<CanvasGroup>(), timerForCoroutine, 1));
    }

    public void Show(float timerForCoroutine)
    {
        image.color = Color.black;

        canvas.alpha = countAplpha;

        StartCoroutine(MMFade.FadeCanvasGroup(GetComponent<CanvasGroup>(), timerForCoroutine, 1));
    }

    public void Hide(float timerForCoroutine)
    {
        canvas.alpha = 1;

        StartCoroutine(MMFade.FadeCanvasGroup(GetComponent<CanvasGroup>(), timerForCoroutine, 0));
    }
}

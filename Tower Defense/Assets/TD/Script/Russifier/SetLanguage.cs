using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class SetLanguage : MonoBehaviour
{
    public static UnityEvent changeTextMainMenu = new UnityEvent();
    [Header("Button")]
    [SerializeField] private Button changeButton;
    [SerializeField] private Sprite engSprite;
    [SerializeField] private Sprite rusSprite;
    private const string rus = "rus";
    private const string eng = "eng";

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
            {
                changeButton.image.sprite = rusSprite;
                PlayerPrefs.SetString("Language", rus);
            }
            else
            {
                changeButton.image.sprite = engSprite;
                PlayerPrefs.SetString("Language", eng);
            }
        }
        else
        {
            if (PlayerPrefs.GetString("Language") == eng)
            {
                changeButton.image.sprite = engSprite;
            }
            else
            {
                changeButton.image.sprite = rusSprite;
            }

        }
        changeTextMainMenu?.Invoke();
    }
    private void Update()
    {
        print(PlayerPrefs.GetString("Language"));
    }
    public void ChangeLanguage()
    {
        if (PlayerPrefs.GetString("Language") == eng)
        {
            changeButton.image.sprite = rusSprite;
            PlayerPrefs.SetString("Language", rus);
        }
        else
        {
            changeButton.image.sprite = engSprite;
            PlayerPrefs.SetString("Language", eng);
        }
        changeTextMainMenu?.Invoke();
    }
}

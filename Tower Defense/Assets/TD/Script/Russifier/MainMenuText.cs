using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuText : MonoBehaviour
{
    [Header("Rus")]
    [SerializeField] private string rusText;
    [SerializeField] private Font rusFont;
    [Header("Eng")]
    [SerializeField] private string engText;
    [SerializeField] private Font engFont;
    [Space]
    private Text text;
    private const string rus = "rus";
    private const string eng = "eng";

    private void Awake()
    {
        text = gameObject.GetComponent<Text>();
        SetLanguage.changeTextMainMenu.AddListener(ChangeText);
        ChangeText();
    }
    public void ChangeText()
    {
        if (PlayerPrefs.GetString("Language") == eng)
        {
            text.text = engText;
            text.font = engFont;
            text.fontStyle = FontStyle.Normal;
        }
        else
        {
            text.text = rusText;
            text.font = rusFont;
            text.fontStyle = FontStyle.Bold;
        }
    }
}

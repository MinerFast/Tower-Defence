using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeText : MonoBehaviour
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
        text = GetComponent<Text>();
        if (PlayerPrefs.GetString("Language") == rus)
        {
            text.text = rusText;
            text.font = rusFont;
            text.fontStyle = FontStyle.Normal;
        }
        else
        {
            text.text = engText;
            text.font = engFont;
            text.fontStyle = FontStyle.Bold;
        }

    }
}

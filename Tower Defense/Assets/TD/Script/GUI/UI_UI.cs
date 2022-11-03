using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UI : MonoBehaviour
{
    public Slider healthSlider;
    public Slider enemyWavePercentSlider;

    public Text health;
    public Text pointTxt;
    public Text levelName;


    float healthValue, enemyWaveValue;

    public float lerpSpeed = 1;

    #region MonoBehaviour
    private void Start()
    {
        healthValue = 1;
        enemyWaveValue = 0;

        healthSlider.value = 1;
        enemyWavePercentSlider.value = 0;
        levelName.text = "Level " + GlobalValue.levelPlaying;
    }

    private void Update()
    {
        healthSlider.value = Mathf.Lerp(healthSlider.value, healthValue, lerpSpeed * Time.deltaTime);
        enemyWavePercentSlider.value = Mathf.Lerp(enemyWavePercentSlider.value, enemyWaveValue, lerpSpeed * Time.deltaTime);

        pointTxt.text = GameManager.Instance.Point + "";
    }
    #endregion
    #region Update
    public void UpdateHealthbar(float currentHealthCount, float maxHealthCount)
    {
        healthValue = Mathf.Clamp01(currentHealthCount / maxHealthCount);
        health.text = currentHealthCount + "/" + maxHealthCount;
    }

    public void UpdateEnemyWavePercent(float currentSpawnCount, float maxValueCount)
    {
        enemyWaveValue = Mathf.Clamp01(currentSpawnCount / maxValueCount);
    }
    #endregion
}

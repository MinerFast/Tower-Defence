using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UI : MonoBehaviour
{
    public Slider healthSlider;
    public Text health;

    public Slider enemyWavePercentSlider;

    float healthValue, enemyWaveValue;
    public float lerpSpeed = 1;

    public Text pointTxt;
    public Text levelName;

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

    public void UpdateHealthbar(float currentHealth, float maxHealth)
    {
        healthValue = Mathf.Clamp01(currentHealth / maxHealth);
        health.text = currentHealth + "/" + maxHealth;
    }

    public void UpdateEnemyWavePercent(float currentSpawn, float maxValue)
    {
        enemyWaveValue = Mathf.Clamp01(currentSpawn / maxValue);
    }
    
   
}

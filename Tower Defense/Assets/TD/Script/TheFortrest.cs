using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheFortrest : MonoBehaviour, ICanTakeDamage
{
    public float maxHealth = 1000;

    [ReadOnly] public float extraHealth = 0;
    [ReadOnly] public float currentHealth;

    [Header("SHAKNG")]
    public float speed = 30f;
    public float amount = 0.5f;
    public float shakeTime = 0.3f;
    public bool shakeX, shakeY;

    Vector2 startingPos;
    IEnumerator ShakeCoDo;

    #region MonoBehaviour
    void Start()
    {
        startingPos = transform.position;
        extraHealth = maxHealth * GlobalValue.StrongWallExtra;
        maxHealth += extraHealth;
        currentHealth = maxHealth;
        MenuManager.Instance.UpdateHealthbar(currentHealth, maxHealth);
    }
    #endregion

    IEnumerator ShakeCo(float time)
    {
        float counter = 0;
        while (counter < time)
        {
            transform.position = startingPos + new Vector2(Mathf.Sin(Time.time * speed) * amount * (shakeX ? 1 : 0), Mathf.Sin(Time.time * speed) * amount * (shakeY ? 1 : 0));

            yield return null;
            counter += Time.deltaTime;
        }

        transform.position = startingPos;
    }

    public void TakeDamage(float damage, Vector2 force, Vector2 hitPoint, GameObject instigator, BODYPART bodyPart = BODYPART.NONE, WeaponEffect weaponEffect = null)
    {
        currentHealth -= damage;
        MenuManager.Instance.UpdateHealthbar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }
        else
        {
            if (ShakeCoDo != null)
            {
                StopCoroutine(ShakeCoDo);
            }

            ShakeCoDo = ShakeCo(shakeTime);
            StartCoroutine(ShakeCoDo);
        }
    }
}

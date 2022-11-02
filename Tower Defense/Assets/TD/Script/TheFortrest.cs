using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheFortrest : MonoBehaviour, ICanTakeDamage
{
    public float maxHealth = 1000;

    [ReadOnly] public float extraHealth = 0;
    [ReadOnly] public float currentHealth;

    [Header("SHAKNG")]
    public float speed = 30f; //how fast it shakes
    public float amount = 0.5f; //how much it shakes
    public float shakeTime = 0.3f;
    public bool shakeX, shakeY;

    Vector2 startingPos;
    IEnumerator ShakeCoDo;

    //void Start()
    //{
        
    //}

    IEnumerator ShakeCo(float time)
    {
        float counter = 0;
        while (counter < time)
        {
            transform.position = startingPos + new Vector2(Mathf.Sin(Time.time * speed) * amount * (shakeX?1:0), Mathf.Sin(Time.time * speed) * amount * (shakeY ? 1 : 0));

            yield return null;
            counter += Time.deltaTime;
        }

        transform.position = startingPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        extraHealth = maxHealth * GlobalValue.StrongWallExtra;
        maxHealth += extraHealth;
        currentHealth = maxHealth;
        MenuManager.Instance.UpdateHealthbar(currentHealth, maxHealth);
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
                StopCoroutine(ShakeCoDo);

            ShakeCoDo = ShakeCo(shakeTime);
            StartCoroutine(ShakeCoDo);
        }
    }
}

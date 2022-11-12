using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField] private GameObject boom;
    private MenuManager menuManager;

    private void Start()
    {
        menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.GetComponent<TheFortrest>())
        {
            collision.gameObject.GetComponent<TheFortrest>().currentHealth -= 300;
            boom.transform.parent = GameObject.Find("Particle").transform;
            MenuManager.Instance.UpdateHealthbar(collision.gameObject.GetComponent<TheFortrest>().currentHealth, collision.gameObject.GetComponent<TheFortrest>().maxHealth);
            boom.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    public void Explosion()
    {
        boom.transform.parent = GameObject.Find("Particle").transform;
        boom.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

}

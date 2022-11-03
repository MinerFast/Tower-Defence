using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemyFallOut : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
        {
            collision.transform.position = LevelEnemyManager.Instance.spawnPosition + Vector2.up * 2;
        }
    }
}

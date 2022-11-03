using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCallMinion : MonoBehaviour
{
    public GameObject minion;
    private List<GameObject> listEnemy;
    public LayerMask layerAsGround;

    public float delayCallMin = 3;
    public float delayCallMax = 6;

    public float distanceMin = 1;
    public float distanceMax = 3;

    public int numberMinionMax = 3;

    private float lastCallTime = 0;
    private float delaySpawn;

    private bool isSpawning = false;

    public int numberEnemyLive()
    {
        int live = 0;

        if (listEnemy.Count > 0)
        {
            foreach (var obj in listEnemy)
            {
                if (obj.activeInHierarchy)
                {
                    live++;
                }
            }
        }
        return live;
    }
    void Start()
    {
        delaySpawn = Random.Range(delayCallMin, delayCallMax);
        listEnemy = new List<GameObject>();

    }
    #region Call Minion

    public bool CanCallMinion()
    {
        if (isSpawning || numberEnemyLive() >= numberMinionMax)
        {
            return false;
        }

        if (Time.time >= lastCallTime + delaySpawn)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CallMinion(bool isFacingRight)
    {
        isSpawning = true;
        Vector2 randomSpawnPoint = new Vector2(Random.Range(transform.position.x + distanceMin * (isFacingRight ? 1 : -1), transform.position.x + distanceMax * (isFacingRight ? 1 : -1)), transform.position.y + 1);
        RaycastHit2D hitCount = Physics2D.Raycast(randomSpawnPoint, Vector2.down, 10, layerAsGround);
        if (hitCount)
        {
            listEnemy.Add(Instantiate(minion, hitCount.point + Vector2.up * 0.1f, Quaternion.identity));

            isSpawning = false;
            lastCallTime = Time.time;
            delaySpawn = Random.Range(delayCallMin, delayCallMax);
        }
        else
        {
            Invoke("CallMinion", 0.1f);
        }
    }
    #endregion
}

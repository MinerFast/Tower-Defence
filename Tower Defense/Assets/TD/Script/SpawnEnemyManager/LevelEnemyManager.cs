using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnemyManager : MonoBehaviour, IListener
{
    public static LevelEnemyManager Instance;
    public Vector2 spawnPosition;
    public EnemyWave[] EnemyWaves;

    private List<GameObject> listEnemySpawned = new List<GameObject>();

    private int totalEnemy;
    private int currentSpawn;
    private void Awake()
    {
        Instance = this;

        if (GameLevelSetup.Instance)
        {
            EnemyWaves = GameLevelSetup.Instance.GetLevelWave();
        }
        var hitCount = Physics2D.Raycast(transform.position, Vector2.down, 100);
        if (hitCount)
        {
            spawnPosition = hitCount.point;
        }
        else
        {
            Debug.LogError("NEED PLACE LEVEL SPAWN MANAGER ABOVE THE GROUND TO SPAWN THE ENEMY");
        }
    }

    void Start()
    {
        totalEnemy = 0;
        for (int i = 0; i < EnemyWaves.Length; i++)
        {

            for (int j = 0; j < EnemyWaves[i].enemySpawns.Length; j++)
            {
                var enemySpawn = EnemyWaves[i].enemySpawns[j];
                for (int k = 0; k < enemySpawn.numberEnemy; k++)
                {
                    totalEnemy++;
                }
            }
        }

        currentSpawn = 0;
    }

    IEnumerator SpawnEnemyCo()
    {
        for (int i = 0; i < EnemyWaves.Length; i++)
        {
            yield return new WaitForSeconds(EnemyWaves[i].wait);

            for (int j = 0; j < EnemyWaves[i].enemySpawns.Length; j++)
            {
                var enemySpawn = EnemyWaves[i].enemySpawns[j];
                yield return new WaitForSeconds(enemySpawn.wait);
                for (int k = 0; k < enemySpawn.numberEnemy; k++)
                {
                    GameObject tempCount = Instantiate(enemySpawn.enemy, spawnPosition + Vector2.up * 2, Quaternion.identity) as GameObject;
                    tempCount.SetActive(false);
                    tempCount.transform.parent = transform;

                    yield return new WaitForSeconds(0.1f);
                    tempCount.SetActive(true);
                    listEnemySpawned.Add(tempCount);

                    currentSpawn++;
                    MenuManager.Instance.UpdateEnemyWavePercent(currentSpawn, totalEnemy);

                    yield return new WaitForSeconds(enemySpawn.rate);
                }
            }
        }

        while (isEnemyAlive())
        {
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1);

        GameManager.Instance.Victory();
    }


    bool isEnemyAlive()
    {
        for (int i = 0; i < listEnemySpawned.Count; i++)
        {
            if (listEnemySpawned[i].activeInHierarchy)
            {
                return true;
            }
        }

        return false;
    }
    #region Interface
    public void IGameOver()
    {
        //throw new System.NotImplementedException();
    }

    public void IOnRespawn()
    {
        //throw new System.NotImplementedException();
    }

    public void IOnStopMovingOff()
    {
        //throw new System.NotImplementedException();
    }

    public void IOnStopMovingOn()
    {
        //throw new System.NotImplementedException();
    }

    public void IPause()
    {
        //throw new System.NotImplementedException();
    }

    public void IPlay()
    {
        StartCoroutine(SpawnEnemyCo());
        //throw new System.NotImplementedException();
    }

    public void ISuccess()
    {
        //throw new System.NotImplementedException();
    }

    public void IUnPause()
    {
        //throw new System.NotImplementedException();
    }
    #endregion
}

[System.Serializable]
public class EnemyWave
{
    public float wait = 3;
    public EnemySpawn[] enemySpawns;
}



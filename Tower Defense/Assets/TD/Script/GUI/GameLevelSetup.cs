using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelSetup : MonoBehaviour
{
    public static GameLevelSetup Instance;

    [ReadOnly] public List<LevelWave> levelWaves = new List<LevelWave>();

    #region MonoBehaviour
    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
    private void OnDrawGizmos()
    {
        if (levelWaves.Count != transform.childCount)
        {
            var wave = transform.GetComponentsInChildren<LevelWave>();

            levelWaves = new List<LevelWave>(wave);

            for (int i = 0; i < levelWaves.Count; i++)
            {
                levelWaves[i].level = i + 1;
                levelWaves[i].gameObject.name = "Level " + levelWaves[i].level;
            }
        }
    }
    #endregion
    public EnemyWave[] GetLevelWave()
    {
        foreach(var obj in levelWaves)
        {
            if (obj.level == GlobalValue.levelPlaying)
            {
                return obj.Waves;
            }
        }
        return null;
    }

    public bool isFinalLevel()
    {
        return GlobalValue.levelPlaying == levelWaves.Count;
    }
}

/// <summary>
/// Game manager. 
/// Handle all the actions, parameter of the game
/// You can easy get the state of the game with the IListener script.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int EnemyAlive()
    {
        return enemyAlives.Count;
    }
    public enum GameState { Menu, Playing, GameOver, Success, Pause };
    public GameState State { get; set; }

    [ReadOnly] public int levelStarGot;
    [ReadOnly] public bool isWatchingAd;

    [HideInInspector]
    public List<IListener> listeners;

    [HideInInspector]
    public List<GameObject> enemyAlives;

    [HideInInspector]
    public List<GameObject> listEnemyChasingPlayer;
    public int Point { get; set; }


    #region MonoBehaviour
    void Awake()
    {
        Instance = this;
        State = GameState.Menu;
        listeners = new List<IListener>();
    }

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void OnDisable()
    {
        if (State == GameState.Success)
        {
            GlobalValue.LevelStar(GlobalValue.levelPlaying, levelStarGot);
        }
    }
    #endregion

    #region Listener
    public void AddListener(IListener _listener)
    {
        if (!listeners.Contains(_listener))
        {
            listeners.Add(_listener);
        }
    }
    public void RemoveListener(IListener _listener)
    {
        if (listeners.Contains(_listener))
        {
            listeners.Remove(_listener);
        }
    }
    #endregion
    public void ShowFloatingText(string text, Vector2 positon, Color color)
    {

    }

    public void StartGame()
    {
        State = GameState.Playing;

        var listener_ = FindObjectsOfType<MonoBehaviour>().OfType<IListener>();
        foreach (var _listener in listener_)
        {
            listeners.Add(_listener);
        }

        foreach (var item in listeners)
        {
            item.IPlay();
        }
    }

    public void Gamepause()
    {
        State = GameState.Pause;
        foreach (var item in listeners)
        {
            item.IPause();
        }
    }

    public void UnPause()
    {
        State = GameState.Playing;
        foreach (var item in listeners)
        {
            item.IUnPause();
        }
    }

    public void Victory()
    {
        if (AdsManager.Instance)
        {
            AdsManager.Instance.ShowNormalAd(GameState.Success);
        }

        SoundManager.Instance.PauseMusic(true);
        SoundManager.PlaySfx(SoundManager.Instance.soundVictory, 0.6f);
        State = GameState.Success;

        foreach (var item in listeners)
        {
            if (item != null)
            {
                item.ISuccess();
            }
        }

        GlobalValue.SavedCoins += Point;
        if (GlobalValue.levelPlaying > GlobalValue.LevelPass)
        {
            GlobalValue.LevelPass = GlobalValue.levelPlaying;
        }

    }

    public void GameOver()
    {
        if (AdsManager.Instance)
        {
            AdsManager.Instance.ShowNormalAd(GameState.GameOver);
        }

        SoundManager.Instance.PauseMusic(true);

        if (State == GameState.GameOver)
        {
            return;
        }

        State = GameState.GameOver;

        foreach (var item in listeners)
        {
            item.IGameOver();
        }
    }


    public void RigisterEnemy(GameObject objec)
    {
        enemyAlives.Add(objec);
    }

    public void RemoveEnemy(GameObject objec)
    {
        enemyAlives.Remove(objec);
    }



}

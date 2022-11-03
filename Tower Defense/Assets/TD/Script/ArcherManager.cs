using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ARCHER_ABILITY { NONE, POSION, FREEZE }
public enum ARCHER_FIRE_ARROWS { ONE, DOUBLE, TRIPLE }

public class ArcherManager : MonoBehaviour, IListener
{
    public static ArcherManager Instance;
    public int defaultArchers = 2;

    [ReadOnly] public ARCHER_FIRE_ARROWS numberArrow = ARCHER_FIRE_ARROWS.ONE;
    [ReadOnly] public ARCHER_ABILITY ability = ARCHER_ABILITY.NONE;

    public Player_Archer[] archers;

    [Header("Sound")]
    public AudioClip switchAbilitySound;
    public AudioClip switchArrowSound;

    #region MonoBehaviour
    void Start()
    {
        Instance = this;

        int totalArchers = Mathf.Clamp(defaultArchers + GlobalValue.UpgradeArcher, defaultArchers, archers.Length);
        for(int i = 0; i < archers.Length; i++)
        {
            archers[i].gameObject.SetActive(i < totalArchers ? true : false);
        }
    }
    #endregion
    public void SetNumberArrow(ARCHER_FIRE_ARROWS _numberArrow)
    {
        numberArrow = _numberArrow;
        SoundManager.PlaySfx(switchArrowSound);
    }
    public void SetAbility(ARCHER_ABILITY _ability)
    {
        ability = _ability;
        SoundManager.PlaySfx(switchAbilitySound);
    }


    public void Shoot()
    {
        foreach (var archer in archers)
        {
            if (archer.isAvailable && archer.gameObject.activeInHierarchy)
            {
                archer.Shoot(numberArrow, ability);
                return;
            }
        }
    }
    #region Interface
    public void IPlay()
    {
      
    }

    public void ISuccess()
    {
        foreach (var archer in archers)
        {
            if (archer.gameObject.activeInHierarchy)
                archer.Victory();
        }
    }

    public void IPause()
    {
      
    }

    public void IUnPause()
    {
       
    }

    public void IGameOver()
    {
       
    }

    public void IOnRespawn()
    {
      
    }

    public void IOnStopMovingOn()
    {
       
    }

    public void IOnStopMovingOff()
    {
        
    }
    #endregion
}

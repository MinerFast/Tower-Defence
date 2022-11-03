using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IListener, ICanTakeDamage
{
    #region MonoBehaviour
    public virtual void Start()
    {
    }


    public virtual void Update()
    {
    }

    public virtual void LateUpdate()
    {
    }
    #endregion

    #region Virtual
    public virtual void TakeDamage(float damage, Vector2 force, Vector2 hitPoint, GameObject instigator, BODYPART bodyPart = BODYPART.NONE, WeaponEffect weaponEffect = null) { }

    public virtual void Dead() { }
    public virtual void GetPosion() { }
    public virtual void Hit() { }
    public virtual void GetFrezze() { }
    #endregion
    #region Interface
    public virtual void IPlay()
    {
    }

    public virtual void ISuccess()
    {
    }

    public virtual void IPause()
    {
    }

    public virtual void IUnPause()
    {
    }

    public virtual void IGameOver()
    {
    }

    public virtual void IOnRespawn()
    {
    }

    public virtual void IOnStopMovingOn()
    {
    }

    public virtual void IOnStopMovingOff()
    {
    }
    #endregion
}

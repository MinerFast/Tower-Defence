using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour, IListener
{
    public enum Type { Clk, CClk }


    public float speed = 0.5f;

    public Type rotateType;

    private bool isStop = false;

    #region Interface

    public void IPlay()
    {
        //		throw new System.NotImplementedException ();
    }

    public void ISuccess()
    {
        //		throw new System.NotImplementedException ();
    }

    public void IPause()
    {
        //		throw new System.NotImplementedException ();
    }

    public void IUnPause()
    {
        //		throw new System.NotImplementedException ();
    }

    public void IGameOver()
    {
        //		throw new System.NotImplementedException ();
    }

    public void IOnRespawn()
    {
        //		throw new System.NotImplementedException ();
    }

    public void IOnStopMovingOn()
    {
        isStop = true;

    }

    public void IOnStopMovingOff()
    {
        isStop = false;
    }

    #endregion

    #region MonoBehaviour
    void Update()
    {
        if (isStop)
        {
            return;
        }

        transform.Rotate(Vector3.forward, Mathf.Abs(speed) * (rotateType == Type.CClk ? 1 : -1));
    }
    #endregion
}

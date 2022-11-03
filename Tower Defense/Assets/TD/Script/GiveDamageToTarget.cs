using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDamageToTarget : MonoBehaviour
{
    public int Damage = 20;
    public LayerMask targetLayer;

    public bool multipleDetect = false;

    bool isHit = false;

    #region MonoBehaviour
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHit && !multipleDetect)
        {
            return;
        }

        if (targetLayer != (targetLayer | (1 << collision.gameObject.layer)) || Damage == 0)
        {
            return;
        }

        var takeDamageCount = (ICanTakeDamage)collision.gameObject.GetComponent(typeof(ICanTakeDamage));
        if (takeDamageCount != null)
        {
            takeDamageCount.TakeDamage(Damage, Vector2.zero, transform.position, gameObject);
            isHit = true;
        }
    }
    private void OnEnable()
    {
        isHit = false;
    }
    #endregion
}

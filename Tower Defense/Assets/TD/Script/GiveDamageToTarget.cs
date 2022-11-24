using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDamageToTarget : MonoBehaviour
{
    public int Damage = 20;
    public LayerMask targetLayer;

    public bool multipleDetect = false;

    [HideInInspector] public bool isAttack = false;

    bool isHit = false;

    #region MonoBehaviour
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isAttack)
        {

            //if (isHit && !multipleDetect)
            //{
            //    print("FIRST");
            //    return;
            //}

            //if (targetLayer != (targetLayer | (1 << collision.gameObject.layer)) || Damage == 0)
            //{
            //    return;
            //}

            ICanTakeDamage takeDamageCount = (ICanTakeDamage)collision.gameObject.GetComponent(typeof(ICanTakeDamage));
            print("Sheeesh");
            if (takeDamageCount != null)
            {
                takeDamageCount.TakeDamage(Damage, Vector2.zero, transform.position, gameObject);
                isHit = true;
            }
            isAttack = false;

        }
    }

    private void OnEnable()
    {
        isHit = false;
    }
    #endregion
}

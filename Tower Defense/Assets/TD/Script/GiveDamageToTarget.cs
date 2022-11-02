using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDamageToTarget : MonoBehaviour {
	public int Damage = 20;
	public LayerMask targetLayer;

    public bool multipleDetect = false;
    bool isHit = false;

    private void OnEnable()
    {
        isHit = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.LogError(collision.gameObject);
        if (isHit && !multipleDetect)
            return;

        if (targetLayer != (targetLayer | (1 << collision.gameObject.layer)) || Damage == 0)
            return;

        var takeDamage = (ICanTakeDamage)collision.gameObject.GetComponent(typeof(ICanTakeDamage));
        if (takeDamage != null)
        {
            takeDamage.TakeDamage(Damage, Vector2.zero, transform.position, gameObject);
            isHit = true;
        }
    }

    //   protected virtual void OnTriggerEnter2D (Collider2D other){
    //       Debug.LogError(other.gameObject);
    //       if (isHit && !multipleDetect)
    //           return;

    //	if (targetLayer != (targetLayer | (1 << other.gameObject.layer)) || Damage==0)
    //		return;

    //	var takeDamage = (ICanTakeDamage)other.gameObject.GetComponent (typeof(ICanTakeDamage));
    //	if (takeDamage != null) {
    //           takeDamage.TakeDamage (Damage, Vector2.zero, transform.position, gameObject);
    //           isHit = true;
    //	}
    //}
}

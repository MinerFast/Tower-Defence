using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("ADDP/Enemy AI/Throw Attack")]
public class EnemyThrowAttack : MonoBehaviour
{
    [Header("Grenade")]
    public float angleThrow = 60;
    public float throwForceMin = 290;
    public float throwForceMax = 320;
    public float addTorque = 100;
    public float throwRate = 0.5f;

    public Transform throwPosition;
    public Transform checkPoint;

    public GameObject _Grenade;

    public AudioClip soundAttack;

    float lastShoot = 0;

    public LayerMask targetPlayer;

    public float radiusDetectPlayer = 5;
    public bool isAttacking { get; set; }

    public bool AllowAction()
    {
        return Time.time - lastShoot > throwRate;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(checkPoint.position, radiusDetectPlayer);
    }
    public void Throw(bool isFacingRight)
    {
        Vector3 throwPos = throwPosition.position;

        GameObject objec = Instantiate(_Grenade, throwPos, Quaternion.identity) as GameObject;

        float angle;
        angle = isFacingRight ? angleThrow : 135;

        objec.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        objec.GetComponent<Rigidbody2D>().AddRelativeForce(objec.transform.right * Random.Range(throwForceMin, throwForceMax));
        objec.GetComponent<Rigidbody2D>().AddTorque(objec.transform.right.x * addTorque);

    }
    public void Action()
    {
        if (_Grenade == null)
        {
            return;
        }
        lastShoot = Time.time;

    }
    public bool CheckPlayer()
    {
        RaycastHit2D hitCount = Physics2D.CircleCast(checkPoint.position, radiusDetectPlayer, Vector2.zero, 0, targetPlayer);
        if (hitCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}

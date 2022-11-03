using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

[AddComponentMenu("ADDP/Enemy AI/Range Attack")]
public class EnemyRangeAttack : MonoBehaviour
{
    public LayerMask enemyLayer;

    public Transform checkPoint;

    [SpineBone]
    public string targetBone = "hip";

    public AudioClip soundShoot;

    [Range(0, 1)]
    public float soundShootVolume = 0.5f;

    public Transform shootingPoint;

    public float damage = 30;
    public float detectDistance = 5;

    public Projectile bullet;

    [HideInInspector] public float shootingRate = 1;
    [HideInInspector] public int multiShoot = 1;
    [HideInInspector] public float multiShootRate = 0.2f;

    float lastShoot = 0;

    [HideInInspector] public GameObject GunObj;

    Vector3 dir = Vector3.right;
    public bool isAttacking { get; set; }

    private Bone bone;
    private SkeletonMecanim skeletonAnimation;

    public Vector3 firePosition()
    {
        Vector3 firePoint = bone.GetWorldPosition(skeletonAnimation.transform);
        return firePoint;
    }
    #region MonoBehaviour
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonMecanim>();
        bone = skeletonAnimation.skeleton.FindBone(targetBone);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawRay(checkPoint.position, dir * detectDistance);
    }
    #endregion

    public bool AllowAction()
    {
        return Time.time - lastShoot > shootingRate;
    }

    public bool CheckPlayer(bool isFacingRight)
    {
        dir = isFacingRight ? Vector2.right : Vector2.left;


        RaycastHit2D hit = Physics2D.Raycast(checkPoint.position, dir, detectDistance, enemyLayer);
        if (hit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Action()
    {
        isAttacking = true;
        lastShoot = Time.time;
    }

    /// <summary>
    /// called by Enemy
    /// </summary>
    public void Shoot(bool isFacingRight)
    {
        StartCoroutine(ShootCo(isFacingRight));
    }

    IEnumerator ShootCo(bool isFacingRight)
    {
        for (int i = 0; i < multiShoot; i++)
        {
            SoundManager.PlaySfx(soundShoot, soundShootVolume);

            float shootAngle;
            shootAngle = isFacingRight ? 0 : 180;

            var projectil = SpawnSystemHelper.GetNextObject(bullet.gameObject, false).GetComponent<Projectile>();
            var magicCount = 1;

            projectil.transform.position = shootingPoint != null ? shootingPoint.position : firePosition();
            projectil.transform.rotation = Quaternion.Euler(0, 0, shootAngle);
            projectil.Initialize(gameObject, Vector2.right * (isFacingRight ? magicCount : -magicCount), Vector2.zero, false, damage, damage, 0);
            projectil.gameObject.SetActive(true);

            yield return new WaitForSeconds(multiShootRate);
        }

        CancelInvoke();

        Invoke("EndAttack", 1);
    }

    void EndAttack()
    {
        isAttacking = false;
    }

}

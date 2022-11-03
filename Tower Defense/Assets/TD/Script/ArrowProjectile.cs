using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : Projectile, IListener, ICanTakeDamage
{
    private WeaponEffect weaponEffect;
    public Sprite hitImageBlood;
    public SpriteRenderer arrowImage;
    Vector2 oldPos;
    public GameObject DestroyEffect;
    public int pointToGivePlayer;
    public float timeToLive = 3;
    public AudioClip soundHitEnemy;
    [Range(0, 1)]
    public float soundHitEnemyVolume = 0.5f;
    public AudioClip soundHitNothing;
    [Range(0, 1)]
    public float soundHitNothingVolume = 0.5f;

    public GameObject ExplosionObj;
    float timeToLiveCounter = 0;
    public bool parentToHitObject = true;

    bool isHit = false;
    Rigidbody2D rig;

    public Vector2 checkTargetDistanceOffset = new Vector2(-0.25f, 0);
    public float checkTargetDistance = 1;
    #region MonoBehaviour
    public void Init(Vector2 velocityForce, float gravityScale, WeaponEffect _weaponEffect)
    {
        weaponEffect = _weaponEffect;

        rig = GetComponent<Rigidbody2D>();
        rig.gravityScale = gravityScale;
        rig.velocity = velocityForce;
    }

    void OnEnable()
    {
        timeToLiveCounter = timeToLive;
        isHit = false;

        if (rig == null)
            rig = GetComponent<Rigidbody2D>();
        rig.isKinematic = false;
    }

    void Start()
    {
        oldPos = transform.position;

        GameManager.Instance.listeners.Add(this);
    }


    void Update()
    {
        if (isHit)
        {
            return;
        }

        if ((Vector2)transform.position != oldPos)
        {
            transform.right = ((Vector2)transform.position - oldPos).normalized;
        }

        RaycastHit2D hit = Physics2D.Linecast(oldPos, transform.position, LayerCollision);

        if (hit)
        {
            Hit(hit);
            isHit = true;
        }

        oldPos = transform.position;

        if ((timeToLiveCounter -= Time.deltaTime) <= 0)
        {
            DestroyProjectile();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine((Vector2)transform.position + checkTargetDistanceOffset, (Vector2)transform.position + checkTargetDistanceOffset + (Vector2)transform.right * checkTargetDistance);
    }
    #endregion
    void Hit(RaycastHit2D other)
    {
        transform.position = other.point + (Vector2)(transform.position - transform.Find("head").position);

        var takeBodyDamageCount = (ICanTakeDamageBodyPart)other.collider.gameObject.GetComponent(typeof(ICanTakeDamageBodyPart));
        if (takeBodyDamageCount != null)
        {
            OnCollideTakeDamageBodyPart(other.collider, takeBodyDamageCount);
        }
        else
        {
            var takeDamageCount = (ICanTakeDamage)other.collider.gameObject.GetComponent(typeof(ICanTakeDamage));
            if (takeDamageCount != null)
            {
                OnCollideTakeDamage(other.collider, takeDamageCount);


            }
            else
                OnCollideOther(other.collider);
        }
    }

    IEnumerator DestroyProjectile(float delay = 0)
    {
        var rigibody = GetComponent<Rigidbody2D>();
        rigibody.velocity = Vector2.zero;
        rigibody.isKinematic = true;

        yield return new WaitForSeconds(delay);
        if (DestroyEffect != null)
        {
            SpawnSystemHelper.GetNextObject(DestroyEffect, true).transform.position = transform.position;
        }

        if (Explosion)
        {
            Instantiate(ExplosionObj, transform.position, Quaternion.identity);
        }

        gameObject.SetActive(false);
    }

    public void TakeDamage(float damage, Vector2 force, Vector2 hitPoint, GameObject instigator, BODYPART bodyPart = BODYPART.NONE)
    {
        SoundManager.PlaySfx(soundHitNothing, soundHitNothingVolume);
        StartCoroutine(DestroyProjectile(1));
    }


    protected override void OnCollideTakeDamage(Collider2D other, ICanTakeDamage takedamage)
    {
        base.OnCollideTakeDamage(other, takedamage);
        takedamage.TakeDamage(float.MaxValue, Vector2.zero, transform.position, Owner);
        SoundManager.PlaySfx(soundHitEnemy, soundHitEnemyVolume);
        StartCoroutine(DestroyProjectile(0));
    }

    protected override void OnCollideOther(Collider2D other)
    {
        SoundManager.PlaySfx(soundHitNothing, soundHitNothingVolume);
        StartCoroutine(DestroyProjectile(3));

        if (parentToHitObject)
        {
            transform.parent = other.gameObject.transform;
        }

    }
    protected override void OnCollideTakeDamageBodyPart(Collider2D other, ICanTakeDamageBodyPart takedamage)
    {

        base.OnCollideTakeDamageBodyPart(other, takedamage);

        float normalDamage = (int)Random.Range(weaponEffect.normalDamageMin, weaponEffect.normalDamageMax);

        takedamage.TakeDamage(normalDamage, force, transform.position, Owner, weaponEffect);

        StartCoroutine(DestroyProjectile(5));

        if (parentToHitObject)
        {
            transform.parent = other.gameObject.transform;
        }

        if (arrowImage && hitImageBlood)
        {
            arrowImage.sprite = hitImageBlood;
        }
    }

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

    }

    public void IOnStopMovingOff()
    {
    }

    public void TakeDamage(float damage, Vector2 force, Vector2 hitPoint, GameObject instigator, BODYPART bodyPart = BODYPART.NONE, WeaponEffect weaponEffect = null)
    {
        StartCoroutine(DestroyProjectile(0));
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity.Examples;

public class CharacterBodyPart : MonoBehaviour, ICanTakeDamageBodyPart
{
    public GameObject owner;
    public BODYPART _bodyPart;
    public int multipleDamageX = 1;
    public GameObject hitFX;
    public GameObject ownerHitFX;

    public bool allowPushBack = false;
    public bool allowKnockDown = false;
    public bool allowShock = false;

    private Enemy ownerDamage;

    void Awake()
    {
        ownerDamage = owner.GetComponent<Enemy>();
        if (ownerHitFX)
            ownerHitFX.SetActive(false);
    }
    public void TakeDamage(float damageCount, Vector2 forceCount, Vector2 hitPosition, GameObject instigator, WeaponEffect weaponEffect = null, float pushBackPercent = 0, float knockDownRagdollPercent = 0, float shockPercent = 0)
    {
        damageCount *= multipleDamageX;

        ownerDamage.TakeDamage(damageCount, hitPosition, forceCount, instigator, _bodyPart, weaponEffect);

        if (hitFX)
        {
            SpawnSystemHelper.GetNextObject(hitFX, true).transform.position = hitPosition;
        }

        if (ownerHitFX)
        {
            ownerHitFX.SetActive(false);
            ownerHitFX.SetActive(true);
        }
        
    }

   

}

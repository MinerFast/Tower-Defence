using UnityEngine;
using System.Collections;

public interface ICanTakeDamage
{
    void TakeDamage(float damage, Vector2 force, Vector2 hitPoint, GameObject instigator, BODYPART bodyPart = BODYPART.NONE, WeaponEffect weaponEffect = null);
}

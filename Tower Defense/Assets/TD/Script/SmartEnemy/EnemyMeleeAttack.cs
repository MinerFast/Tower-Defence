using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("ADDP/Enemy AI/Melee Attack")]
public class EnemyMeleeAttack : MonoBehaviour {
	public LayerMask targetPlayer;
	public Transform checkPoint;
	public GameObject meleeDamageObj;
	public float detectDistance = 1;
	public float meleeRate = 1;
	float lastShoot = -999;
	public bool isAttacking { get; set; }
	[HideInInspector] public GameObject MeleeObj;
    [Range(0, 1)]
    public float soundAttacksVol = 0.5f;
    public AudioClip[] soundAttacks;

	void Start(){
        meleeDamageObj.SetActive (false);
	}

	public bool AllowAction(){
		return Time.time - lastShoot > meleeRate;
	}

	// Update is called once per frame
	public bool CheckPlayer (bool isFacingRight) {
        Debug.DrawRay(checkPoint.position, (isFacingRight ? Vector2.right : Vector2.left) * detectDistance);
		RaycastHit2D hit = Physics2D.Raycast (checkPoint.position, isFacingRight ? Vector2.right : Vector2.left, detectDistance, targetPlayer);
        if (hit)
            return true;
        else
            return false;
	}

	public void Action(){
		
		
		lastShoot = Time.time;
	}


	void EndAttack(){
		isAttacking = false;
	}

	/// <summary>
	/// Called by Enemy
	/// </summary>
	public void Check4Hit(){
        meleeDamageObj.SetActive (true);
        if (soundAttacks.Length > 0)
            SoundManager.PlaySfx(soundAttacks[Random.Range(0, soundAttacks.Length)], soundAttacksVol);

    }

	public void EndCheck4Hit(){
        meleeDamageObj.SetActive (false);

		CancelInvoke ();
		Invoke ("EndAttack", 1);
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position + Vector3.right * detectDistance * -1, 0.2f);
	}
}

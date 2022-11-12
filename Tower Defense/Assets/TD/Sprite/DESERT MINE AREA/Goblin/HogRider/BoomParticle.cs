using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomParticle : MonoBehaviour
{
    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(DeleteBoom());

        }
    }
    IEnumerator DeleteBoom()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}

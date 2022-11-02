using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class SpineAnimation : MonoBehaviour
{
    public string animationName = "Fx3";
    public float disableTime = 1;

    SkeletonAnimation skeleton;
    // Start is called before the first frame update
    void Awake()
    {
        skeleton = GetComponent<SkeletonAnimation>();
    }

    private void OnEnable()
    {
        skeleton.state.SetAnimation(0, animationName, false);
        Invoke("DisableCo", disableTime);
    }

    private void DisableCo()
    {
        gameObject.SetActive(false);
    }
}

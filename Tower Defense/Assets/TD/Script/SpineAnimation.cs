using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class SpineAnimation : MonoBehaviour
{
    public string animationName = "Fx3";

    public float disableTime = 1;

    private SkeletonAnimation skeleton;

    #region MonoBehaviour
    private void OnEnable()
    {
        skeleton.state.SetAnimation(0, animationName, false);
        Invoke("DisableCo", disableTime);
    }
    void Awake()
    {
        skeleton = GetComponent<SkeletonAnimation>();
    }
    #endregion

    private void DisableCo()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class PlayerSetIKPosition : MonoBehaviour
{
    [SpineBone]
    public string boneName;

    private Bone bone;

    public Vector3 targetPosition;
    #region MonoBehaviour
    void Start()
    {
        SkeletonMecanim skeletonAnimation = GetComponent<SkeletonMecanim>();
        bone = skeletonAnimation.Skeleton.FindBone(boneName);
        skeletonAnimation.UpdateLocal += SkeletonAnimation_UpdateLocal;
    }
    #endregion

    public void SetAnimIK(Vector3 pos)
    {
        targetPosition = pos - transform.position;
    }
    public void SetAnimIK()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition = mousePos - transform.position;
    }



    void SkeletonAnimation_UpdateLocal(ISkeletonAnimation animated)
    {
        bone.SetLocalPosition(targetPosition);
    }
}

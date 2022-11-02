using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class PlayerSetIKPosition : MonoBehaviour
{
    [SpineBone]
    public string boneName;
    Bone bone;
    public Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        SkeletonMecanim skeletonAnimation = GetComponent<SkeletonMecanim>();
        bone = skeletonAnimation.Skeleton.FindBone(boneName);
        skeletonAnimation.UpdateLocal += SkeletonAnimation_UpdateLocal;
    }

    public void SetAnimIK()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition = mousePos - transform.position;
    }

    public void SetAnimIK(Vector3 pos)
    {
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition = pos - transform.position;
        //Debug.LogError("targetPosition" + targetPosition);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetMouseButton(0))
    //    {

    //    }
    //}

    void SkeletonAnimation_UpdateLocal(ISkeletonAnimation animated)
    {
        //var localPositon = transform.InverseTransformPoint(targetPosition);
        bone.SetLocalPosition(targetPosition);
    }
}

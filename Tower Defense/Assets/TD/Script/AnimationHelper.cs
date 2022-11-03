using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper
{

    public static float getAnimationLength(Animator animator, string animationName)
    {
        if (animator.isInitialized)
        {
            RuntimeAnimatorController ac = animator.runtimeAnimatorController;
            for (int i = 0; i < ac.animationClips.Length; i++)
            {
                if (ac.animationClips[i].name == animationName)
                {
                    return ac.animationClips[i].length;
                }
            }
        }
        return 0;
    }

    public static float getCurrentStateTime(Animator animator, int layer)
    {
        AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(layer);
        AnimatorClipInfo[] myAnimatorClip = animator.GetCurrentAnimatorClipInfo(layer);

        float myTime = myAnimatorClip[layer].clip.length * animationState.normalizedTime;

        return myTime;
    }
}

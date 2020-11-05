using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorUtility
{
    static public void PlayAnimation(Animator animator, string animationName,  int layer = 0)
    {
        animator.Play(animationName, layer);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayControllerAnimations : AbstractReplayableMono
{

    public Animator [] m_animations;

    public override void ResetToZero()
    {
        for (int i = 0; i < m_animations.Length; i++)
        {
            m_animations[i].Play(CurrentAnimationName(in m_animations[i]), 0, 0);
        }
    }

    public override void UpdateTime(in float previousTime, in float deltaTime, in float currentTime)
    {
        for (int i = 0; i < m_animations.Length; i++)
        {
            m_animations[i].Play(CurrentAnimationName(in m_animations[i]), 0, currentTime);
        }
    }

  

    string CurrentAnimationName(in Animator animator)   
    {
        var currAnimName = "";
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(clip.name))
            {
                currAnimName = clip.name.ToString();
            }
        }

        return currAnimName;

    }
}

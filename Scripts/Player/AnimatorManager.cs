using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DX
{
    public class AnimatorManager : MonoBehaviour
    {
        public Animator anim;
        
        public void PlayTargetAnimation(string targetAnim, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool("isInteracting", isInteracting);
            anim.CrossFade(targetAnim, 0.2f);
        }
        public void PlayEnemyTargetAnimation(string targetAnim, bool isPerforming)
        {
            anim.applyRootMotion = isPerforming;
            anim.SetBool("isPerforming", isPerforming);
            anim.CrossFade(targetAnim, 0.2f);
        }
    }
}

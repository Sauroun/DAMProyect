using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DX
{
    public class EnemyAnimatorHandler : AnimatorManager
    {
        
        EnemyLocomotionManager enemyLocomotionManager;
        private void Awake()
        {
            anim = GetComponent<Animator>();
            enemyLocomotionManager = GetComponentInParent<EnemyLocomotionManager>();
        }
        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            if (delta > 0)
            {
                enemyLocomotionManager.enemyRigidbody.drag = 0;
                Vector3 deltaPosition = anim.deltaPosition;
                deltaPosition.y = 0;
                Vector3 velocity = deltaPosition / delta;
                enemyLocomotionManager.enemyRigidbody.velocity = velocity;
            }
        }
    }
}

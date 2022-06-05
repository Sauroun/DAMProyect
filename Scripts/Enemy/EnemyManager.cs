using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DX
{
    public class EnemyManager : MonoBehaviour
    {
        EnemyLocomotionManager enemyLocomotionManager;
        EnemyAnimatorHandler enemyAnimatorHandler;
        EnemyStats enemyStats;
        Animator anim;
        public bool isPerforming;

        public EnemyAttackAction[] enemyAttacks;
        public EnemyAttackAction currentAttack;

        [Header("A.I. Settings")]
        public float detectionRadius = 20;
        public float maximumDetectionAngle = 50;
        public float minimumDetectionAngle = -50;

        public float currentRecoveryTime = 1;

        private void Awake()
        {
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
            anim = GetComponentInChildren<Animator>();
            enemyStats = GetComponent<EnemyStats>();
        }

        private void Update()
        {
            isPerforming = anim.GetBool("isPerforming");
            HandleRecoveryTimer();
        }

        private void FixedUpdate()
        {
            if (!enemyStats.isDead)
            {
                HandleCurrentAction();
            }
            
        }

        private void HandleCurrentAction()
        {
            if (enemyLocomotionManager.currentTarget != null)
            {
                enemyLocomotionManager.distanceFromTarget = Vector3.Distance(enemyLocomotionManager.currentTarget.transform.position, transform.position);
            }
            if (enemyLocomotionManager.currentTarget==null)
            {

                enemyLocomotionManager.HandleDetection();
                
            }
            else if(enemyLocomotionManager.distanceFromTarget > enemyLocomotionManager.stoppingDistance)
            {
                
                enemyLocomotionManager.HandleMoveToTarget();
            }
            else if(enemyLocomotionManager.distanceFromTarget <= enemyLocomotionManager.stoppingDistance)
            {
                
                AttackTarget();
            }
        }

        private void HandleRecoveryTimer()
        {
            if (currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }

            if (isPerforming)
            {
                if (currentRecoveryTime <=0)
                {
                    isPerforming = false;
                }
            }
        }

        #region Attacks

        private void AttackTarget()
        {
            if (isPerforming)
            {
                
                return;
            }
            if (currentAttack == null)
            {
                
                GetNewAttack();
            }
            else
            { 
                isPerforming = true;
                currentRecoveryTime = currentAttack.recoveryTime;
                enemyAnimatorHandler.PlayEnemyTargetAnimation(currentAttack.actionAnimation, true);
                currentAttack = null;
            }
        }
        private void GetNewAttack()
        {
            Vector3 targetsDirection = enemyLocomotionManager.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targetsDirection, transform.forward);
            enemyLocomotionManager.distanceFromTarget = 
                Vector3.Distance(enemyLocomotionManager.currentTarget.transform.position, transform.position);

            int maxScore = 0;
            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];
                if (enemyLocomotionManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack 
                    && enemyLocomotionManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viewableAngle <= enemyAttackAction.maximumAttackAngle && viewableAngle 
                        >= enemyAttackAction.minimumAttackAngle)
                    {
                        maxScore += enemyAttackAction.attackScore;
                    }
                }
            }
            int randomValue = Random.Range(0, maxScore);
            int temporaryScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];
                if (enemyLocomotionManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                    && enemyLocomotionManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viewableAngle <= enemyAttackAction.maximumAttackAngle && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                    {
                        if(currentAttack != null)
                        {
                            return;
                        }
                        temporaryScore += enemyAttackAction.attackScore;

                        if (temporaryScore > randomValue)
                        {
                            currentAttack = enemyAttackAction;
                        }
                    }
                }
            }
        }
        #endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DX
{
    public class EnemyLocomotionManager : MonoBehaviour
    {
        EnemyManager enemyManager;
        EnemyAnimatorHandler enemyAnimatorHandler;
        public NavMeshAgent navMeshAgent;
        public Rigidbody enemyRigidbody;

        public CharacterStats currentTarget;
        public LayerMask detectionLayer;

        public float distanceFromTarget;
        public float stoppingDistance = 1;

        public float rotationSpeed = 15;
        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
            
            enemyRigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            navMeshAgent.enabled = false;
            enemyRigidbody.isKinematic = false;
        }
       
        public void HandleDetection()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();

                if (characterStats != null)
                {
                    Vector3 targetDirection = characterStats.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                    if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                    {
                        currentTarget = characterStats;
                        enemyAnimatorHandler.PlayEnemyTargetAnimation("Scream", true);
                        
                    }
                }
            }
        }

        public void HandleMoveToTarget()
        {
            if (enemyManager.isPerforming)
            {
                return;
            }
            
            Vector3 targetDirection = currentTarget.transform.position - transform.position;
            distanceFromTarget = Vector3.Distance(currentTarget.transform.position, this.transform.position);
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

            if (enemyAnimatorHandler.anim.GetBool("isPerforming"))
            {
                enemyAnimatorHandler.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                navMeshAgent.enabled = false;
            }
            else
            {
                if (distanceFromTarget > stoppingDistance)
                {
                    enemyAnimatorHandler.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);


                }
                else if(distanceFromTarget <= stoppingDistance)
                {
                    enemyAnimatorHandler.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                }
            }
            HandleRotateTowardsTarget();
            navMeshAgent.transform.localPosition = Vector3.zero;
            navMeshAgent.transform.localRotation = Quaternion.identity;
        }

        private void HandleRotateTowardsTarget()
        {
            if (enemyManager.isPerforming)
            {
                Vector3 direction = currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed / Time.deltaTime);
            }
            else
            {
               // Vector3 relativeDirection = transform.InverseTransformDirection(navMeshAgent.desiredVelocity);
               // Vector3 targetVelocity = enemyRigidbody.velocity;

                
                navMeshAgent.enabled = true;
                navMeshAgent.SetDestination(currentTarget.transform.position);
                enemyRigidbody.velocity = navMeshAgent.desiredVelocity;
                transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation, rotationSpeed/Time.deltaTime);
            }

            
        }
        
       
    }
}

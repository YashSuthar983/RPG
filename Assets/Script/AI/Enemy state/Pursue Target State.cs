using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SG
{
    public class PursueTargetState : State
    {
        public CombateStanceState combateStanceState;
        public IdleState idleState;
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            
            if (enemyManager.isInteracting)
            {
                return this;
            }
            //cahse target 
            if (enemyManager.isPerformingAction)
            {
                enemyAnimatorManager.anim.SetFloat("Vertical",0,0.1f,Time.deltaTime);
                return this ;
            }
            if(enemyManager.currentTarget.isDead)
            {
                return idleState ;
            }

            Vector3 targdetDir = enemyManager.currentTarget.transform.position -enemyManager.transform.position;
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position,enemyManager.transform.position);

            float viewableAngle = Vector3.Angle(targdetDir,enemyManager.transform.forward);

            if (distanceFromTarget > enemyManager.maximumAttackRange)
            {
                enemyAnimatorManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            }
            

            HandleRotateTowardsTarget(enemyManager);


            //within attcak range switch to combet state

            if(distanceFromTarget <=enemyManager.maximumAttackRange)
            {
                return combateStanceState;
            }
            else
            {
                return this;
            }


        }

        private void HandleRotateTowardsTarget(EnemyManager enemyManager)
        {
            if (enemyManager.isPerformingAction)
            {
                Vector3 direction = enemyManager.currentTarget.transform.position -enemyManager.transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = enemyManager.transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);

            }
            else
            {
                Vector3 realtiveDir = transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyManager.rb.velocity;

                enemyManager.navMeshAgent.enabled = true;
                enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
                enemyManager.rb.velocity = targetVelocity;
                enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
            }

            


        }
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FPS.Control
{
    public class EnemyAI : MonoBehaviour
    {
        Transform target;
        NavMeshAgent navMeshAgent;
        [SerializeField] float chaseRange = 5f;
        [SerializeField] float turnSpeed = 3f; 
        float distanceToTarget = Mathf.Infinity;
        public bool isUsingChaseRange = false;
        bool isProvoked = false;
       
        Animator anim;

        [Header("Ranged")]
        [SerializeField] float weaponRange = 5f;
        public bool isRanged = false;

        private void Awake() 
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            navMeshAgent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            distanceToTarget = Vector3.Distance(target.position, transform.position);
            if(isUsingChaseRange)
            {
                if(isProvoked)
                {
                   Engage();
                }
               else if (distanceToTarget <= chaseRange)
                {
                    isProvoked = true;
                } 
            }
            else
            {
                Engage();
            }   
            UpdateAnim();
        }

        public void OnDamageTaken()
        {
            isProvoked = true;
        }

        private void Engage()
        {
            FaceTarget();
            if(distanceToTarget >= navMeshAgent.stoppingDistance)
            {
                ChaseTarget();
            }
            if(isRanged)
            {
                if(distanceToTarget <= weaponRange)
                {
                    AttackTarget();
                }
            }
            if(distanceToTarget <= navMeshAgent.stoppingDistance)
            {
                AttackTarget();
            }
        }

        private void ChaseTarget()
        {
            navMeshAgent.SetDestination(target.position);
        }

        private void AttackTarget()
        {
            //Debug.Log(name + " starting Attack on " + target.name);
            anim.SetTrigger("isAttacking");
        }

        private void FaceTarget()
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }

        private void UpdateAnim()
        {
            Vector3 objectVelocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(objectVelocity);
            float speed = localVelocity.z;
            anim.SetFloat("forwardSpeed", speed);
        }

        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }

        public void LaunchProjectile()
        {
            

        }
    }
}


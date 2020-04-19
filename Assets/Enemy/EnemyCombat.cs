using System;
using Boo.Lang;
using Player;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


namespace Enemy
{
    public class EnemyCombat : MonoBehaviour
    {
        public float lookRadius = 15f;

        public Animator animator;

        Transform _target;
        NavMeshAgent _agent;
        private AudioManager _audioManager;
        public Rigidbody rb;
    
        public static bool AttackingWithFoot = false;
        public static bool EnemyIsAttacking;

        private int currentAttackType;
        
        // Start is called before the first frame update
        void Start()
        {
            _target = PlayerManager.instance.player.transform;
            _agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            _audioManager = FindObjectOfType<AudioManager>();
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate() //every 2 seconds 
        {
            if (!EnemyIsAttacking)
            {
                currentAttackType = GetRandomAttack();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (EnemyStats.EnemyIsDead)
            {
                enabled = false;
                return;
            }

            FollowPlayer();
        }

        private void FollowPlayer()
        {
            float distance = Vector3.Distance(_target.position, transform.position);
            
            if (distance <= lookRadius)
            {
                SetRunningAnimation(true, _agent.velocity.magnitude);
                _agent.SetDestination(_target.position);

                if (distance <= _agent.stoppingDistance + 4)
                {
                    _agent.SetDestination(transform.position);
                    FaceTarget();

                    SetRunningAnimation(false, _agent.velocity.magnitude);
                    
                    SetAttackingAnimation(true);
                }
            }
            else
            {
                SetAttackingAnimation(false);
                SetRunningAnimation(false, _agent.velocity.magnitude);
            }
        }

        private void SetRunningAnimation(bool isRunning, float magnitude)
        {
            {
                if (isRunning)
                {
                    animator.SetFloat(EnemyAnimHash.RunningHash, magnitude);
                    _audioManager.Play("EnemyFootSteps");
                }
                else
                {
                    animator.SetFloat(EnemyAnimHash.RunningHash, magnitude);
                    _audioManager.Stop("EnemyFootSteps");
                }
            }
        }
        
        private void SetAttackingAnimation(bool isAttacking)
        {
            {
                if (isAttacking && !PlayerCombat.IsDead)
                {
                    animator.SetBool(currentAttackType, true);
                    _audioManager.Play("GorillaRoar");
                    
                    EnemyIsAttacking = true;
                }
                else
                {
                    animator.SetBool(currentAttackType, false);
                    _audioManager.Stop("GorillaRoar");
                    
                    EnemyIsAttacking = false;
                }
            }
        }
        
        private int GetRandomAttack()
        {
            var value = Random.Range(0, 3);

            if (value == 0)
            {
                Debug.Log("Stab");
                return EnemyAnimHash.StabHash;
            }
            if (value == 1)
            {
                Debug.Log("ComboKick");
                return EnemyAnimHash.ComboKickHash;
            }
            if (value == 2)
            {
                Debug.Log("Swipe");
                return EnemyAnimHash.QuickSwipeHash;
            }

            return EnemyAnimHash.StabHash;
            
        }
        

        void FaceTarget()
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        //radius around enemy 
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, lookRadius);
        }
    }
}
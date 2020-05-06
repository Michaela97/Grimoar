using System;
using System.Collections;
using Boo.Lang;
using Player;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


namespace Enemy
{
    public class EnemyCombat : MonoBehaviour
    {
        [SerializeField] private float lookRadius = 15f;
        public Animator animator;

        Transform _target;
        NavMeshAgent _agent;
        private AudioManager _audioManager;

        public static bool AttackingWithFoot;
        public static bool EnemyIsAttacking;

        private int _currentAttackType;
        private delegate void AttackDelegate();
        private AttackDelegate _attackDelegate;

        // Start is called before the first frame update
        void Start()
        {
            _target = PlayerManager.instance.player.transform;
            _agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            _audioManager = FindObjectOfType<AudioManager>();

            _attackDelegate += IsDead;
        }

        private void FixedUpdate() //every 2 seconds 
        {
            if (!EnemyIsAttacking)
            {
                _currentAttackType = GetRandomAttack();
            }
        }

        // Update is called once per frame
        void Update()
        {
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
                    _attackDelegate += PerformAttack;
                    _attackDelegate += FaceTarget;
                    _attackDelegate?.Invoke();
                }
            }
            else
            {
                if (_attackDelegate != null)
                {
                    _attackDelegate -= PerformAttack;
                    _attackDelegate -= FaceTarget;
                    _attackDelegate += StopAttacking;
                    _attackDelegate?.Invoke();
                }
            }
        }

        private void PerformAttack()
        {
            _agent.SetDestination(transform.position);

            SetRunningAnimation(false, _agent.velocity.magnitude);
            SetAttackingAnimation(true);
        }

        private void StopAttacking()
        {
            SetRunningAnimation(false, _agent.velocity.magnitude);
            SetAttackingAnimation(false);
        }

        private void IsDead()
        {
            if (EnemyStats.EnemyIsDead)
            {
                enabled = false;
            }
        }


        private void SetRunningAnimation(bool isRunning, float magnitude)
        {
            if (isRunning)
            {
                _audioManager.Play("EnemyFootSteps");
            }
            else
            {
                _audioManager.Stop("EnemyFootSteps");
            }

            animator.SetFloat(EnemyAnimHash.RunningHash, magnitude);
        }

        private void SetAttackingAnimation(bool isAttacking)
        {
            {
                if (isAttacking && !PlayerCombat.IsDead && !EnemyStats.EnemyIsDead)
                {
                    _audioManager.Play("GorillaRoar");
                    animator.SetBool(_currentAttackType, true);
                    EnemyIsAttacking = true;
                }
                else
                {
                    _audioManager.Stop("GorillaRoar");
                    animator.SetBool(_currentAttackType, false);
                    EnemyIsAttacking = false;
                }
            }
        }

        private int GetRandomAttack()
        {
            var value = Random.Range(0, 3);

            if (value == 0)
            {
                AttackingWithFoot = false;
                return EnemyAnimHash.StabHash;
            }

            if (value == 1)
            {
                AttackingWithFoot = true;
                return EnemyAnimHash.ComboKickHash;
            }

            if (value == 2)
            {
                AttackingWithFoot = false;
                return EnemyAnimHash.QuickSwipeHash;
            }

            //default
            return EnemyAnimHash.StabHash;
        }

        private void FaceTarget()
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
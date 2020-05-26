using System.Collections;
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

        private static event AttackDelegate AttackAction;

        // Start is called before the first frame update
        void Start()
        {
            _target = PlayerManager.instance.player.transform;
            _agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            _audioManager = FindObjectOfType<AudioManager>();

            //GetRandomAttack();
            StartCoroutine(GetRandomAttack());
        }


        // Update is called once per frame
        void Update()
        {
            FollowPlayer();
            IsDead(); //disable script if its dead
            AttackAction?.Invoke();
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
                    AttackAction += PerformAttack;
                    AttackAction += FaceTarget;
                }
            }
            else
            {
                if (AttackAction != null)
                {
                    AttackAction -= PerformAttack;
                    AttackAction -= FaceTarget;
                    AttackAction += StopAttack;
                }
            }
        }

        private void PerformAttack()
        {
            EnemyIsAttacking = true;
            _agent.SetDestination(transform.position);
            
            SetRunningAnimation(false, _agent.velocity.magnitude);
            SetAttackingAnimation(true);
        }

        private void StopAttack()
        {
            EnemyIsAttacking = false;
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

        private IEnumerator GetRandomAttack()
        {
            while (true)
            {
                var value = Random.Range(0, 3);
        
                switch (value)
                {
                    case 0:
                        AttackingWithFoot = false;
                        _currentAttackType = EnemyAnimHash.StabHash;
                        break;
                    case 1:
                        AttackingWithFoot = true;
                        _currentAttackType = EnemyAnimHash.ComboKickHash;
                        break;
                    case 2:
                        AttackingWithFoot = false;
                        _currentAttackType = EnemyAnimHash.QuickSwipeHash;
                        break;
                }
                yield return new WaitForSeconds(3);
            }
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
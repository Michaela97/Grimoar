using System;
using System.Runtime.InteropServices;
using Enemy;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        public static bool IsAttacking;
        public static bool IsDead;
        
        private PlayerStats _playerStats;
        [SerializeField]
        private Animator animator;
        public delegate void DieEvent();
        public static event DieEvent hasDied;

        private PlayerController _playerController;

        void Start()
        {
            animator = GetComponent<Animator>();
            _playerStats = gameObject.AddComponent<PlayerStats>();
            _playerController = FindObjectOfType<PlayerController>();
            
            hasDied += SetDyingAnimation;
            hasDied += DisableScripts;
        }
        
        void Update()
        {
            Attack();
            
            if (_playerStats.GetCurrentHealth() == 0)
            {
                Debug.Log("Player is dead");
                
                IsDead = true;
                hasDied?.Invoke();
            }
        }

        private void Attack()
        {
            if (!IsDead)
            {
                var input = Input.GetMouseButton(0);
                IsAttacking = input;
                SetAttackingAnimation(IsAttacking); 
            }
        }
        
        private void DisableScripts()
        {
            enabled = false;
            _playerController.enabled = false;
        }

        private void SetDyingAnimation()
        {
            animator.SetTrigger(PlayerAnimHash.deadHash);
        }

        private void SetAttackingAnimation(bool value)
        {
            animator.SetBool(PlayerAnimHash.attackHash, value);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("EnemyHands"))
            {
                if (EnemyCombat.EnemyIsAttacking)
                {
                    Debug.Log("Player was hit by enemy hands");
                    _playerStats.TakeDamage(10);
                }
            }

            if (other.gameObject.CompareTag("EnemyFoot"))
            {
                if (EnemyCombat.AttackingWithFoot && EnemyCombat.EnemyIsAttacking)
                {
                    Debug.Log("Player was hit by enemy foot");
                    _playerStats.TakeDamage(20);
                }
            }
        }
    }
}
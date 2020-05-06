using System;
using Enemy;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        public static bool IsAttacking;
        public static bool IsDead;
        
        private int attackHash = Animator.StringToHash("Attack");
        private int deadHash = Animator.StringToHash("IsDead");
        
        private PlayerStats _playerStats;
        
        public Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
            _playerStats = gameObject.AddComponent<PlayerStats>();
        }

    
        void Update()
        {
            Attack();
            Die();
        }
        
        private void Attack()
        {
            var input = Input.GetMouseButton(0);
            
            if (input)
            {
                IsAttacking = true;
                animator.SetBool(attackHash, true);
            }
            else
            {
                IsAttacking = false;
                animator.SetBool(attackHash, false);
            }
        }

        private void Die()
        {
            if (_playerStats.GetCurrentHealth() == 0)
            {
                Debug.Log("Player is dead");
                animator.SetBool(attackHash, false);
                animator.SetTrigger(deadHash);
                IsDead = true;
                enabled = false;
            }
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
                if (EnemyCombat.AttackingWithFoot)
                {
                    Debug.Log("Player was hit by enemy foot");
                    _playerStats.TakeDamage(20); 
                }

            }
            
        }
        
    }
}

using System;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        public static bool IsAttacking;
        
        private int attackHash = Animator.StringToHash("Attack");
        private int deadHash = Animator.StringToHash("Dead");

        private PlayerController _playerController;
        private PlayerStats _playerStats;
        
        public Animator animator;
        
        void Start()
        {
            animator = GetComponent<Animator>();
            _playerController = GetComponent<PlayerController>();
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
                animator.SetBool(deadHash, true);
                //Destroy(gameObject, 3);
                enabled = false;
                //_playerController.enabled = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("EnemyHands"))
            {
                Debug.Log("Player was hit by enemy");
                _playerStats.TakeDamage(20);
                
            }
            
        }
        
    }
}

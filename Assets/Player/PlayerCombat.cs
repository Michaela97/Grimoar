using System;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        public float health = 100;
        public static bool IsAttacking;
        public static bool PlayerIsDead;
        
        private int attackHash = Animator.StringToHash("Attack");
        private int deadHash = Animator.StringToHash("Dead");

        private PlayerController _playerController;
        
        public Animator animator;
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            _playerController = GetComponent<PlayerController>();
        }

        // Update is called once per frame
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
            if (health == 0f)
            {
                PlayerIsDead = true;
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
                Debug.Log("TRIGGER: Player was hit by enemy");
                health -= 10f;
            }
            
        }
        
    }
}

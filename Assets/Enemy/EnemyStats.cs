using System;
using System.Threading;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyStats : MonoBehaviour
    {
        public float health = 100;
        public Animator animator;
        private int isDeadHash = Animator.StringToHash("IsDead");
        public static bool EnemyIsDead;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Sword"))
            {
                if (PlayerCombat.IsAttacking)
                {
                    health -= 10f;
                    Debug.Log("Enemy health = " + health);
                    Die();
                }
            }
        }

        private void Die()
        {
            if (health == 0)
            {
                EnemyIsDead = true;

                animator.SetBool(isDeadHash, true);
                Destroy(gameObject, 3);
                enabled = false;
            }
        }
    }
}
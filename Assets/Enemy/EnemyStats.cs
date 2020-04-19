using System;
using System.Threading;
using System.Threading.Tasks;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyStats : MonoBehaviour
    {
        public float health = 100;
        public Animator animator;
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
                    SetHitAnimation();
                    TakeDamage(10f);
                    Debug.Log("Enemy health = " + health);
                    Die();
                }
            }
        }

        private void SetHitAnimation()
        {
            animator.SetTrigger(EnemyAnimHash.IsHitHash);
        }

        private void TakeDamage(float dmg)
        {
            health -= dmg;
        }

        private void Die()
        {
            if (health == 0)
            {
                EnemyIsDead = true;

                animator.SetBool(EnemyAnimHash.IsDeadHash, true);
                Destroy(gameObject, 3);
                enabled = false;
            }
        }
    }
}
﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyStats : MonoBehaviour
    {
        public float health = 20;
        public Animator animator;
        public static bool EnemyIsDead;
        private delegate void TakeDamageDelegate();

        private TakeDamageDelegate _takeDamageDelegate;

        private void Start()
        {
            animator = GetComponent<Animator>();

            _takeDamageDelegate += SetHitAnimation;
            _takeDamageDelegate += TakeDamage;
            _takeDamageDelegate += Die;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Sword"))
            {
                if (PlayerCombat.IsAttacking)
                {
                    _takeDamageDelegate?.Invoke();
                    Debug.Log("Enemy health = " + health);
                }
            }
        }

        private void SetHitAnimation()
        {
            animator.SetTrigger(EnemyAnimHash.IsHitHash);
        }

        private void TakeDamage()
        {
            health -= 10f;
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
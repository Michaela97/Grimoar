
using Enemy;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        public static bool IsAttacking;
        public static bool IsDead;
        public static bool IsHit;
        
        private PlayerStats _playerStats;
        [SerializeField]
        private Animator animator;
        private AudioManager _audioManager;
        public delegate void OnDiedEvent();
        public delegate void TakeDamageEvent(int damage);
        public static event OnDiedEvent hasDied;
        public static event TakeDamageEvent takeDmg;
        
        private PlayerController _playerController;

        void Start()
        {
            animator = GetComponent<Animator>();
            
            _playerStats = FindObjectOfType<PlayerStats>();
            _playerController = FindObjectOfType<PlayerController>();
            _audioManager = FindObjectOfType<AudioManager>();
            
            hasDied += SetDyingAnimation;
            hasDied += DisableScripts;
            hasDied += GameOver;
            
            takeDmg += OnIsHit;
        }
        
        void Update()
        {
            Attack();
            Die();
            
            if (IsHit && _playerStats.GetCurrentHealth() > 0)
            {
                takeDmg?.Invoke(10);
            }
        }

        private void Die()
        {
            if (_playerStats.GetCurrentHealth() < 1)
            {
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

        private void OnIsHit(int dmg)
        {
            animator.SetTrigger(PlayerAnimHash.isHitHash);
            _playerStats.TakeDamage(dmg);
            IsHit = false;
            _audioManager.PlayOnce("UGHSound");
        }

        private void GameOver()
        {
            GameOverManager.gameOver = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("EnemyHands"))
            {
                if (EnemyCombat.EnemyIsAttacking)
                {
                    Debug.Log("Player was hit by enemy hands");
                    takeDmg?.Invoke(10);
                }
            }

            if (other.gameObject.CompareTag("EnemyFoot"))
            {
                if (EnemyCombat.AttackingWithFoot && EnemyCombat.EnemyIsAttacking)
                {
                    Debug.Log("Player was hit by enemy foot");
                    takeDmg?.Invoke(20);
                }
            }
        }
    }
}
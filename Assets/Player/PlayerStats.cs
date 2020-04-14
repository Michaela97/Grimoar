using HealthBar;
using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        public int maxHealth = 100;
        public int currentHealth;

        public PlayerHealthBar healthBar;

        private void Start()
        {
            healthBar = gameObject.AddComponent<PlayerHealthBar>();
           
            currentHealth = maxHealth;
       
            healthBar.SetMaxHealth(currentHealth);
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            Debug.Log("Player: dmg taken, current health: " + currentHealth);
        
        }

        public int GetCurrentHealth()
        {
            return currentHealth;
       
        }
    }
}
    

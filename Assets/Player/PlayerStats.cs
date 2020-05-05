using HealthBar;
using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        public int maxHealth = 50;
        private static int currentHealth;
        private static int i = 0;

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

        public void AddHealth()
        {
            Debug.Log("Before Current health: " + currentHealth + " / no, of calls: " + i);

            currentHealth += 10;
            healthBar.SetHealth(currentHealth);
            Debug.Log("Player took the potion. Current health: " + currentHealth + " / no, of calls: " + i);
            i += 1;
        }
    }
}
    

﻿using HealthBar;
using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField]
        private int maxHealth = 100;
        private static int currentHealth;

        public PlayerHealthBar healthBar;
        
        
        private void Start()
        {
            healthBar = gameObject.AddComponent<PlayerHealthBar>();
           
            currentHealth = maxHealth;
       
            healthBar.SetMaxHealth(currentHealth);
            healthBar.SetHealth(currentHealth);
            
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
            Debug.Log("before health: " + currentHealth);
            currentHealth += 10;
            Debug.Log("current health: " + currentHealth);
            healthBar.SetHealth(currentHealth);
        }
    }
}
    

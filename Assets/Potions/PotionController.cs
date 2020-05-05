using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class PotionController : MonoBehaviour
{
    private PlayerStats _playerStats;

    private void Start()
    {
        _playerStats = gameObject.AddComponent<PlayerStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            _playerStats.AddHealth();
        }
    }


    
}

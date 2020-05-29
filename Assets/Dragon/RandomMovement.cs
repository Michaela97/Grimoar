using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    [Range(5,10)]
    [SerializeField]
    private float wanderRadius;
    [Range(3,6)]
    [SerializeField]
    private float wanderTimer;
    [SerializeField]
    private NavMeshAgent agent;
    private float _timer;
    private AudioManager _audioManager;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
 
        if (_timer >= wanderTimer) {
            Vector3 newPos = AgentRandomMovement.RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            _timer = 0;
        }
    }
}
